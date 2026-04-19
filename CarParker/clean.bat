@echo off
dotnet build-server shutdown
taskkill /F /IM CarParker.exe
rmdir /s /q bin
rmdir /s /q obj
echo Cleanup complete
pause