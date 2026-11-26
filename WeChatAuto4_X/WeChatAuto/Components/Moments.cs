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
using Emgu.CV.Dai;
using System.Windows.Controls;
using Emgu.CV.Structure;
using WeChatAuto.Services;
using Emgu.CV;
using System.Windows.Media.Imaging;
using System.ComponentModel.DataAnnotations;


namespace WeChatAuto.Components
{
	/// <summary>
	/// 朋友圈操作
	/// </summary>
	public partial class Moments
	{
		private readonly IServiceProvider _serviceProvider;
		private UIThreadInvoker _uiMainThreadInvoker;
		private AutoLogger<Moments> _logger;
		private WeChatClient _Client;

		internal Moments(WeChatClient client, UIThreadInvoker uiThreadInvoker, IServiceProvider serviceProvider)
		{
			_logger = serviceProvider.GetRequiredService<AutoLogger<Moments>>();
			_uiMainThreadInvoker = uiThreadInvoker;
			_Client = client;
			_serviceProvider = serviceProvider;
		}

		/// <summary>
		/// 静默刷新朋友圈.
		/// </summary>
		/// <returns></returns>
		public async Task<bool> RefeshSnsList()
		{
			return await WeChatInvoker.Call(RefreshSnsListCore);
		}

		/// <summary>
		/// 刷新朋友圈核心方法
		/// </summary>
		/// <param name="automation"></param>
		/// <returns></returns>
		private bool RefreshSnsListCore(UIA3Automation automation)
		{
			var result = IsOpenMomentsWin(automation);
			if (!result.success)
			{
				var win = OpenMomentsCore(automation);
				if (win != null)
				{
					RandomWait.Wait(2000, 5000);
					return true;
				}
			}
			result = IsOpenMomentsWin(automation);
			if (!result.success)
			{
				_logger.Error("错误：朋友圈窗口没有打开，可能UI Tree发生错误，请联系作者！");
				return false;
			}
			var snsWin = result.win;
			var refreshButtonRetry = Retry.WhileNull(() => snsWin.FindFirstByXPath(UITreeGlobal.Sns_Refresh_button), TimeSpan.FromSeconds(2), TimeSpan.FromMilliseconds(200));
			if (refreshButtonRetry.Success)
			{
				snsWin.Focus();
				var button = refreshButtonRetry.Result.AsButton();
				button.Click();
				this._Client.MainWindow.Focus();
				RandomWait.Wait(2000, 5000);
				return true;
			}
			_logger.Error("错误：朋友圈窗口没有打开，可能UI Tree发生错误，请联系作者！");
			return false;
		}


		internal AutomationElement GetToolBar(UIA3Automation automation)
		{
			var window = OpenMomentsCore(automation);
			if (window == null)
				return null;
			var toolBarRetry = Retry.WhileNull(() => window.FindFirstByXPath(UITreeGlobal.Sns_Toolbar_path), TimeSpan.FromSeconds(2), TimeSpan.FromMilliseconds(200));
			return toolBarRetry.Success ? toolBarRetry.Result : null;
		}

		/// <summary>
		/// 打开朋友圈,如果未打开，则打开朋友圈，如果已经打开了，则窗口提前
		/// </summary>
		/// <returns></returns>
		public async Task<Window> OpenMoments()
		{
			return await WeChatInvoker.Call(OpenMomentsCore);
		}

		internal Window OpenMomentsCore(UIA3Automation automation)
		{
			(bool success, Window win) result = IsOpenMomentsWin(automation);
			if (!result.success)
			{
				this._Client.Navigation.SwitchNavigationCore(automation, NavigationType.朋友圈);
				RandomWait.Wait(600, 1200);
				result = IsOpenMomentsWin(automation);
				if (result.success)
				{
					this._Client.MoveWinToMainCenter(result.win);
					RandomWait.Wait(500,2000);
					this._Client.Navigation.SwitchNavigationCore(automation, NavigationType.微信);
					return result.win;
				}
			}
			else
			{
				this._Client.MoveWinToMainCenter(result.win);
				return result.win;
			}
			return null;
		}

		/// <summary>
		/// 是否打开朋友圈
		/// </summary>
		/// <param name="automation"></param>
		/// <returns></returns>
		internal (bool success, Window win) IsOpenMomentsWin(UIA3Automation automation)
		{
			var desktop = automation.GetDesktop();
			var winRetry = Retry.WhileNull(() => desktop.FindFirstChild(cf => cf.ByProcessId(this._Client.MainWindow.Properties.ProcessId).And(cf.ByName("朋友圈")).And(cf.ByAutomationId("SNSWindow"))), TimeSpan.FromSeconds(2), TimeSpan.FromMilliseconds(200));
			return (winRetry.Success, winRetry.Result.AsWindow());
		}
		/// <summary>
		/// 关闭朋友圈
		/// </summary>
		/// <returns></returns>
		public async Task CloseMoments()
		{
			await WeChatInvoker.Call(CloseMomentsCore);
		}

		internal void CloseMomentsCore(UIA3Automation automation)
		{
			(bool success, Window win) result = IsOpenMomentsWin(automation);
			if (result.success)
			{
				result.win.Focus();
				result.win.Close();
				RandomWait.Wait(600, 900);
			}
		}
	}
}