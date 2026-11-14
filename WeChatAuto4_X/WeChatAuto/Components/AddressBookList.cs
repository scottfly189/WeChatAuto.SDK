using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Input;
using FlaUI.Core.Tools;
using FlaUI.Core.WindowsAPI;
using Microsoft.Extensions.DependencyInjection;
using WeAutoCommon.Models;
using WeAutoCommon.Utils;
using WeChatAuto.Extentions;
using WeChatAuto.Utils;
using WeAutoCommon.Enums;
using System.Threading.Tasks;
using FlaUI.UIA3;
using WeAutoCommon.Extentions;
using System.IO;
using FlaUI.Core;
using MessagePack;
using System.Net.Http;
using System.Drawing;
using WeChatAuto.Options;
using WeChatAuto.Models;
using System.Windows.Navigation;


namespace WeChatAuto.Components
{
	/// <summary>
	/// 通讯录列表
	/// </summary>
	public class AddressBookList
	{
		private readonly IServiceProvider _serviceProvider;
		private UIThreadInvoker _uiMainThreadInvoker;
		private AutoLogger<AddressBookList> _logger;
		private WeChatClient _Client;
		private ListBox Root => _Client.MainWindow.FindFirstDescendant(cf => cf.ByName("通讯录").And(cf.ByAutomationId("primary_table_.contact_list"))).AsListBox();
		public AddressBookList(WeChatClient client, UIThreadInvoker uiThreadInvoker, IServiceProvider serviceProvider)
		{
			_logger = serviceProvider.GetRequiredService<AutoLogger<AddressBookList>>();
			_uiMainThreadInvoker = uiThreadInvoker;
			_Client = client;
			_serviceProvider = serviceProvider;
		}
		/// <summary>
		///<para> 获取所有好友的信息列表,具体请考<see cref="FriendInfo"/>类说明.</para>
		///<para> 注意：只会获取通讯录中的联系人、企业微信联系人和群聊的记录,公众号，服务号，我的企业等特殊账号不会获取.</para>
		///<para> 1.如果是企业微信，会剔除@xxxx后缀，以保持一致性.</para>
		///<para> 2.如果好友/群聊/企业微信联系人等有备注，则备注会覆盖昵称显示.</para>
		///<para> 3.注意：如果微信联系人有重名，此方法会仅获取/保存一个联系人，所以运行此方法前:建议好友/群聊/企业微信联系人有重名时，通过手工的方式添加备注，以保持区分.</para>
		///<para> 4.普通联系人可以获取wxid,其他的如：群聊/企业微信联系人无法获取wxid.</para>
		///<para> 5. 此方法运行结果会保存在cache中,默认为true,从cache中获取数据，如果设置为false,则重新刷新一遍通讯录,cache也会同步更新，建议实际开发过程中运行一遍从通讯录获取好友信息的操作,并且做好添加好友时的同步工作（在一些监听的场景，如果读取到此好友没有wxid,也会自动获取,并同步更新cache，所以也不必太担心cache的过时问题）</para>
		/// </summary>
		/// <param name="fromCache">是否从cahce中获取数据</param>
		/// <returns>好友列表</returns>
		public async Task<List<FriendInfo>> GetAllFriends(bool fromCache = true)
		{
			return await WeChatInvoker.Call(GetAllFriendsCore, fromCache);
		}

		internal List<FriendInfo> GetAllFriendsCore(UIA3Automation automation, bool fromCache)
		{
			try
			{
				//通过cache获取数据
				if (fromCache)
				{
					(bool success, List<FriendInfo> lt) cacheResult = _GetFromCache();
					if (cacheResult.success)
					{
						return cacheResult.lt;
					}
				}
				//cache没有，重新刷新
				this._Client.Navigation.SwitchNavigationCore(automation, NavigationType.通讯录);
				var list = new List<FriendInfo>();
				var root = Root;
				if (root == null)
					return list;
				_Client.MainWindow.Focus();
				root?.DrawHighlightExt();
				__CollapseAllGroups(root);   //先折叠所有分组
											 //获取群聊的记录
				var items = root.Items;
				__FetchGroupChatFriends(items, list);
				__CollapseAllGroups(root);   //折叠所有分组
											 //获取企业微信联系人的记录
				items = root.Items;
				__FetchEntpriseWeChatFriends(items, list);
				__CollapseAllGroups(root);   //折叠所有分组
											 //获取普通联系人的记录
				items = root.Items;
				_FetchNormalFriends(items, list);
				__CollapseAllGroups(root);   //折叠所有分组
				_SaveList(list); //保存进入cache中
				return list;
			}
			catch (Exception ex)
			{
				_logger.Error($"获取好友列表失败，异常信息：{ex}");
				return new List<FriendInfo>();
			}
			finally
			{
				this._Client.Navigation.SwitchNavigationCore(automation, NavigationType.微信);
			}
		}
		//如果有cache，则取cache
		private (bool success, List<FriendInfo> lt) _GetFromCache()
		{
			var path = Path.Combine(AppContext.BaseDirectory, _Client.Alias + "_cache.dat");
			if (File.Exists(path))
			{
				byte[] bytes = File.ReadAllBytes(path);
				var lt = MessagePackSerializer.Deserialize<List<FriendInfo>>(bytes);
				return (true, lt);
			}
			return (false, null);
		}

		private void _SaveList(List<FriendInfo> list)
		{
			byte[] bytes = MessagePackSerializer.Serialize(list);
			var path = Path.Combine(AppContext.BaseDirectory, _Client.Alias + "_cache.dat");
			File.WriteAllBytes(path, bytes);
			RandomWait.Wait(800, 1800);
			_Client.MainWindow.Focus();
		}
		private System.Drawing.Point GetSafeRandomPoint(Rectangle rectangle)
		{
			var width = (int)(rectangle.Width * 0.2);  //取1/4的安全位置
			var height = (int)(rectangle.Height * 0.25);

			Random random = new Random((int)DateTime.Now.Ticks);
			var x = rectangle.Left + (int)(rectangle.Width / 2) + random.Next(width * -1, width);
			var y = rectangle.Top + (int)(rectangle.Height / 2) + random.Next(1, 150);
			return new System.Drawing.Point(x, y);
		}

