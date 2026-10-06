from __future__ import annotations

import asyncio
import logging
from datetime import date, time
from typing import TYPE_CHECKING

import pytest
from wechat_auto_sdk import (
    FriendRequestAutoAcceptOptions,
    MomentsOptions,
    NavigationType,
    NewFriendBackItem,
)
from wechat_auto_sdk.models.message_context import MessageContext
from wechat_auto_sdk.models.time_only_range import TimeOnlyRange

if TYPE_CHECKING:
    from wechat_auto_sdk import SystemMessageContext, WeChatClient

logger = logging.getLogger(__name__)


@pytest.mark.asyncio
async def test_get_ower_info(client: WeChatClient):
    """
    获取本微信的个人信息，包括头像文件位置，wxid,昵称
    """
    info = await client.get_ower_info()
    logger.info("user info=%s", info)
    assert info is not None


@pytest.mark.asyncio
async def test_max_window(client: WeChatClient):
    """
    最大化微信窗口
    """
    await client.max_window()


@pytest.mark.asyncio
async def test_restore_window(client: WeChatClient):
    """
    还原微信窗口
    """
    await client.restore_window()


@pytest.mark.asyncio
async def test_pinned_window(client: WeChatClient):
    """
    置顶微信窗口
    """
    await client.pinned_window()


@pytest.mark.asyncio
async def test_unpined_widnow(client: WeChatClient):
    """
    取消置顶微信窗口
    """
    await client.unpined_widnow()


@pytest.mark.asyncio
async def test_focus_window(client: WeChatClient):
    """
    使主窗口获取焦点
    """
    await client.focus_window()


@pytest.mark.asyncio
async def test_close_search_window(client: WeChatClient):
    """
    关闭查询窗口,如果查询窗口打开则关闭，如果查询窗口没有打开，则不作动作
    """
    await client.close_search_window("秋歌")
    assert True


@pytest.mark.asyncio
async def test_open_subWin_1(client: WeChatClient):
    """
    打开who指定的子窗口
    """
    await client.open_subWin("秋歌")


@pytest.mark.asyncio
async def test_open_subWin_2(client: WeChatClient):
    """
    打开who指定的子窗口
    """
    await client.open_subWin("前端攻城狮")


@pytest.mark.asyncio
async def test_get_owner_window_handler(client: WeChatClient):
    """
    得到本微信窗口句柄
    """
    handler = await client.get_owner_window_handler()
    print(f"本微信[{client.from_wechat}]的句柄为：{handler}")


@pytest.mark.asyncio
async def test_get_owner_window_process_id(client: WeChatClient):
    """
    得到本微信窗口的进程id
    """
    process_id = await client.get_owner_window_process_id()
    print(f"本微信[{client.from_wechat}]的process_id为：{process_id}")


@pytest.mark.asyncio
async def test_switch_navigation(client: WeChatClient):
    """
    切换导航栏
    """
    await client.switch_navigation(navigationType=NavigationType.通讯录)
    await asyncio.sleep(2)
    await client.switch_navigation(NavigationType.微信)


@pytest.mark.asyncio
async def test_close_navWin(client: WeChatClient):
    """
    关闭通过导航栏打开的窗口.
    """
    await client.switch_navigation(navigationType=NavigationType.朋友圈)
    await asyncio.sleep(2)
    await client.close_navWin(NavigationType.朋友圈)


@pytest.mark.asyncio
async def test_click_motify_icon(client: WeChatClient):
    """
    点击任务栏微信图标
    """
    await client.click_motify_icon(1)


@pytest.mark.asyncio
async def test_click_motify_icon_name(client: WeChatClient):
    """
    点击指定微信名称的任务栏图标
    """
    await client.click_motify_icon_name(client.from_wechat)


@pytest.mark.asyncio
async def test_get_all_conversations(client: WeChatClient):
    """获取会话列表所有会话的标题
    考虑到效率，只返回名称列表

    Returns:
        list[str]: 返回会话标题名称列表
    """
    list = await client.get_all_conversations()
    assert len(list) > 0


@pytest.mark.asyncio
async def test_get_visible_conversation_titles(client: WeChatClient):
    """获取会话列表可见会话标题

    Returns:
        list[str]: 可见的会话列表的标题列表
    """
    list = await client.get_visible_conversation_titles()
    print(list)
    assert len(list) > 0


