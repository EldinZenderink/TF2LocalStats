# TF2LocalStats

TF2LocalStats is a simple program that allows you to see real time information about the whole server (Total Kills, Crits, etc). 

### Demo
[YouTube video showing what it does.](https://www.youtube.com/watch?v=NzCfrH6TdLs)

### Use Cases
If you like numbers and data in real time, this is just for you :D. It doesn't have any uses, just something to keep track of the server your playing on ^^. 

### Requirements
TF2 To be installed in the default directory (will be changeable in the future): C:\Program Files (x86)\Steam\steamapps\common\Team Fortress 2

### Usage
Download the map containing the executable and the interface, make sure you keep those files together. Run the executable, and access the interface by either clicking on the .html file or opening your browser and typing: 127.0.0.1:6365/tf2data.html, and yes, this works on phones as well by doing the following:

Look up your local network ip by doing the following: 
 
1. Open Command Prompt (Click on start, type cmd, and press enter)
2. Type ipconfig
3. Remember the IPv4 Address
4. Open your browser on your phone (while connected to the same network)
5. Type in http://"YOURIPv4ADDRESS":6365/tf2data.html
6. If nothing shows up, refresh a few times ^^.
 

### Tech

TF2LocalStats uses a number of open source projects to work properly:

* [jQuery](https://jquery.com/) - duh
* [MaterializeCSS](http://materializecss.com/) - awesome css framwork
* [CSWebServerDetection](https://github.com/EldinZenderink/CSWebServerDetection) - MINE :D
* [Google Charts](https://developers.google.com/chart/interactive/docs/) - amazing graphs and easy to use :D
* [Newtonsoft Json.NET](http://www.newtonsoft.com/json) - awesome json framework for C#

### Development
I just got bored and wanted to see some real time numbers, isn't anything serious so I will randomly add some stuff when I feel like it ^^. Or upon request. (Keep in mind, I can't access user specific data unfortunately :( )

License
----

MIT


**Free Software, Hell Yeah!**
