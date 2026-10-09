@echo off
rem ============================================================
rem  允许"最小化远程桌面窗口"而不中断 UI 自动化。
rem
rem  原理：RDP 客户端(mstsc.exe)在窗口最小化时会挂起远端显示缓冲，
rem  通过写入 RemoteDesktop_SuppressWhenMinimized=2 禁用该优化。
rem
rem  重要：本脚本必须运行在"发起远程桌面的客户端电脑"上，
rem  而不是被控的服务器/机器人电脑上。写 HKLM 需要管理员权限（脚本会自动提权）。
rem ============================================================

net session >nul 2>&1
if %errorlevel% neq 0 (
    echo 正在请求管理员权限...
    powershell -NoProfile -Command "Start-Process -FilePath '%~f0' -Verb RunAs"
    exit /b
)

set VALUE_NAME=RemoteDesktop_SuppressWhenMinimized
set DATA=2

reg add "HKCU\Software\Microsoft\Terminal Server Client"             /v %VALUE_NAME% /t REG_DWORD /d %DATA% /f
reg add "HKCU\Software\Wow6432Node\Microsoft\Terminal Server Client" /v %VALUE_NAME% /t REG_DWORD /d %DATA% /f
reg add "HKLM\Software\Microsoft\Terminal Server Client"             /v %VALUE_NAME% /t REG_DWORD /d %DATA% /f
reg add "HKLM\Software\Wow6432Node\Microsoft\Terminal Server Client" /v %VALUE_NAME% /t REG_DWORD /d %DATA% /f

echo.
echo 设置完成。请按以下步骤操作以生效：
echo   1. 执行作业之前，注销机器人（服务器）计算机上的所有用户。
echo   2. 执行作业之后，重新打开远程桌面连接。
echo 现在，即使 RDP 窗口已最小化，您也可以自动化用户界面操作。
pause
