![NZBDash Preview](http://i.imgur.com/0onuYbH.png)

[![Build status](https://ci.appveyor.com/api/projects/status/lsho0rk4etbvdwmd?svg=true)](https://ci.appveyor.com/project/tidusjar/nzbdash)
[![Gitter](https://badges.gitter.im/NZBDash/NZBDash.svg)](https://gitter.im/NZBDash/NZBDash?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge)


# NZBDash

NZBDash is a all in one dashboard for your server! 
The goal is to provide basic information about your news reader (SabNZB, NZBGet), your news automation programs (Sonarr, CouchPotato etc.) and also information about your system!

The problem I had was I was having to manually go and check each of my usenet readers and automation programs, NZBDash put's all of their functionality together. You are able to control your usenet readers and automation programs from one place. 

It's not all about Usenet though, there is also a server monitoring aspect to NZBDash, want to know when your media HDD is getting low on space? We can alert on that! Want to know when your CPU is > 80% for 2 minutes? Get an email notification to let you know!

### Current Features
- Server Monitoring alerts:
  - CPU Thresholds
  - HDD Space
  - Network activity

- SabNzbd Integration
  - Current Downloads
  - Download History 
  - Logs

- NZBGet Integration
  - Current Downloads
  - Download History 
  - Logs

- Sonarr Integration
  - View current series
  - View episode statues e.g. Downloaded, Missing
  - Search for episodes

- CouchPotato Integration


### Possible Features
- XBMC Integration
- Plex Integration
- Headphones Integration
- Sickbeard Integration

# Preview 

![NZBDash Preview](http://i.imgur.com/MKDE9Nr.gif)

## Contributors

We are looking for any contributions to the project! Just pick up a task, if you have any questions ask and i'll get straight on it!

Please feel free to submit a pull request!


## Ideas

We are still very early in the project so if you have any ideas just post it!

## Installation

Currently there is a windows installer, The Installer will currently install the Monitoring service and put the Website on your server, the website will not be setup though.

To setup the website you need to have IIS (Currently working on a Mono solution) and create a new website and it's path will be the NZBDash install web directory, something like "C:\Program Files(x86)\NZBDash\UI\".
Then go to the Application Pool that the website is in and Right Click > Advanced Settings > Enabled 32-Bit Applications = `True`

## Sponsors
- [JetBrains](http://www.jetbrains.com/) for providing us with free licenses to their great tools!!!
    - [ReSharper](http://www.jetbrains.com/resharper/)
    - [dotTrace] (https://www.jetbrains.com/profiler/)
    - [dotMemory] (https://www.jetbrains.com/dotmemory/)
    - [dotCover] (https://www.jetbrains.com/dotcover/)

