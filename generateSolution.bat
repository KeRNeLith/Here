@echo off
pushd %CD%
cd /d %~dp0%
Sharpmake\Generator\Sharpmake.Application.exe "/sources(@"Sharpmake/main.sharpmake.cs") /extensions(@"Sharpmake.SwitchToSource") /verbose %*"
pause
popd
@echo on