@pytest.mark.asyncio
async def test_get_visible_conversations(client: WeChatClient):
    """获取可见会话列表
    会话信息包含：会话名称、会话未读消息数、会话头像等具体信息

    Returns:
        list[SimpleConversation]: 返回<see cref="Conversation"/>列表
    """
    list = await client.get_visible_conversations()
    print(list)
    assert len(list) > 0


@pytest.mark.asyncio
async def test_search_friend(client: WeChatClient):
    """搜索好友/群聊

    Args:
        who (str): 待搜索的好友/群聊昵称,who - 微信会话列表肉眼可见的名称,如果群有备注，则这个who即为备注名

    Returns:
        bool: 如果找到，返回true,如果没有找到，则返回false.
    """
    result = await client.search_friend("师父")
    assert result
    await asyncio.sleep(2)
    result = await client.search_friend("女女")
    assert result
    await asyncio.sleep(2)
    result = await client.search_friend("测试xxxxxx")
    assert not result


@pytest.mark.asyncio
async def test_locate_conversation(client: WeChatClient):
    """定位会话
    定位会话的用途：可以将会话列表滚动到指定会话的位置，使指定会话可见

    Args:
        who (str): 会话标题

    Returns:
        bool: 如果找到会话，则返回true，否则返回false
    """
    result = await client.locate_conversation("一")
    assert result
    result = await client.locate_conversation("xxxxx")
    assert not result


@pytest.mark.asyncio
async def test_set_do_not_disturb(client: WeChatClient):
    """设置会话消息免打扰

    Args:
        who (str): 要设置的 好友/群聊 名称,可以为空,如果为空，则为当前窗口设置免打扰
        setting (bool): 如果为:true,则设置会话消息免打扰，如果为:false,则：允许消息通知

    Returns:
        bool: 执行消息免打扰结果
    """
    result = await client.set_do_not_disturb("秋歌", True)
    assert result
    await asyncio.sleep(2)
    result = await client.set_do_not_disturb("秋歌", False)
    assert result


@pytest.mark.asyncio
async def test_set_top_most(client: WeChatClient):
    """设置会话置顶

    Args:
        who (str): true:聊天置顶;false:取消聊天置顶
        setting (bool): 要设置的 好友/群聊 名称,可以为空,如果为空，则为当前窗口设置置顶

    Returns:
        bool: 执行会话置顶结果
    """
    result = await client.set_top_most("秋歌", True)
    assert result
    await asyncio.sleep(2)
    result = await client.set_top_most("秋歌", False)
    assert result


@pytest.mark.asyncio
async def test_get_title(client: WeChatClient):
    """获取当前聊天窗口的标题对象

    Returns:
        HeaderInfo: 标题对象
    """
    result = await client.get_title()
    print(result)
    assert result is not None


@pytest.mark.asyncio
async def test_get_only_title(client: WeChatClient):
    """获取当前聊天窗口的标题对象

    Returns:
        HeaderInfo: 标题对象
    """
    result = await client.get_only_title()
    print(result)
    assert result != ""


@pytest.mark.asyncio
async def test_send_message(client: WeChatClient):
    """
    发送文本消息,可以是群聊名称或者好友名称，名称可以为空，如果为空，则给当前聊天窗口发送消息

    Args:
        who (str): 好名/群聊的名称,也就是肉眼所见的标题
        message (str): 消息内容，文本消息内容
        at_user (str | list[str] | None, optional): 被@的好友,可以一个，也可以多个
        chat_refer (ChatRefer | None, optional): 引用的对话内容,请参考<see cref="ChatRefer"/>
    """
    await client.send_message(
        "DroidMirror官方技术支持", "hello world!", at_user=["AI.Net_test", "智影工坊"]
    )


@pytest.mark.asyncio
async def test_send_file(client: WeChatClient):
    """
    发送文件
    """
    with pytest.raises(Exception):
        await client.send_file("AI.Net_test", [])
    with pytest.raises(FileNotFoundError):
        await client.send_file("AI.Net_test", ["xxxx", "xxx"])
    # 发送图片
    await client.send_file(
        "AI.Net_test", ["C:\\Users\\Administrator\\Desktop\\me\\1.png"]
    )
    # 发送文档
    await asyncio.sleep(2)
    await client.send_file(
        "AI.Net_test",
        [
            "C:\\Users\\Administrator\\Desktop\\me\\1.png",
            "C:\\Users\\Administrator\\Desktop\\me\\2024年膳中膳商业计划书222.pdf",
        ],
    )


@pytest.mark.asyncio
async def test_send_emoji(client: WeChatClient):
    """
    发送表情
    """
    await client.send_emoji("AI.Net_test", 1)
    await asyncio.sleep(2)
    await client.send_emoji("", "微笑")


