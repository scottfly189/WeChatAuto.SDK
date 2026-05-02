#!/usr/bin/env python3
# -*- coding: utf-8 -*-

'pythhon調用示例，更多的調用（如：髮送文件等）請參加.net代碼'

import requests

_from="Alex"
_baseUrl = "http://localhost:5000/api/v1/message"


def _SendMessageCore(who,message):
    requests.post(_baseUrl,json={
        "from":_from,
        "to":who,
        "message": message
    })

def SendMessage():
    whoList = []
    for x in [1,2,3,4,5]:
        whoList.append("测试0"+str(x))
    for who in whoList:
        _SendMessageCore(who,"这是通过自动化\r\n发送的文本消息")

if __name__ == "__main__":
    SendMessage()