		private void _FetchNormalFriends(ListBoxItem[] items, List<FriendInfo> list)
		{
			try
			{
				var rootItem = items.FirstOrDefault(u => u.Name.StartsWith("联系人"));
				if (rootItem == null)
					return;
				rootItem.DrawHighlightExt();
				rootItem.Click();   //展开群聊分组
				RandomWait.Wait(500, 1000);


				var count = int.TryParse(rootItem.Name.Substring(3), out var result) ? result : 0;  //获得联系人实际数量
				var findCount = 0;

				var scrollPoint = GetSafeRandomPoint(Root.BoundingRectangle);
				AutomationElement lastElement = Root.Items.LastOrDefault(u => u.ClassName.Equals("mmui::ContactsCellItemView") && u.ControlType.Equals(ControlType.ListItem));
				Mouse.Position = scrollPoint;
				RandomWait.Wait(50, 300);

				int index = 0;
				List<string> oldSnap = new List<string>();
				Random random = new Random((int)DateTime.Now.Ticks);
				while (index < 3)
				{
					var newItems = Root.Items.Where(u => u.ClassName.Equals("mmui::ContactsCellItemView") &&
						u.ControlType.Equals(ControlType.ListItem) && u.BoundingRectangle.Y >= Root.BoundingRectangle.Y && u.BoundingRectangle.Y + u.BoundingRectangle.Height <= Root.BoundingRectangle.Y + Root.BoundingRectangle.Height).ToList();
					var newSnap = newItems.Select(u => u.Name.Trim()).ToList();
					var actionList = newSnap.Except(oldSnap).ToList();
					oldSnap = newSnap;  //为下一次做准备.
					if (actionList.Count() == 0)
					{
						_DownStep(scrollPoint, random, 2);
						index++;
						continue;
					}
					index = 0;
					//获取用户数据
					foreach (var item in actionList)
					{
						if (string.IsNullOrWhiteSpace(item))
							continue;
						var subItem = Root.Items.Where(u => u.ClassName.Equals("mmui::ContactsCellItemView") &&
						u.ControlType.Equals(ControlType.ListItem) && u.BoundingRectangle.Y >= Root.BoundingRectangle.Y && u.BoundingRectangle.Y + u.BoundingRectangle.Height <= Root.BoundingRectangle.Y + Root.BoundingRectangle.Height).ToList().Find(u => u.Name.Trim().Equals(item.Trim()));
						if (subItem != null)
						{
							FriendInfo friendInfo = new FriendInfo();
							friendInfo.NickName = subItem.Name.Trim();  //后面纠正.
							friendInfo.MemoName = subItem.Name.Trim();
							friendInfo.ChatType = ChatType.好友;
							subItem.DrawHighlightExt();
							if (subItem.BoundingRectangle.Y + subItem.BoundingRectangle.Height > Root.BoundingRectangle.Y + Root.BoundingRectangle.Height)
							{
								_DownStep(scrollPoint, random, 3);
								subItem = Root.Items.Where(u => u.Name.Trim().Equals(item) && u.ClassName.Equals("mmui::ContactsCellItemView") &&
								  u.ControlType.Equals(ControlType.ListItem) && u.BoundingRectangle.Y >= Root.BoundingRectangle.Y && u.BoundingRectangle.Y + u.BoundingRectangle.Height <= Root.BoundingRectangle.Y + Root.BoundingRectangle.Height).FirstOrDefault();
							}
							RandomWait.Wait(100, 600);
							__FetchWxUserInfo(list, subItem, friendInfo);
							list.Add(friendInfo);
							findCount++;
						}
					}

					_DownStep(scrollPoint, random, 2);
					var lastItem = Root.Items.LastOrDefault(u => u.ClassName.Equals("mmui::ContactsCellItemView") && u.ControlType.Equals(ControlType.ListItem));
					if (lastItem == null)
						break;
					if (lastItem.Name != lastElement.Name)
					{
						lastElement = lastItem;
					}
					else
					{
						index++;
					}
				}

				if (findCount != count)
				{
					_logger.Error($"读取好友数量与实际的值不一致: 读取到{findCount}个，实际应该有:{count}个.");
				}
			}
			catch (Exception ex)
			{
				_logger.Error($"滚动点击时发生错误，错误原因:{ex.ToString()}");
			}
		}

		private static void _DownStep(System.Drawing.Point scrollPoint, Random random, int maxStep)
		{
			for (int i = 0; i < maxStep; i++)
			{
				Mouse.Position = scrollPoint;
				RandomWait.Wait(50, 200);
				if (i == 0)
				{
					Mouse.Scroll(-1 * random.Next(1, 3));
				}
				if (i == maxStep - 1)
				{
					Mouse.Scroll(-1 * random.Next(3, 5));
				}
				RandomWait.Wait(50, 300);
			}
		}

