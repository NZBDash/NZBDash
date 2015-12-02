CREATE TABLE IF NOT EXISTS LinksConfigurations
(
    ID                                   INTEGER PRIMARY KEY AUTOINCREMENT,
    LinkName                             VARCHAR(100)  NOT NULL,
    LinkEndpoint                         VARCHAR(100)  NOT NULL
);

CREATE TABLE IF NOT EXISTS NzbGetSettings
(
    ID                                  INTEGER PRIMARY KEY AUTOINCREMENT,
    IpAddress                           VARCHAR(100)  NOT NULL,
    Port                                INTEGER  NOT NULL,
    Enabled           				     INTEGER  NOT NULL,
    ShowOnDashboard				     INTEGER  NOT NULL,
    Username							 VARCHAR(100)  NOT NULL,
    Password							 VARCHAR(100)  NOT NULL
);

CREATE TABLE IF NOT EXISTS SonarrSettings
(
    ID                                  INTEGER PRIMARY KEY AUTOINCREMENT,
    IpAddress                           VARCHAR(100)  NOT NULL,
    Port                                INTEGER  NOT NULL,
    Enabled           					 INTEGER  NOT NULL,
    ShowOnDashboard					 INTEGER  NOT NULL,
    ApiKey					             VARCHAR(100)  NOT NULL
);

CREATE TABLE IF NOT EXISTS PlexSettings
(
    ID                                  INTEGER PRIMARY KEY AUTOINCREMENT,
    IpAddress                           VARCHAR(100)  NOT NULL,
    Port                                INTEGER  NOT NULL,
    Enabled           					 INTEGER  NOT NULL,
    ShowOnDashboard					 INTEGER  NOT NULL,
    Username							 VARCHAR(100)  NOT NULL,
    Password							 VARCHAR(100)  NOT NULL
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