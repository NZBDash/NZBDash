@echo off
cd C:\Windows\System32\inetsrv\

appcmd add site /name:NZBDash /bindings:http/*:7500 /physicalPath:%1/UI/