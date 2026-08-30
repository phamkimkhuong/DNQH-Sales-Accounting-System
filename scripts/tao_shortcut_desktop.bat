@echo off
setlocal
chcp 65001 >nul
echo ================================================================================
echo    TAO SHORTCUT UNG DUNG DNQH KE TOAN BAN HANG NGOAI DESKTOP
echo ================================================================================

pushd "%~dp0.."
set "ROOT_DIR=%CD%"
popd

set "TARGET_EXE=%ROOT_DIR%\bin\Debug\DNQH_KeToanBanHang.exe"
set "WORKING_DIR=%ROOT_DIR%\bin\Debug"

if not exist "%TARGET_EXE%" (
    set "TARGET_EXE=%ROOT_DIR%\bin\Release\DNQH_KeToanBanHang.exe"
    set "WORKING_DIR=%ROOT_DIR%\bin\Release"
)

powershell -NoProfile -ExecutionPolicy Bypass -Command "$ws = New-Object -ComObject WScript.Shell; $desktop = [Environment]::GetFolderPath('Desktop'); $shortcutPath = Join-Path $desktop 'DNQH - Ke Toan Ban Hang.lnk'; $shortcut = $ws.CreateShortcut($shortcutPath); $shortcut.TargetPath = '%TARGET_EXE%'; $shortcut.WorkingDirectory = '%WORKING_DIR%'; $shortcut.Description = 'He Thong Ke Toan Ban Hang DNQH'; $shortcut.IconLocation = '%TARGET_EXE%,0'; $shortcut.Save(); Write-Host '[THANH CONG] Da tao loi tat Desktop:' $shortcutPath"

echo ================================================================================
echo Shortcut ngoai Desktop da duoc thiet lap bieu tuong App Icon DNQH dong bo.
echo ================================================================================
pause