@pytest.mark.asyncio
async def test_send_voice_chat(client: WeChatClient):
    """
    发起单人语音聊天
    """
    await client.send_voice_chat("AI.Net_test")


@pytest.mark.asyncio
async def test_send_vedio_chat(client: WeChatClient):
    """
    发起单人视频聊天
    """
    await client.send_vedio_chat("AI.Net_test")


@pytest.mark.asyncio
async def test_send_voice_chats(client: WeChatClient):
    """
    发起多人语音聊天，适用于群聊发起语音聊天
    """
    await client.send_voice_chats(
        "DroidMirror官方技术支持", ["AI.Net_test", "智影工坊"]
    )


@pytest.mark.asyncio
async def test_send_voice_message(client: WeChatClient):
    """发送语音消息,此功能依赖虚拟声卡：Cable input/Cable output
    请在声音-->设置-->将输入设备改成: Cable output
    如果没有安装虚拟声卡，请在:https://github.com/alexzhao189/wechatautosdk/blob/main/Resources/VBCABLE_Driver_Pack45.zip下载

    Args:
        who (str): 好友昵称或群聊名称,可以为空，如果为空，则给焦点聊天窗口发送语音消息
        file_path (str): 语音文件路径
    """
    await client.send_voice_message(
        "AI.Net_test",
        "D:\\repo\\WeChatAuto.SDK\\WeChatAuto.SDK\\src\\WeChatAuto4_X\\WeChatAuto.Tests\\Assets\\littlecat.wav",
    )


@pytest.mark.asyncio
async def test_get_chatHistory(client: WeChatClient):
    """
    根据日期获取聊天历史
    """
    list = await client.get_chatHistory(fetch_date=date.today())
    await asyncio.sleep(2)
    print(list)
    assert len(list) > 0
    list = await client.get_chatHistory("Admin.net官方", fetch_date=date.today())
    print(list)
    assert len(list) > 0


@pytest.mark.asyncio
async def test_tap_who(client: WeChatClient):
    """
    点击who指定的会话
    """
    result = await client.tap_who("AI.Net_test", 60)
    assert result
    result = await client.tap_who("Alex")
    assert not result


@pytest.mark.asyncio
async def test_forward_multiple_message(client: WeChatClient):
    """
    转发多条消息
    """
    result = await client.forward_multiple_message(
        "人工智能自动化技术讨论群", ["Alex", "AI.Net_test"]
    )
    assert result


@pytest.mark.asyncio
async def test_forward_single_message(client: WeChatClient):
    """
    转发单条消息
    """
    result = await client.forward_single_message(
        "矛", "@菜鸟 用刘亦菲的声音给我报一下现在的时间", ["Alex", "AI.Net_test"]
    )
    assert result


async def test_open_add_friens_win(client: WeChatClient):
    """
    打开新增朋友窗口 与 关闭新增朋友窗口
    """
    await client.open_add_friens_win()
    await asyncio.sleep(2)
    await client.close_add_friend_win()


@pytest.mark.asyncio
async def test_add_friends(client: WeChatClient):
    """
    通过手机号码、微信号查找并添加好友
    """
    list = await client.add_friends(
        ["18978694189", "13719238557", "13719238558", "18978194199"]
    )
    print(list)
    assert len(list) > 0


@pytest.mark.asyncio
async def test_is_owner_chat_group(client: WeChatClient):
    """
    是否是自有群
    """
    assert await client.is_owner_chat_group("DroidMirror官方技术支持")
    assert not await client.is_owner_chat_group("前端攻城狮")


@pytest.mark.asyncio
async def test_get_group_owner(client: WeChatClient):
    """
    获取群主
    """
    assert await client.get_group_owner("人工智能自动化技术讨论群") == "AI.Net_test"
    assert await client.get_group_owner("DroidMirror官方技术支持") == "Alex"


@pytest.mark.asyncio
async def test_add_owner_chat_group_member(client: WeChatClient):
    """
    添加群聊成员，适用于自有群
    """
    await client.add_owner_chat_group_member(
        "DroidMirror官方技术支持", ["AI.Net_test", "智影工坊"]
    )


@pytest.mark.asyncio
async def test_create_owner_chat_group(client: WeChatClient):
    """
    创建群聊,如果存在，则打开群聊，否则创建一个新群聊
    """
    assert await client.create_owner_chat_group(
        "测试dddd2", "AI.Net_test", ["智影工坊"]
    )


