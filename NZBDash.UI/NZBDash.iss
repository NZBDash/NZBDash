; -- Example1.iss --
; Demonstrates copying 3 files and creating an icon.

; SEE THE DOCUMENTATION FOR DETAILS ON CREATING .ISS SCRIPT FILES!

[Setup]
AppName=NZBDash
AppVersion=1.0
DefaultDirName={pf}\NZBDash
DefaultGroupName=NZBDash
UninstallDisplayIcon={app}\NZBDash.exe
Compression=lzma2
SolidCompression=yes
OutputDir=..\Installer
PrivilegesRequired=admin
UninstallRestartComputer=false


[Files]
Source: "bin\*"; DestDir: "{app}\bin"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "Content\*"; DestDir: "{app}\Content"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "fonts\*"; DestDir: "{app}\fonts"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "Scripts\*"; DestDir: "{app}\Scripts"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "Styles\*"; DestDir: "{app}\Styles"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "Views\*"; DestDir: "{app}\Views"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "favicon.ico"; DestDir: "{app}"; Flags: ignoreversion  
Source: "NLog.config"; DestDir: "{app}"; Flags: ignoreversion  
Source: "Web.config"; DestDir: "{app}"; Flags: ignoreversion  
Source: "Global.asax"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
Name: "{group}\My Program"; Filename: "{app}\MyProg.exe"

[Run]
Filename: {cmd}; Parameters: "/C ""C:\Windows\System32\inetsrv\appcmd.exe add site /name:NZBDash /bindings:http/*:7500 /physicalPath:{pf}/NZBDash/"""; Check: IsCheckSelectedIIS; Flags: runhidden; StatusMsg: "Settings Up Website"

[Code]


var iisDefaultDir: String;
var WebServerPage: TInputOptionWizardPage;

function GetWebServerType(Param:String):String;

var
	WebServer: String;
begin
	case WebServerPage.SelectedValueIndex of
		0:
			if iisDefaultDir <> '' then begin
				WebServer := 'iis';
			end else
				WebServer := 'apache';
		1: WebServer := 'apache';
	end;
	Result := WebServer;
end;

function IsCheckSelectedIIS():Boolean;
begin
	Result := GetWebServerType('') = 'iis';
end;

