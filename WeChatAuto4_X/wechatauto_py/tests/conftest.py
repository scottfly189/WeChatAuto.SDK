import pytest_asyncio
from wechat_auto_sdk import WeChatClient, WeChatConfig, WechatFactory

DEFAULT_URI = "ws://localhost:5177/ws"

@pytest_asyncio.fixture(scope="function")
async def client():
    async with WechatFactory.create_factory(
        DEFAULT_URI,
        WeChatConfig(
            # 这里可以设置一些配置信息
        ),
    ) as factory:
        await factory.initialize()
        wechat_list = factory.client_list
        print(
            f"本服务器({DEFAULT_URI})共有微信客户端{len(wechat_list)}个:{list(wechat_list.keys())}"
        )
        client: WeChatClient = wechat_list["Alex"]  # 得到某个微信客户端，支持多微信
        print("*" * 20 + " 测试开始 " + "*" * 20)
        yield client
        print("*" * 20 + " 测试结束 " + "*" * 20)