@pytest.mark.asyncio
async def test_change_owner_chat_group_name(client: WeChatClient):
    """
    修改群名，适用于自有群群名修改
    """
    assert await client.change_owner_chat_group_name("测试dddd2", "测试dddd3")


@pytest.mark.asyncio
async def test_change_chat_group_nick_name(client: WeChatClient):
    """
    修改自己在群中的昵称
    """
    assert await client.change_chat_group_nick_name(
        "DroidMirror官方技术支持", "Alex_test"
    )


@pytest.mark.asyncio
async def test_change_chat_group_memo(client: WeChatClient):
    """
    改变群备注,群备注仅自己可见.
    """
    assert await client.change_chat_group_memo(
        "DroidMirror官方技术支持", "DroidMirror官方技术支持_test"
    )
    await asyncio.sleep(2)
    # 删除群备注
    assert await client.change_chat_group_memo("", "")
    await asyncio.sleep(2)
    # 微信bug,再删除一次
    assert await client.change_chat_group_memo("", "")


@pytest.mark.asyncio
async def test_update_group_notice(client: WeChatClient):
    """
    更新群聊公告,仅适用于自有群
    """
    assert await client.update_group_notice("DroidMirror官方技术支持", "hello world!!")


@pytest.mark.asyncio
async def test_get_chat_group_member_list(client: WeChatClient):
    """
    获取群聊成员列表
    """
    list = await client.get_chat_group_member_list("人工智能自动化技术讨论群")
    assert len(list) > 0


@pytest.mark.asyncio
async def test_remove_owner_chat_group_member(client: WeChatClient):
    """
    移除群聊成员,适用于自有群
    """
    result = await client.remove_owner_chat_group_member(
        "DroidMirror官方技术支持", ["智影工坊"]
    )
    assert result


@pytest.mark.asyncio
async def test_quit_chat_group(client: WeChatClient):
    """
    退出群聊
    """
    # 建一个新群
    await client.create_owner_chat_group("测试退出群", "AI.Net_test", ["智影工坊"])
    # 退出群聊
    await asyncio.sleep(2)
    await client.quit_chat_group("测试退出群")


@pytest.mark.asyncio
async def test_invite_chat_group_member(client: WeChatClient):
    """
    邀请群聊成员,适用于外部群
    """
    result = await client.invite_chat_group_member(
        "人工智能自动化技术讨论群", ["khcgb_test"]
    )
    assert result


@pytest.mark.asyncio
async def test_add_chat_group_member_to_friends(client: WeChatClient):
    """
    添加群聊里面的好友为自己的好友,适用于从外部群中添加好友为自己的好友
    """
    result = await client.add_chat_group_member_to_friends(
        "实时AI快讯 5群", ["稲崎咲弥", "Amy", "杨善民"]
    )
    print(result)
    assert len(result) > 0


@pytest.mark.asyncio
async def test_get_all_friends(client: WeChatClient):
    """
    获取所有好友的信息列表
    """
    result = await client.get_all_friends()
    assert len(result) > 0


@pytest.mark.asyncio
async def test_get_all_friend_names(client: WeChatClient):
    """
    获取所有好友名称列表.（通过通讯录）
    """
    result = await client.get_all_friend_names()
    print(result)
    assert len(result) > 0


@pytest.mark.asyncio
async def test_passed_all_new_friend(client: WeChatClient):
    """通过加好友添加申请"""
    options = FriendRequestAutoAcceptOptions(
        passed_delete=True, keyword=["test"], label="测试标签", suffix="test"
    )

    async def on_passed(
        passed_list: list[NewFriendBackItem], wechat_client: WeChatClient
    ):
        print(passed_list)
        await wechat_client.send_message("DroidMirror官方技术支持", "你好！")

    result = await client.passed_all_new_friend(options, on_passed)
    assert len(result) > 0


# vip 用户功能
# @pytest.mark.asyncio
# async def test_open_moments(client: WeChatClient):
#     """打开朋友圈,如果未打开，则打开朋友圈，如果已经打开了，则窗口提前到顶端"""
#     assert await client.open_moments()

# vip 用户功能
# @pytest.mark.asyncio
# async def test_close_moments(client: WeChatClient):
#     """打开朋友圈,如果未打开，则打开朋友圈，如果已经打开了，则窗口提前到顶端"""
#     assert await client.close_moments()

