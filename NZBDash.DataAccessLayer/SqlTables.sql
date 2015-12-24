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

CREATE TABLE IF NOT EXISTS NzbDashSettings
(
    Id                                   INTEGER PRIMARY KEY AUTOINCREMENT,
    Authenticate                         INTEGER  NOT NULL
);

CREATE TABLE IF NOT EXISTS NzbGetSettings
(
    ID                                  INTEGER PRIMARY KEY AUTOINCREMENT,
    IpAddress                           VARCHAR(100)  NOT NULL,
    Port                                INTEGER  NOT NULL,
    Enabled           				    INTEGER  NOT NULL,
    ShowOnDashboard						INTEGER  NOT NULL,
    Username							VARCHAR(100)  NOT NULL,
    Password							VARCHAR(100)  NOT NULL
);

CREATE TABLE IF NOT EXISTS SonarrSettings
(
    ID                                  INTEGER PRIMARY KEY AUTOINCREMENT,
    IpAddress                           VARCHAR(100)  NOT NULL,
    Port                                INTEGER  NOT NULL,
    Enabled           					INTEGER  NOT NULL,
    ShowOnDashboard						INTEGER  NOT NULL,
    ApiKey					            VARCHAR(100)  NOT NULL
);

CREATE TABLE IF NOT EXISTS PlexSettings
(
    ID                                  INTEGER PRIMARY KEY AUTOINCREMENT,
    IpAddress                           VARCHAR(100)  NOT NULL,
    Port                                INTEGER  NOT NULL,
    Enabled           					INTEGER  NOT NULL,
    ShowOnDashboard						INTEGER  NOT NULL,
    Username							VARCHAR(100)  NOT NULL,
    Password							VARCHAR(100)  NOT NULL
);

CREATE TABLE IF NOT EXISTS CouchPotatoSettings
(
    ID                                  INTEGER PRIMARY KEY AUTOINCREMENT,
    IpAddress                           VARCHAR(100)  NOT NULL,
    Port                                INTEGER  NOT NULL,
    Enabled           					INTEGER  NOT NULL,
    ShowOnDashboard					    INTEGER  NOT NULL,
    Username							VARCHAR(100)  NOT NULL,
    Password							VARCHAR(100)  NOT NULL,
    ApiKey					            VARCHAR(100)  NOT NULL
);

CREATE TABLE IF NOT EXISTS SabNzbSettings
(
    ID                                  INTEGER PRIMARY KEY AUTOINCREMENT,
    IpAddress                           VARCHAR(100) NOT NULL,
    Port                                INTEGER NOT NULL,
    Enabled           					INTEGER NOT NULL,
    ShowOnDashboard					    INTEGER NOT NULL,
    ApiKey					            VARCHAR(100) NOT NULL
);