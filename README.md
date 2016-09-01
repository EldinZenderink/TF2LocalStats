# TF2LocalStats

TF2LocalStats is a simple program that allows you to see real time information about the whole server (Total Kills, Crits, etc). 

### Demo
[YouTube video showing what it does. (v0.1)](https://www.youtube.com/watch?v=NzCfrH6TdLs)

[YouTube video (v0.3)](https://youtu.be/MYEpSDlQrg4)

### Use Cases
If you like numbers and data in real time, this is just for you :D. It doesn't have any uses, just something to keep track of the server your playing on ^^. 

### Usage
Download the map containing the executable and the interface, make sure you keep those files together. Run the executable, follow the setup questions asked within the console window, after you are done, it will launch the interface in your default browser as well as launching team fortress 2 if it  isn't running yet. 

You can access the interface on your phone or tablet as well, if your phone and tablet are connected on the same network as your PC where you are playing tf2 on.

Look up your local network ip by doing the following: 
 
1. Open Command Prompt (Click on start, type cmd, and press enter)
2. Type ipconfig
3. Remember the IPv4 Address
4. Open your browser on your phone (while connected to the same network)
5. Type in http://"YOURIPv4ADDRESS":6365/TF2LocalStat.html
 

### Changelog
**v0.3**

				Changes:
				- Now directly connects to the server to retreive player names.
				- Removed auto detection of servers on local network (overkill as I can just use the ip from your address bar in your browser :S)
				
				Added:
				- Control charts with binds while you are inside TF2.
					-Default charts controls:
						Key: x = resetting the charts data.
						Key: z = pausing the chart (it will not receive new data)
						Key: c = resuming the chart
				- You can change the key bindings by following the setup procedure in the Console Window when you launch the application.
				- Interface shows in the upper right corner the status (paused, recording/receiving, waiting (for tf2 to join a server).
				- New bar chart added which shows Kills, Deaths and K/D Ratio per player (blue,red,green bars)
				- New homemade barchart library added (to replace chartjs barchart, should also replace linechart in the future to phase out chartjs).

**v0.2:**

				Changes:
				- Moved on from google charts to chartsjs due to easier updating of charts.
				- Fully removed command line library and code

				Added:
				- Now auto detects tf2 directory(first time and slow) and stores the location in registery (second launch is faster).
				- Opens interface directly in your default browser when clicking on application.
				- 2 line charts displaying: kills per second & total kills related to the time in seconds
				- 2 bar charts displaying: total kills vs crits & misc such as (headshots, backstabs etc)
				- Option to toggle full history (form second 0 to x) or resetting the line charts every 15 seconds by clicking on them.

				Fixed:

				- Due to character encoding the Contains function would not return true when it should have, fixed!

### Modding

You can develop your own interface, if the default one is not to your liking ^^.

[Developing your own interface!](https://github.com/EldinZenderink/TF2LocalStats/wiki/Developing-your-own-interface!)

### Tech

TF2LocalStats uses a number of open source projects to work properly:

* [jQuery](https://jquery.com/) - duh
* [MaterializeCSS](http://materializecss.com/) - awesome css framwork
* [ChartJS](http://www.chartjs.org/) - soon to be repaced by my home made chart library :D
* [Newtonsoft Json.NET](http://www.newtonsoft.com/json) - awesome json framework for C#

### Development
I just got bored and wanted to see some real time numbers, isn't anything serious so I will randomly add some stuff when I feel like it ^^. Or upon request. (Keep in mind, I can't access user specific data unfortunately :( )

License
----

MIT


**Free Software, Hell Yeah!**