# vip 用户功能
# @pytest.mark.asyncio
# async def test_add_moments(client: WeChatClient):
#     """
#     发送朋友圈
#     """
#     options = MomentsOptions(
#         at_usrs=["AI.Net_test"], labels=["aaa"], is_close_moments=True
#     )
#     assert await client.add_moments(
#         [
#             "D:\\repo\\WeChatAuto.SDK\\WeChatAuto.SDK\\src\\WeChatAuto4_X\\WeChatAuto.Tests\\Assets\\1.png"
#         ],
#         "测试的朋友圈消息",
#         options,
#     )

# vip 用户功能
# @pytest.mark.asyncio
# async def test_remove_moments(client: WeChatClient):
#     """
#     移除自己发送的朋友圈
#     """
#     assert await client.remove_moments("测试的朋友圈消息")

# vip 用户功能
# @pytest.mark.asyncio
# async def test_add_group_system_message_listener(client: WeChatClient):
#     """
#     增加系统消息监听，以实现如： 检测到群主邀请好友后发送欢迎消息等功能
#     """

#     async def system_group_monitor_callback(
#         context: SystemMessageContext, client: WeChatClient
#     ) -> None:
#         print(f"{context.from_who = }")
#         print(f"{context.new_messages = }")
#         await client.send_message(
#             "DroidMirror官方技术支持", f"发生了动作 {context.new_messages}，呵呵"
#         )

#     await client.add_group_system_message_listener(
#         ["DroidMirror官方技术支持"], system_group_monitor_callback
#     )
#     await client.keep_running()

# vip 用户功能
# @pytest.mark.asyncio
# async def test_add_message_listener(client: WeChatClient):
#     """
#     消息监听
#     """

#     async def message_monitor_callback(
#         context: MessageContext, client: WeChatClient
#     ) -> None:
#         print(f"{context.owner_nick_name = }")
#         print(f"{context.new_messages = }")
#         print(f"{context.history_messages}")
#         await client.send_message(
#             "DroidMirror官方技术支持", f"发生了动作 {context.new_messages}，呵呵"
#         )

#     await client.add_message_listener(
#         ["DroidMirror官方技术支持"], message_monitor_callback
#     )
#     await client.keep_running()


# @pytest.mark.asyncio
# async def test_add_message_listener_with_time(client: WeChatClient):
#     """
#     一个从什么时候开始，什么时候结束的消息监听
#     vip 用户 功能
#     """

#     async def message_monitor_callback(
#         context: MessageContext, client: WeChatClient
#     ) -> None:
#         print(f"{context.owner_nick_name = }")
#         print(f"{context.new_messages = }")
#         print(f"{context.history_messages}")
#         await client.send_message(
#             "DroidMirror官方技术支持", f"发生了动作 {context.new_messages}，呵呵"
#         )

#     await client.add_message_listener_with_time(
#         ["DroidMirror官方技术支持"],
#         message_monitor_callback,
#         time(11, 20),
#         time(12, 20),
#     )
#     await client.keep_running()

# vip 用户功能
# @pytest.mark.asyncio
# async def test_add_message_listener_with_range(client: WeChatClient):
#     """
#     一天中多个时间段的消息监听
#     """

#     async def message_monitor_callback(
#         context: MessageContext, client: WeChatClient
#     ) -> None:
#         print(f"{context.owner_nick_name = }")
#         print(f"{context.new_messages = }")
#         print(f"{context.history_messages}")
#         await client.send_message("DroidMirror官方技术支持", "hello world!")

#     range_list = [
#         TimeOnlyRange(star_time=time(9, 20), end_time=time(9, 40)),
#         TimeOnlyRange(star_time=time(10, 20), end_time=time(11, 20)),
#     ]
#     await client.add_message_listener_with_range(
#         ["DroidMirror官方技术支持"], message_monitor_callback, range_list
#     )
#     await client.keep_running()


# vip 用户功能
# @pytest.mark.asyncio
# async def test_add_friend_request_auto_accept_listener(client: WeChatClient):
#     """
#     新好友添加监听
#     """

#     async def new_friends_callback(
#         items: list[NewFriendBackItem], client: WeChatClient
#     ):
#         print(items)

#     options = FriendRequestAutoAcceptOptions(
#         passed_delete=True, keyword=["test"], label="测试标签", suffix="test"
#     )

#     await client.add_friend_request_auto_accept_listener(options, new_friends_callback)
#     await client.keep_running()


@pytest.mark.asyncio
async def test_remove_friend(client: WeChatClient):
    result = await client.remove_friend("智影工坊")
    assert result
