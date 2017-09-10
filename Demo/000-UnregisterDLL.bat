regsvr32 /u "%~dp0SignalGen.dll"
regsvr32 /u "%~dp0SignalGenPS.dll"
"%~dp0OPCServer.exe" /RegServer /u
regsvr32 /u "%~dp0OPCServerPS.dll"
pause