		//可能为空
		//也可能重复
		private void __FetchWxUserInfo(List<FriendInfo> list, AutomationElement subItem, FriendInfo friendInfo)
		{
			var index = 0;
			var success = false;
			while (!success && index < 2)
			{
				try
				{
					var clkRetry = Retry.WhileFalse(() =>
					{
						try
						{
							return !subItem.Properties.BoundingRectangle.Value.IsEmpty;
						}
						catch (Exception)
						{
							return false;
						}
					}, timeout: TimeSpan.FromSeconds(4), interval: TimeSpan.FromMilliseconds(200));
					if (clkRetry.Success)
					{
						subItem.Click();
						RandomWait.Wait(200, 900);
						var grouproot = _Client.MainWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Group).And(cf.ByAutomationId("qt_scrollarea_viewport.profile_h_view")));
						if (grouproot == null)
							return;
						__FetchWxUserInfoCore(grouproot, friendInfo);
						success = true;
						break;
					}
					else
					{
						_logger.Error("没有获取到点击位位置");
						RandomWait.Wait(300, 900);
					}
				}
				catch (Exception ex)
				{
					index++;
					RandomWait.Wait(300, 900);
					var subItemRetry = Retry.WhileNull(() => Root.Items.Where(u => u.Name.Trim().Equals(subItem.Name.Trim()) && u.ClassName.Equals("mmui::ContactsCellItemView") &&
					  u.ControlType.Equals(ControlType.ListItem) && u.BoundingRectangle.Y >= Root.BoundingRectangle.Y && u.BoundingRectangle.Y + u.BoundingRectangle.Height <= Root.BoundingRectangle.Y + Root.BoundingRectangle.Height).FirstOrDefault(),
					  timeout: TimeSpan.FromSeconds(2), interval: TimeSpan.FromMilliseconds(200));
					if (subItemRetry.Success)
					{
						subItem = subItemRetry.Result;
					}
					else
					{
						break;
					}

					if (index < 2)
					{
						_logger.Error($"点击列表项目发生错误，准备重试！错误原因:{ex.ToString()}");
					}
				}
			}
		}

		private void __FetchWxUserInfoCore(AutomationElement grouproot, FriendInfo friendInfo)
		{
			var nickNameRetry = Retry.WhileNull(() => grouproot.FindFirstDescendant(cf => cf.ByName("昵称：").And(cf.ByAutomationId("right_v_view.user_info_center_view.basic_line_view.basic_line.key_text")))
			, timeout: TimeSpan.FromSeconds(1), interval: TimeSpan.FromMilliseconds(200));
			if (nickNameRetry.Success)
			{
				var nickName = nickNameRetry.Result;
				var nickValue = nickName.GetSibling(1);
				if (nickValue != null)
				{
					friendInfo.NickName = nickValue.Name.Trim();
				}
			}
			//wxid
			var item = grouproot.FindFirstDescendant(cf => cf.ByAutomationId("right_v_view.user_info_center_view.basic_line_view.basic_line.key_text").And(cf.ByName("微信号：")));
			if (item != null)
			{
				item = item.GetSibling(1);
				if (item != null)
				{
					friendInfo.WxId = item.Name;
				}
			}
			//地区
			item = grouproot.FindFirstDescendant(cf => cf.ByAutomationId("right_v_view.user_info_center_view.basic_line_view.basic_line.key_text").And(cf.ByName("地区：")));
			if (item != null)
			{
				item = item.GetSibling(1);
				if (item != null)
				{
					friendInfo.Area = item.Name;
				}
			}
			//头像
			item = grouproot.FindFirstDescendant(cf => cf.ByAutomationId("head_image_v_view").And(cf.ByControlType(ControlType.Group)));
			if (item != null)
			{
				var path = Path.Combine(AppContext.BaseDirectory, "Avator", $"{friendInfo.WxId}.png");
				item.CaptureToFile(path);
				friendInfo.AvatarPath = path;
			}
			//共同群聊
			item = grouproot.FindFirstDescendant(cf => cf.ByAutomationId("content_v_view.ProfileResizeVBoxView.detail_content_host.detail_center_v_view.detail_derived_content_view.section_shell.more_line_v_view.chatroom_intersection.key_text_h_view.key_text_view").And(cf.ByName("共同群聊")));
			if (item != null)
			{
				var parent = item.GetParent().GetParent();
				var button = parent.FindFirstChild(cf => cf.ByControlType(ControlType.Button));
				if (button != null)
				{
					var text = button.FindFirstDescendant(cf => cf.ByControlType(ControlType.Text));
					if (text != null)
					{
						friendInfo.SameGroupNumber = text.Name;
					}
				}
			}
			//个性签名
			item = grouproot.FindFirstDescendant(cf => cf.ByAutomationId("content_v_view.ProfileResizeVBoxView.detail_content_host.detail_center_v_view.detail_derived_content_view.section_shell.more_line_v_view.sign.key_text_h_view.key_text_view").And(cf.ByName("个性签名")));
			if (item != null)
			{
				var parent = item.GetParent().GetParent();
				var button = parent.FindFirstChild(cf => cf.ByControlType(ControlType.Button));
				if (button != null)
				{
					var text = button.FindFirstDescendant(cf => cf.ByControlType(ControlType.Text));
					if (text != null)
					{
						friendInfo.Signature = text.Name;
					}
				}
			}
			//来源
			item = grouproot.FindFirstDescendant(cf => cf.ByAutomationId("content_v_view.ProfileResizeVBoxView.detail_content_host.detail_center_v_view.detail_derived_content_view.section_shell.more_line_v_view.source.key_text_h_view.key_text_view").And(cf.ByName("来源")));
			if (item != null)
			{
				var parent = item.GetParent().GetParent();
				var button = parent.FindFirstChild(cf => cf.ByControlType(ControlType.Button));
				if (button != null)
				{
					var text = button.FindFirstDescendant(cf => cf.ByControlType(ControlType.Text));
					if (text != null)
					{
						friendInfo.Source = text.Name;
					}
				}
			}
			//添加时间
			item = grouproot.FindFirstDescendant(cf => cf.ByAutomationId("content_v_view.ProfileResizeVBoxView.detail_content_host.detail_center_v_view.detail_derived_content_view.section_shell.more_line_v_view.became_friend_time.key_text_h_view.key_text_view").And(cf.ByName("添加时间")));
			if (item != null)
			{
				var parent = item.GetParent().GetParent();
				var button = parent.FindFirstChild(cf => cf.ByControlType(ControlType.Button));
				if (button != null)
				{
					var text = button.FindFirstDescendant(cf => cf.ByControlType(ControlType.Text));
					if (text != null)
					{
						friendInfo.AddDateTime = text.Name;
					}
				}
			}
			//标签
			item = grouproot.FindFirstDescendant(cf => cf.ByAutomationId("content_v_view.ProfileResizeVBoxView.detail_content_host.detail_center_v_view.detail_derived_content_view.section_shell.main_line_v_view.tag_line.key_text_h_view.key_text_view").And(cf.ByName("标签")));
			if (item != null)
			{
				var parent = item.GetParent().GetParent();
				var button = parent.FindFirstChild(cf => cf.ByControlType(ControlType.Button));
				if (button != null)
				{
					var text = button.FindFirstDescendant(cf => cf.ByControlType(ControlType.Text));
					if (text != null)
					{
						if (!string.IsNullOrWhiteSpace(text.Name))
						{
							var labelList = text.Name.Split(',').ToList();
							friendInfo.Lable = labelList;
						}
					}
				}
			}
		}

		private void ___UpdateAddressBookMemoName(AutomationElement grouproot, FriendInfo friendInfo)
		{
			var button = grouproot.FindFirstDescendant(cf => cf.ByAutomationId("content_v_view.ProfileResizeVBoxView.detail_content_host.detail_center_v_view.detail_derived_content_view.section_shell.main_line_v_view.remark_line.value_remark_view").And(cf.ByControlType(ControlType.Button)).And(cf.ByClassName("mmui::ProfileDetailValueRemarkView")));
			if (button == null)
				return;
			button.DrawHighlightExt();
			var point = button.BoundingRectangle.SafeRandomPoint();
			Mouse.Position = point;
			Mouse.Click();
			Keyboard.TypeSimultaneously(VirtualKeyShort.CONTROL, VirtualKeyShort.KEY_A);
			RandomWait.Wait(50, 200);
			Keyboard.TypeSimultaneously(virtualKeys: VirtualKeyShort.BACK);
			RandomWait.Wait(50, 300);
			ClipboardHelper.SetText(friendInfo.MemoName);
			Keyboard.TypeSimultaneously(VirtualKeyShort.CONTROL, VirtualKeyShort.KEY_V);
			RandomWait.Wait(50, 300);
			Keyboard.Type(VirtualKeyShort.ENTER);
		}

		//获取企业微信联系人的记录
		private void __FetchEntpriseWeChatFriends(ListBoxItem[] items, List<FriendInfo> list)
		{
			var tmpList = new List<FriendInfo>();
			var rootItem = items.FirstOrDefault(u => u.Name.StartsWith("企业微信联系人"));
			if (rootItem == null)
				return;
			var count = int.TryParse(rootItem.Name.Substring(7), out var result) ? result : 0;
			rootItem.DrawHighlightExt();
			rootItem.Click();   //展开群聊分组
			RandomWait.Wait(500, 1000);

			var root = Root;
			var scrollPoint = root.BoundingRectangle.SafeRandomPoint();

			var subItem = rootItem.GetSibling(1);  //获取第一个子项
			while (subItem != null && subItem.ClassName != "mmui::ContactsCellGroupView")
			{
				if (subItem.ClassName == "mmui::ContactsCellItemView")
				{
					FriendInfo friendInfo = new FriendInfo();
					if (subItem.Name.Trim().IndexOf("@") > 0)
					{
						var name = subItem.Name.Trim().Substring(0, subItem.Name.Trim().IndexOf("@"));
						friendInfo.NickName = name.Trim();
						friendInfo.MemoName = subItem.Name.Trim();
					}
					else
					{
						friendInfo.NickName = subItem.Name.Trim();
						friendInfo.MemoName = subItem.Name.Trim();
					}
					friendInfo.ChatType = ChatType.企业微信;
					subItem.DrawHighlightExt();
					tmpList.Add(friendInfo);
				}
				if (subItem.BoundingRectangle.Y > root.BoundingRectangle.Y + (int)(root.BoundingRectangle.Height * 0.7))
				{
					Mouse.Position = scrollPoint;
					Mouse.Scroll(-3);
					RandomWait.Wait(100, 400);
				}
				var tryNextItem = subItem.GetSibling(1);
				if (tryNextItem == null)
				{
					Mouse.Position = scrollPoint;
					Mouse.Scroll(-3);
					tryNextItem = subItem.GetSibling(1);
					subItem = tryNextItem;
					RandomWait.Wait(100, 400);
				}
				else
				{
					if (tryNextItem.ClassName == "mmui::ContactsCellGroupView")
						break;
					subItem = tryNextItem;
					if (subItem.BoundingRectangle.Y + subItem.BoundingRectangle.Height > root.BoundingRectangle.Y + root.BoundingRectangle.Height)
					{
						Mouse.Position = scrollPoint;
						Mouse.Scroll(-1);
						RandomWait.Wait(100, 400);
					}
					else
					{
						Mouse.Position = scrollPoint;
						Mouse.Scroll(-2);
						RandomWait.Wait(100, 400);
					}
				}
			}
			if (tmpList.Count != count)
			{
				_logger.Error($"读取企业微信与实际的值不一致: 读取到{tmpList.Count}个，实际应该有:{count}个.");
			}
			list.AddRange(tmpList);
		}



		private void __FetchGroupChatFriends(ListBoxItem[] items, List<FriendInfo> list)
		{
			var item = items.FirstOrDefault(u => u.Name.StartsWith("群聊"));
			if (item == null)
				return;
			item.DrawHighlightExt();
			item.Click();   //展开群聊分组
			RandomWait.Wait(500, 1000);

			var count = int.TryParse(item.Name.Substring(2), out var result) ? result : 0;

			var root = Root;
			var scrollPoint = root.BoundingRectangle.SafeRandomPoint();

			var findCount = 0;

			var subItem = item.GetSibling(1);  //获取第一个子项
			while (subItem != null && subItem.ClassName == "mmui::ContactsCellItemView")
			{
				if (subItem.ClassName == "mmui::ContactsCellItemView")
				{
					FriendInfo friendInfo = new FriendInfo();
					friendInfo.NickName = subItem.Name.Trim();
					friendInfo.MemoName = subItem.Name.Trim();
					friendInfo.ChatType = ChatType.群聊;
					if (!list.Any(f => f.NickName == friendInfo.NickName))
					{
						subItem.DrawHighlightExt();
						list.Add(friendInfo);
						findCount++;
					}
				}
				if (subItem.BoundingRectangle.Y > root.BoundingRectangle.Y + (int)(root.BoundingRectangle.Height * 0.7))
				{
					Mouse.Position = scrollPoint;
					Mouse.Scroll(-3);
					RandomWait.Wait(100, 400);
				}
				var tryNextItem = subItem.GetSibling(1);
				if (tryNextItem == null)
				{
					Mouse.Position = scrollPoint;
					Mouse.Scroll(-3);
					tryNextItem = subItem.GetSibling(1);
					subItem = tryNextItem;
					RandomWait.Wait(100, 400);
				}
				else
				{
					if (tryNextItem.ClassName == "mmui::ContactsCellGroupView")
						break;
					subItem = tryNextItem;
					if (subItem.BoundingRectangle.Y + subItem.BoundingRectangle.Height > root.BoundingRectangle.Y + root.BoundingRectangle.Height)
					{
						Mouse.Position = scrollPoint;
						Mouse.Scroll(-1);
						RandomWait.Wait(100, 400);
					}
					else
					{
						Mouse.Position = scrollPoint;
						Mouse.Scroll(-2);
						RandomWait.Wait(100, 400);
					}
				}

			}

			if (findCount != count)
			{
				_logger.Error($"读取群聊个数与实际的值不一致: 读取到{findCount}个，实际应该有:{count}个.");
			}
		}

		private void __CollapseAllGroups(ListBox root)
		{
			var item = root.FindFirstChild(cf => cf.ByClassName("mmui::ContactsCellGroupView").And(cf.ByControlType(ControlType.ListItem)));
			if (item == null || item.BoundingRectangle.Y > root.BoundingRectangle.Y + 10)
			{
				__GotoTop(root);
			}
			item = root.FindFirstChild(cf => cf.ByClassName("mmui::ContactsCellGroupView").And(cf.ByControlType(ControlType.ListItem)));
			while (item != null)
			{
				item = item.GetSibling(1);
				if (item != null)
				{
					if (item.ClassName != "mmui::ContactsCellGroupView")
					{
						var parent = item.GetSibling(-1);
						parent.Click();
						RandomWait.Wait(300, 800);
						item = root.FindFirstChild(cf => cf.ByClassName("mmui::ContactsCellGroupView").And(cf.ByControlType(ControlType.ListItem)));
					}
				}
				else
				{
					break;
				}
			}

		}

		private void __GotoTop(ListBox root)
		{
			int index = 0;
			var itemName = "";
			var point = root.BoundingRectangle.SafeRandomPoint();
			var random = new Random((int)DateTime.Now.Ticks);
			while (index < 2)
			{
				Mouse.Position = point;
				Mouse.Scroll(random.Next(5, 12));
				RandomWait.Wait(20, 100);
				if (root.Items.First().Name == itemName)
				{
					index++;
				}
				else
				{
					index = 0;
					itemName = root.Items.First().Name;
				}
			}

		}

		/// <summary>
		/// 获取所有好友名称列表.（通过通讯录）
		/// 如果好友有昵称与备注，优先选择备注名
		/// 注意：如果是企业微信，会剔除@xxxx后缀，以保持一致性.
		/// </summary>
		/// <returns></returns>
		internal async Task<List<string>> GetAllFriendNames()
		{
			var list = (await GetAllFriends(true)).Select(f => f.Name).ToList();
			return list;
		}

		/// <summary>
		/// 通过新的好友加好友申请
		/// </summary>
		/// <param name="options">配置对象，具体参见<see cref="FriendRequestAutoAcceptOptions"/></param>
		/// <param name="token">取消今牌</param>
		/// <returns>返回加成功的好友昵称</returns>
		public async Task<List<NewFriendBackItem>> PassedAllNewFriend(FriendRequestAutoAcceptOptions options, CancellationToken token)
		{
			var list = await WeChatInvoker.Call(PassedAllNewFriendCore, options, token);
			if (list.Count > 0)
			{
				if (options.PassedCallBack != null)
				{
					await options?.PassedCallBack(list, this._Client, this._serviceProvider);
				}
			}
			return list;
		}


		internal List<NewFriendBackItem> PassedAllNewFriendCore(UIA3Automation automation, FriendRequestAutoAcceptOptions options, CancellationToken token)
		{
			this._Client.Navigation.SwitchNavigationCore(automation, NavigationType.通讯录);
			try
			{
				token.ThrowIfCancellationRequested();
				var result = new List<NewFriendBackItem>();
				if (Root == null)
					return result;
				this._Client.MainWindow.Focus();
				__CollapseAllGroups(Root);
				var newFriendRootItem = Root.Items.Where(u => u.Name.Equals("新的朋友") && u.ClassName.Equals("mmui::ContactsCellGroupView") && u.ControlType == ControlType.ListItem).FirstOrDefault();
				if (newFriendRootItem == null)
					return result;
				newFriendRootItem.Click();
				token.ThrowIfCancellationRequested();
				RandomWait.Wait(300, 900);
				var scrollPoint = Root.BoundingRectangle.SafeRandomPoint();
				var downIndex = 0;
				List<string> oldSnapList = new List<string>();
				while (downIndex < 2)
				{
					(bool scroll, List<string> snapList) processTag = _ProcessThisPage(options, token, automation, result);
					if (!processTag.scroll)
						break;
					var exceptList = processTag.snapList.Except(oldSnapList).ToList();
					if (exceptList.Count == 0)
					{
						downIndex++;
					}
					else
					{
						downIndex = 0;
						oldSnapList = processTag.snapList;
					}
					this._Client.MainWindow.Focus();
					Mouse.Position = scrollPoint;
					Mouse.Scroll(-3);
				}
				this._Client.Navigation.SwitchNavigationCore(automation, NavigationType.微信);
				return result;
			}
			catch (Exception ex)
			{
				_logger.Error($"通过好友申请时发生错误，错误原因:{ex.ToString()}");
				return new List<NewFriendBackItem>();
			}
			finally
			{
				this._Client.Navigation.SwitchNavigationCore(automation, NavigationType.微信);
			}
		}

		//反复处理本页
		private (bool scroll, List<string> snapList) _ProcessThisPage(FriendRequestAutoAcceptOptions options, CancellationToken token, UIA3Automation automation, List<NewFriendBackItem> resultList)
		{
			bool change = false;
			while (true)
			{
				var newFriendRootItem = Root.Items.Where(u => u.Name.Equals("新的朋友") && u.ClassName.Equals("mmui::ContactsCellGroupView") && u.ControlType == ControlType.ListItem).FirstOrDefault();
				if (newFriendRootItem == null)
					return (false, null);
				var items = Root.FindAllChildren(cf => cf.ByClassName("mmui::XTableCell").And(cf.ByControlType(ControlType.ListItem)));
				if (items.Length == 0)
				{
					items = Root.FindAllChildren(cf => cf.ByClassName("mmui::ContactsCellNewFriendView").And(cf.ByControlType(ControlType.ListItem)));
				}
				foreach (var item in items)
				{
					token.ThrowIfCancellationRequested();
					var thisChangeTag = _ProcessThisItem(item, options, token, automation, resultList);
					RandomWait.Wait(600, 1500);
					if (thisChangeTag)
					{
						change = true;
						break;  //如果被子项被改变了，通讯录列表也被改变，则重新重试一次.
					}
					#region 退出策略
					var retryItem = item.GetSibling(1);
					if (retryItem != null && !retryItem.ClassName.Equals("mmui::ContactsCellNewFriendView"))
					{
						return (false, null);
					}
					retryItem = item.GetSibling(-1);
					if (retryItem != null && !retryItem.ClassName.Equals("mmui::ContactsCellNewFriendView") && !retryItem.Name.Equals("新的朋友"))
					{
						return (false, null);
					}
					#endregion
					change = false;
				}
				if (!change)
				{
					break;
				}
			}
			return (true, Root.FindAllChildren(cf => (cf.ByClassName("mmui::XTableCell").Or(cf.ByClassName("mmui::ContactsCellNewFriendView"))).And(cf.ByControlType(ControlType.ListItem))).Select(u => u.Name).ToList());
		}

		private bool _ProcessThisItem(AutomationElement item, FriendRequestAutoAcceptOptions options, CancellationToken token, UIA3Automation automation, List<NewFriendBackItem> resultList)
		{
			if (!item.Name.EndsWith("等待验证"))
			{
				var deleResult = __DeletePassedItem(automation, item, token, options);
				return deleResult;
			}

			item.Click();
			RandomWait.Wait(600, 1200);  //等候页面刷新
			token.ThrowIfCancellationRequested();
			var validButtonRetry = Retry.WhileNull(() => this._Client.MainWindow.FindFirstDescendant(cf => cf.ByName("前往验证").And(cf.ByControlType(ControlType.Button).And(cf.ByClassName("mmui::XOutlineButton")))));
			if (validButtonRetry.Success)
			{
				var validButton = validButtonRetry.Result.AsButton();
				var root = validButton.GetParent();
				(bool flowControl, bool value, string keyword) = __CheckKeyword__(options, root);  //关键词检查，控制是否继续往下走
				if (!flowControl)
				{
					return value;
				}
				//通过关键词检查后操作
				validButton.ClickEnhance(this._Client.MainWindow);
				RandomWait.Wait(300, 600);
				var windowRetry = Retry.WhileNull(() => automation.GetDesktop().FindFirstChild(cf => cf.ByControlType(ControlType.Window).And(cf.ByClassName("mmui::VerifyFriendWindow").And(cf.ByName("通过朋友验证")).And(cf.ByProcessId(this._Client.MainWindow.Properties.ProcessId)))),
				timeout: TimeSpan.FromSeconds(2), interval: TimeSpan.FromMilliseconds(200));
				if (windowRetry.Success)
				{
					var win = windowRetry.Result.AsWindow();  //通过朋友验证窗口
					win.Move(this._Client.MainWindow.BoundingRectangle.X + (this._Client.MainWindow.BoundingRectangle.Width - win.BoundingRectangle.Width) / 2, this._Client.MainWindow.BoundingRectangle.Y + (this._Client.MainWindow.BoundingRectangle.Height - win.BoundingRectangle.Height) / 2);
					try
					{
						var passedFriendRoot = win.FindFirstDescendant(cf => cf.ByClassName("QWidget").And(cf.ByAutomationId("qt_scrollarea_viewport").Or(cf.ByAutomationId("GradientMaskScrollView.gradient_mask_stacked_view.default_scroll_area.qt_scrollarea_viewport"))).And(cf.ByControlType(ControlType.Group)));
						if (passedFriendRoot == null)
							return false;
						var memoItem = passedFriendRoot.FindFirstDescendant(cf => cf.ByControlType(ControlType.Edit).And(cf.ByName("修改备注").And(cf.ByClassName("mmui::XLineEdit"))));
						__ProcessMemoItem(win, memoItem, token, options.GetSuffix(keyword));
						RandomWait.Wait(800, 1200);
						var lableItem = passedFriendRoot.FindFirstDescendant(cf => cf.ByControlType(ControlType.Button).And(cf.ByName("修改标签")).And(cf.ByAutomationId("button")));
						__ProcessLabelItem(lableItem, token, automation, win, options.GetLabel(keyword));
						RandomWait.Wait(1200, 3000);
						__ProcessOk(passedFriendRoot, win, token, options, resultList, keyword);
						//其实上面已经关闭了win,但是为了保险，再检查一遍.
						win = automation.GetDesktop().FindFirstChild(cf => cf.ByControlType(ControlType.Window).And(cf.ByClassName("mmui::VerifyFriendWindow").And(cf.ByName("通过朋友验证")).And(cf.ByProcessId(this._Client.MainWindow.Properties.ProcessId)))).AsWindow();
						if (win != null)
							win.Close();
					}
					catch (Exception ex)
					{
						_logger.Error($"通过好友申请时发生错误，错误原因:{ex.ToString()}");
						win?.Close();
					}
				}

				return true;
			}
			return false;
		}

		private static (bool flowControl, bool value, string keyword) __CheckKeyword__(FriendRequestAutoAcceptOptions options, AutomationElement root)
		{
			var kw = new List<string>();
			if (options.KeyWord != null)
				kw.AddRange(options.KeyWord.Where(item => !string.IsNullOrWhiteSpace(item)).Select(item => item.Trim()));
			if (options.KeywordRules != null)
				kw.AddRange(options.KeywordRules.Keys.Where(item => !string.IsNullOrWhiteSpace(item)).Select(item => item.Trim()));
			var currentKeyword = "";
			if (kw.Count > 0)  //如果设定关键词，检查是否包含关键词
			{
				var textGroup = root.FindFirstDescendant(cf => cf.ByControlType(ControlType.Group).And(cf.ByAutomationId("qt_scrollarea_viewport").And(cf.ByClassName("QWidget"))));
				if (textGroup == null)
					return (flowControl: false, value: false, "");
				var texts = textGroup.FindAllChildren(cf => cf.ByControlType(ControlType.Text));
				if (texts.Length == 0)
					return (flowControl: false, value: false, "");
				var checkTag = false;
				foreach (var text in texts)
				{
					foreach (var item in kw)
					{
						if (text.Name.ToUpper().Contains(item.ToUpper()))
						{
							checkTag = true;
							currentKeyword = item;
							break;
						}
					}
					if (checkTag)
					{
						break;
					}
				}
				if (!checkTag)
					return (flowControl: false, value: false, "");
			}

			return (flowControl: true, value: default, currentKeyword);
		}

		private bool __DeletePassedItem(UIA3Automation automation, AutomationElement el, CancellationToken token, FriendRequestAutoAcceptOptions options)
		{
			if (!options.PassedDelete)
				return false;

			var root = Root;
			if (root == null)
				return false;
			token.ThrowIfCancellationRequested();
			if (el.BoundingRectangle.Y >= root.BoundingRectangle.Y && el.BoundingRectangle.Y + el.BoundingRectangle.Height <= root.BoundingRectangle.Y + root.BoundingRectangle.Height)
			{
				//删除
				var point = el.BoundingRectangle.Center();
				Mouse.Position = new System.Drawing.Point((int)point.X + Random.Shared.Next(-10, 10), (int)point.Y);
				RandomWait.Wait(300, 800);
				Mouse.Click();
				RandomWait.Wait(800, 2000);
				var point2 = new System.Drawing.Point((int)point.X + Random.Shared.Next(-10, 10), (int)point.Y);
				Mouse.MoveTo(point2);
				RandomWait.Wait(300, 800);
				Mouse.RightClick();
				token.ThrowIfCancellationRequested();
				RandomWait.Wait(800, 2000);

				var win = Retry.WhileNull(() => this._Client.MainWindow.FindFirstChild(cf => cf.ByName("Weixin").And(cf.ByClassName("mmui::XMenu"))),
					timeout: TimeSpan.FromSeconds(2), interval: TimeSpan.FromMilliseconds(200));
				if (win.Success)
				{
					var menu = win.Result.FindFirstChild(cf => cf.ByName("删除").And(cf.ByAutomationId("XMenuItem")));
					point = menu.BoundingRectangle.Center();
					point2 = new System.Drawing.Point((int)point.X + Random.Shared.Next(-10, 10), (int)point.Y);
					Mouse.MoveTo(point2);
					RandomWait.Wait(300, 800);
					menu?.Click();
					RandomWait.Wait(800, 2000);

					return true;
				}
			}

			return false;
		}

		private void __ProcessOk(AutomationElement passedFriendRoot, FlaUI.Core.AutomationElements.Window win, CancellationToken token, FriendRequestAutoAcceptOptions options, List<NewFriendBackItem> resultList, string keyword)
		{
			//点击确定按钮
			var button = win.FindFirstDescendant(cf => cf.ByControlType(ControlType.Button).And(cf.ByName("确定")).And(cf.ByClassName("mmui::XOutlineButton")));
			win.Focus();
			var point = button.BoundingRectangle.SafeRandomPoint();
			Mouse.Click(point);
			//获取wxid，保存进缓存
			RandomWait.Wait(1000, 1500);
			token.ThrowIfCancellationRequested();
			var wxidLabelRetry = Retry.WhileNull(() => this._Client.MainWindow.FindFirstDescendant(cf => cf.ByName("微信号：").And(cf.ByControlType(ControlType.Text)).And(cf.ByAutomationId("right_v_view.user_info_center_view.basic_line_view.basic_line.key_text"))), TimeSpan.FromSeconds(5), TimeSpan.FromMilliseconds(200));
			FriendInfo friendInfo = new FriendInfo();
			token.ThrowIfCancellationRequested();
			if (wxidLabelRetry.Success)
			{
				//wxid
				var label = wxidLabelRetry.Result;
				var wxidItem = label.GetSibling(1);
				if (wxidItem != null)
				{
					friendInfo.WxId = wxidItem.Name;
				}
				token.ThrowIfCancellationRequested();
				//昵称
				var parent = label.GetParent().GetSibling(-1);
				if (parent != null)
				{
					label = parent.FindFirstDescendant(cf => cf.ByAutomationId("right_v_view.user_info_center_view.basic_line_view.basic_line.key_text").And(cf.ByName("昵称：")));
					if (label != null)
					{
						var nickNameItem = label.GetSibling(1);
						if (nickNameItem != null)
						{
							friendInfo.NickName = nickNameItem.Name;
							friendInfo.MemoName = nickNameItem.Name; //暂时一致，后面会改变.
						}
					}
				}
				token.ThrowIfCancellationRequested();
				//地区
				parent = label.GetParent().GetParent();
				if (parent != null)
				{
					label = parent.FindFirstDescendant(cf => cf.ByAutomationId("right_v_view.user_info_center_view.basic_line_view.basic_line.key_text").And(cf.ByName("地区：")));
					if (label != null)
					{
						var areaItem = label.GetSibling(1);
						if (areaItem != null)
						{
							friendInfo.Area = areaItem.Name;
						}
					}
				}
				token.ThrowIfCancellationRequested();
				//共同群聊
				label = this._Client.MainWindow.FindFirstDescendant(cf => cf.ByName("共同群聊").And(cf.ByAutomationId("content_v_view.ProfileResizeVBoxView.detail_content_host.detail_center_v_view.detail_derived_content_view.section_shell.more_line_v_view.chatroom_intersection.key_text_h_view.key_text_view")).And(cf.ByClassName("mmui::XTextView")));

				if (label != null)
				{
					parent = label.GetParent().GetParent();
					var text = parent.FindFirstDescendant(cf => cf.ByAutomationId("content_v_view.ProfileResizeVBoxView.detail_content_host.detail_center_v_view.detail_derived_content_view.section_shell.more_line_v_view.chatroom_intersection.value_normal_view.content_view.value_reader_view.value_reader_").And(cf.ByControlType(ControlType.Text)).And(cf.ByClassName("mmui::ProfileTextView")));
					if (text != null)
					{
						friendInfo.SameGroupNumber = text.Name;
					}
				}
				token.ThrowIfCancellationRequested();
				//个性签名
				label = this._Client.MainWindow.FindFirstDescendant(cf => cf.ByName("个性签名").And(cf.ByAutomationId("content_v_view.ProfileResizeVBoxView.detail_content_host.detail_center_v_view.detail_derived_content_view.section_shell.more_line_v_view.sign.key_text_h_view.key_text_view").And(cf.ByControlType(ControlType.Text))));
				if (label != null)
				{
					parent = label.GetParent().GetParent();
					var text = parent.FindFirstDescendant(cf => cf.ByAutomationId("content_v_view.ProfileResizeVBoxView.detail_content_host.detail_center_v_view.detail_derived_content_view.section_shell.more_line_v_view.sign.value_normal_view.content_view.value_reader_view.value_reader_").And(cf.ByControlType(ControlType.Text)));
					if (text != null)
					{
						friendInfo.Signature = text.Name;
					}
				}
				token.ThrowIfCancellationRequested();
				//来源
				label = this._Client.MainWindow.FindFirstDescendant(cf => cf.ByName("来源").And(cf.ByAutomationId("content_v_view.ProfileResizeVBoxView.detail_content_host.detail_center_v_view.detail_derived_content_view.section_shell.more_line_v_view.source.key_text_h_view.key_text_view").And(cf.ByControlType(ControlType.Text))));
				if (label != null)
				{
					parent = label.GetParent().GetParent();
					var text = parent.FindFirstDescendant(cf => cf.ByAutomationId("content_v_view.ProfileResizeVBoxView.detail_content_host.detail_center_v_view.detail_derived_content_view.section_shell.more_line_v_view.source.value_normal_view.content_view.value_reader_view.value_reader_").And(cf.ByControlType(ControlType.Text)));
					if (text != null)
					{
						friendInfo.Source = text.Name;
					}
				}
				token.ThrowIfCancellationRequested();
				//添加时间
				label = this._Client.MainWindow.FindFirstDescendant(cf => cf.ByName("添加时间").And(cf.ByAutomationId("content_v_view.ProfileResizeVBoxView.detail_content_host.detail_center_v_view.detail_derived_content_view.section_shell.more_line_v_view.became_friend_time.key_text_h_view.key_text_view").And(cf.ByControlType(ControlType.Text))));
				if (label != null)
				{
					parent = label.GetParent().GetParent();
					var text = parent.FindFirstDescendant(cf => cf.ByAutomationId("content_v_view.ProfileResizeVBoxView.detail_content_host.detail_center_v_view.detail_derived_content_view.section_shell.more_line_v_view.became_friend_time.value_normal_view.content_view.value_reader_view.value_reader_").And(cf.ByControlType(ControlType.Text)));
					if (text != null)
					{
						friendInfo.AddDateTime = text.Name;
					}
				}
				token.ThrowIfCancellationRequested();
				//备注
				label = this._Client.MainWindow.FindFirstDescendant(cf => cf.ByName("备注").And(cf.ByAutomationId("content_v_view.ProfileResizeVBoxView.detail_content_host.detail_center_v_view.detail_derived_content_view.section_shell.main_line_v_view.remark_line.key_text_h_view.key_text_view").And(cf.ByControlType(ControlType.Text))));
				if (label != null)
				{
					parent = label.GetParent().GetParent();
					var pButton = parent.FindFirstDescendant(cf => cf.ByAutomationId("content_v_view.ProfileResizeVBoxView.detail_content_host.detail_center_v_view.detail_derived_content_view.section_shell.main_line_v_view.remark_line.value_remark_view").And(cf.ByControlType(ControlType.Button)));
					var text = pButton.FindFirstDescendant(cf => cf.ByAutomationId("content_v_view.ProfileResizeVBoxView.detail_content_host.detail_center_v_view.detail_derived_content_view.section_shell.main_line_v_view.remark_line.value_remark_view.content_view.ProfileTextView").And(cf.ByControlType(ControlType.Text)));
					if (text != null)
					{
						if (!string.IsNullOrWhiteSpace(text.Name))
						{
							friendInfo.MemoName = text.Name;
							if (string.IsNullOrWhiteSpace(friendInfo.NickName))
							{
								friendInfo.NickName = friendInfo.MemoName;
							}
						}

					}
				}
				token.ThrowIfCancellationRequested();
				//标签
				label = this._Client.MainWindow.FindFirstDescendant(cf => cf.ByName("标签").And(cf.ByAutomationId("content_v_view.ProfileResizeVBoxView.detail_content_host.detail_center_v_view.detail_derived_content_view.section_shell.main_line_v_view.tag_line.key_text_h_view.key_text_view").And(cf.ByControlType(ControlType.Text))));
				if (label != null)
				{
					parent = label.GetParent().GetParent();
					var text = parent.FindFirstDescendant(cf => cf.ByAutomationId("content_v_view.ProfileResizeVBoxView.detail_content_host.detail_center_v_view.detail_derived_content_view.section_shell.main_line_v_view.tag_line.value_normal_view.content_view.value_reader_view.value_reader_").And(cf.ByControlType(ControlType.Text)));
					//声音,nnn
					if (text != null)
					{
						var lables = text.Name.Split(",");
						friendInfo.Lable = lables.Select(u => u.Trim()).ToList();
					}
				}
				token.ThrowIfCancellationRequested();
				//图标
				label = this._Client.MainWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Button).And(cf.ByAutomationId("head_image_v_view.head_view_")).And(cf.ByClassName("mmui::ContactHeadView")));
				var path = Path.Combine(AppContext.BaseDirectory, "Avator", $"{friendInfo.WxId}.png");
				friendInfo.AvatarPath = path;
				label.CaptureToFile(path);
				RandomWait.Wait(300, 600);
				resultList.Add(new NewFriendBackItem()
				{
					Who = friendInfo.MemoName,
					FromKeyword = keyword,
				});
				//保存进缓存文件
				__SaveToCacheFile(friendInfo);
			}
		}

		private void __ProcessLabelItem(AutomationElement lableItem, CancellationToken token, UIA3Automation automation, FlaUI.Core.AutomationElements.Window win, string label)
		{
			if (string.IsNullOrWhiteSpace(label))
				return;
			token.ThrowIfCancellationRequested();
			var point = lableItem.BoundingRectangle.SafeRandomPoint();
			Mouse.MoveTo(point);
			RandomWait.Wait(100, 600);
			Mouse.LeftClick();
			RandomWait.Wait(1000, 3000);
			var winResult = Retry.WhileNull(() => win.FindFirstChild(cf => cf.ByControlType(ControlType.Window).And(cf.ByClassName("mmui::LabelPopover")).And(cf.ByName("Weixin"))), TimeSpan.FromSeconds(2), TimeSpan.FromMilliseconds(200));
			if (winResult.Success)
			{
				var window = winResult.Result;
				//标签名可能已经存在，或者不存在，需要新建.
				var list = window.FindFirstDescendant(cf => cf.ByControlType(ControlType.List).And(cf.ByClassName("mmui::XTableView")).And(cf.ByName("标签"))).AsListBox();
				var items = list.FindAllChildren(cf => cf.ByControlType(ControlType.CheckBox));
				if (items.Select(x => x.Name).Where(x => x.Equals(label)).Count() > 0)
				{
					//已经有标签
					var selectItem = items.FirstOrDefault(x => x.Name.Equals(label));
					if (selectItem != null)
					{
						var point2 = selectItem.BoundingRectangle.SafeRandomPoint();
						Mouse.MoveTo(point2);
						Mouse.LeftClick();
						RandomWait.Wait(300, 900);
					}
				}
				else
				{
					//无标签，需要新建
					var searchEdit = win.FindFirstDescendant(cf => cf.ByControlType(ControlType.Edit).And(cf.ByName("搜索")).And(cf.ByClassName("mmui::XValidatorTextEdit")));
					if (searchEdit != null)
					{
						searchEdit.Focus();
						var point2 = searchEdit.BoundingRectangle.SafeRandomPoint();
						Mouse.Click(point2);
						ClipboardHelper.SetText(label);
						Keyboard.TypeSimultaneously(VirtualKeyShort.CONTROL, VirtualKeyShort.KEY_V);
						RandomWait.Wait(300, 900);
						list = window.FindFirstDescendant(cf => cf.ByControlType(ControlType.List).And(cf.ByClassName("mmui::XTableView")).And(cf.ByName("标签"))).AsListBox();
						var createItemRetry = Retry.WhileNull(() => list.Items.Where(u => u.Name.Contains("创建新标签") && u.ControlType == ControlType.ListItem).FirstOrDefault(),
						timeout: TimeSpan.FromSeconds(2), interval: TimeSpan.FromMilliseconds(200));
						if (createItemRetry != null)
						{
							var createItem = createItemRetry.Result;
							point2 = createItem.BoundingRectangle.SafeRandomPoint();
							Mouse.Click(point2);
							RandomWait.Wait(300, 900);
						}
					}
				}
				var randomRetry = Random.Shared.Next(1, 10);
				if (randomRetry <= 5)
				{
					//点击备注栏
					var clkItem = win.FindFirstDescendant(cf => cf.ByControlType(ControlType.Edit).And(cf.ByName("修改备注").And(cf.ByClassName("mmui::XLineEdit"))));
					clkItem.Click();
				}
				else
				{
					//点击“确定上面一点”
					var button = win.FindFirstDescendant(cf => cf.ByControlType(ControlType.Button).And(cf.ByName("确定")).And(cf.ByClassName("mmui::XOutlineButton")));
					if (button != null)
					{
						var buttonRect = button.BoundingRectangle;
						var point2 = new Rectangle(buttonRect.X, buttonRect.Y - 200, buttonRect.Width, 200 - 50).SafeRandomPoint();
						Mouse.MoveTo(point2);
						Mouse.Click();
					}
				}
			}
		}

		private void __ProcessMemoItem(FlaUI.Core.AutomationElements.Window window, AutomationElement memoItem, CancellationToken token, string suffix)
		{
			token.ThrowIfCancellationRequested();
			//首先获取到旧备注名
			var oriName = memoItem.GetParent().Name;
			if (oriName.Trim().Length != oriName.Length)
			{
				oriName = oriName.Trim();
			}
			if (string.IsNullOrWhiteSpace(oriName))
			{
				oriName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName());  //如果为空，则得到一个随机名称
			}
			//检查是否与cache中的名称重复，如果重复，则以xxx_1,2,3的形式增加
			var cacheFile = Path.Combine(AppContext.BaseDirectory, $"{this._Client.Alias}_cache.dat");
			if (File.Exists(cacheFile))
			{
				oriName = _ProcessCacheSameName(cacheFile, oriName);
			}
			if (!string.IsNullOrWhiteSpace(suffix))
			{
				if (!oriName.EndsWith($"_{suffix}"))
				{
					oriName = oriName + $"_{suffix}";
				}
			}
			window.Focus();
			var point = memoItem.BoundingRectangle.SafeRandomPoint();
			Mouse.Position = point;
			Mouse.LeftClick();
			RandomWait.Wait(100, 400);
			Keyboard.TypeSimultaneously(VirtualKeyShort.CONTROL, VirtualKeyShort.KEY_A);
			RandomWait.Wait(300, 800);
			Keyboard.TypeSimultaneously(VirtualKeyShort.BACK);
			RandomWait.Wait(600, 1500);
			ClipboardHelper.SetText(oriName);
			Keyboard.TypeSimultaneously(VirtualKeyShort.CONTROL, VirtualKeyShort.KEY_V);
			RandomWait.Wait(300, 800);
			var randValue = Random.Shared.Next(1, 10);
			if (randValue > 5)
				Keyboard.TypeSimultaneously(VirtualKeyShort.RETURN);
			RandomWait.Wait(500, 1500);
		}

		private void __SaveToCacheFile(FriendInfo friendInfo)
		{
			var cacheFile = Path.Combine(AppContext.BaseDirectory, $"{this._Client.Alias}_cache.dat");
			if (File.Exists(cacheFile))
			{
				byte[] bytes = File.ReadAllBytes(cacheFile);
				var lt = MessagePackSerializer.Deserialize<List<FriendInfo>>(bytes);
				if (lt.Select(x => x.WxId).ToList().Contains(friendInfo.WxId))
				{
					//有微信id相当，应该只是修改
					var item = lt.Find(x => x.WxId.Equals(friendInfo.WxId));
					item.MemoName = friendInfo.MemoName;
					item.Lable = friendInfo.Lable;
					item.SameGroupNumber = friendInfo.SameGroupNumber;
					item.Signature = friendInfo.Signature;
					item.Source = friendInfo.Source;
					item.Area = friendInfo.Area;
				}
				else
				{
					lt.Add(friendInfo);
				}
				bytes = MessagePackSerializer.Serialize<List<FriendInfo>>(lt);
				File.WriteAllBytes(cacheFile, bytes);
				RandomWait.Wait(100, 600);
			}
		}

		private string _ProcessCacheSameName(string cacheFile, string name)
		{
			byte[] bytes = File.ReadAllBytes(cacheFile);
			var lt = MessagePackSerializer.Deserialize<List<FriendInfo>>(bytes);
			if (lt.Select(x => x.MemoName).ToList().Contains(name))
			{
				var count = lt.Select(x => x.MemoName).Where(x => x.StartsWith(name)).Count();
				name = name + $"_{count}";
			}
			return name;
		}

		private bool _CanScroll()
		{
			var root = Root;
			var tmpList = root.Items.Where(item => item.ClassName.Equals("mmui::ContactsCellGroupView") && !item.Name.Equals("新的朋友"));
			if (tmpList.Count() > 0)
			{
				return false;
			}
			return true;
		}

		private (bool success, AutomationElement el, bool click) _TryGetNextItem(ListBoxItem item)
		{
			var rItem = item.GetSibling(1);
			if (rItem.ClassName.Equals("mmui::ContactsCellGroupView") || (!rItem.ClassName.Equals("mmui::XTableCell")))
			{
				return (false, null, false);
			}
			if (rItem == null)
			{
				return (true, null, false);
			}
			var name = rItem.Name;
			if (name.EndsWith("等待验证"))
			{
				return (true, rItem, true);
			}
			return (true, rItem, false);
		}

		/// <summary>
		/// 移除好友
		/// 注意： 如果删除好友，从通讯录删除好友后，同步的，如果此好友处在监听中，应该将监听中的好友删除
		/// </summary>
		/// <param name="nickName">好友昵称,可以为空，如果为空，则将焦点窗口的好友删除</param>
		/// <returns>是否成功</returns>
		public async Task<bool> RemoveFriend(string nickName)
		{
			if (!string.IsNullOrWhiteSpace(nickName))
			{
				var result = await _Client.SearchFriend(nickName);
				if (!result)
					return false;
			}
			var title = await _Client.GetTitle();
			if (!title.CanTalk())
				return false;
			if (title.HeaderType != ChatType.好友 && title.HeaderType != ChatType.企业微信)
			{
				return false;
			}
			return await WeChatInvoker.Call(RemoveFriendCore);
		}

		internal bool RemoveFriendCore(UIA3Automation automation)
		{
			_Client.OuterGroup.ClickChatInfoButton();
			var result = ClickPersion(automation);
			if (!result.Success) return result.Success;
			result = ClickMoreButton(automation);
			if (!result.Success) return result.Success;
			result = ClickDeleteButton(automation);
			if (!result.Success) return result.Success;
			result = ClickConfirmButton(automation);
			if (!result.Success) return result.Success;

			// 关闭聊天信息窗口
			_Client.OuterGroup.ClickChatInfoButton();

			//由于删除后显示一片空白，

			return result.Success;
		}

		private Result ClickConfirmButton(UIA3Automation automation)
		{
			var path = "/Window[@Name='Weixin']/Window[@Name='Weixin']/Group/Group/Group/Button[@Name='删除']";
			var confirmRetry = Retry.WhileNull(() => automation.GetDesktop().FindFirstByXPath(path), TimeSpan.FromSeconds(2), TimeSpan.FromMilliseconds(200));
			if (!confirmRetry.Success)
				return Result.Fail("错误： 没有发现 删除 按钮");
			var confirmButton = confirmRetry.Result;
			confirmButton.Click();
			RandomWait.Wait(300, 1200);
			return Result.Ok();
		}

		private Result ClickDeleteButton(UIA3Automation automation)
		{
			var path = "/Window[@Name='Weixin']/Window[@Name='Weixin']/MenuItem[@Name='删除联系人']";
			var menuItemRetry = Retry.WhileNull(() => automation.GetDesktop().FindFirstByXPath(path), TimeSpan.FromSeconds(2), TimeSpan.FromMilliseconds(200));
			if (!menuItemRetry.Success)
				return Result.Fail("错误：没有发现 删除联系人 菜单");
			var menuItem = menuItemRetry.Result;
			menuItem.Click();
			return Result.Ok();
		}

		private Result ClickMoreButton(UIA3Automation automation)
		{
			var path = "/Window[@Name='Weixin']/Group/Group/Group/Group/Group/Group/Group/Button[@Name='更多']";
			var buttonRetry = Retry.WhileNull(() => automation.GetDesktop().FindFirstByXPath(path), TimeSpan.FromSeconds(2), TimeSpan.FromMilliseconds(200));
			if (!buttonRetry.Success) return Result.Fail("错误：没有找到 更多 按钮");
			var button = buttonRetry.Result;
			button.Click();
			RandomWait.Wait(300, 1200);
			return Result.Ok();
		}

		private Result ClickPersion(UIA3Automation automation)
		{
			var path = "/Group/Custom/Group/Group/Group/Custom/Custom/Custom/Group/Custom/Custom/Group/Group/Group[@AutomationId='single_chat_info_view']/Group/Group[@AutomationId='qt_scrollarea_viewport']";
			var paneRootRetry = Retry.WhileNull(() => this._Client.MainWindow.FindFirstByXPath(path), TimeSpan.FromSeconds(2), TimeSpan.FromMilliseconds(100));
			if (!paneRootRetry.Success)
				return Result.Fail("没有找到根路径!");
			var paneRoot = paneRootRetry.Result;
			var point = this._Client.OcrEngee.OCRVerticalDetect(paneRoot, 0.5f, "添加");
			if (point.IsEmpty)
				return Result.Fail("错误: OCR 添加 按钮失败!");
			this._Client.MainWindow.Focus();
			Mouse.Position = paneRoot.BoundingRectangle.Center();
			RandomWait.Wait(600, 1200);
			var point2 = new Point(point.X, point.Y - 30).Confusion(10, 5);
			SupperMouseKey.MoveTo(point2);
			RandomWait.Wait(300, 1200);
			point2 = new Point(point2.X - 65, point2.Y);
			RandomWait.Wait(300, 600);
			SupperMouseKey.MoveTo(point2);
			RandomWait.Wait(300, 1200);
			SupperMouseKey.LeftClick();
			RandomWait.Wait(800, 1500);
			return Result.Ok();
		}
	}

}