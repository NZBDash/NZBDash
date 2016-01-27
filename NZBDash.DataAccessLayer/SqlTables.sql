--Any DB changes need to be made in this file.

CREATE TABLE IF NOT EXISTS Users
(
    UserID								INTEGER PRIMARY KEY AUTOINCREMENT,
    UserName							varchar(50) NOT NULL,
    PasswordHash						varchar(100) NOT NULL,
    SecurityStamp						varchar(100) NOT NULL
);

CREATE TABLE IF NOT EXISTS GlobalSettings
(
    Id									INTEGER PRIMARY KEY AUTOINCREMENT,
    SettingsName						varchar(50) NOT NULL,
    Content								varchar(100) NOT NULL
);

CREATE TABLE IF NOT EXISTS LinksConfiguration
(
    Id                                   INTEGER PRIMARY KEY AUTOINCREMENT,
    LinkName	                         varchar(50) NOT NULL,
	LinkEndpoint						 varchar(2083) NOT NULL
);

CREATE TABLE IF NOT EXISTS MonitoringEvents
(
    Id                                   INTEGER PRIMARY KEY AUTOINCREMENT,
    EventName	                         varchar(50) NOT NULL,
	EventStart							 Text(50) NOT NULL,
	EventEnd							 Text(50) NULL
	EventType							 VARCHAR(50) NOT NULL,
);