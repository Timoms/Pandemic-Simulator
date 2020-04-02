# Pandemic Simulator

This project will give you a simple console which will simulate a virus spreading through humans.

### Config
---
In the Helper.cs class you'll find a RunTimeConfig section where you can adjust some values:

        public static int CWidth { get; set; } -> Console Width
        public static int CHeight { get; set; } -> Console Height
        public static int DayDurationMS { get; set; } -> How long a day will take to pass
        public static int SimulateBreak { get; set; } -> Delay after generation
        public static DateTime DateTime { get; set; } -> Start-Date (like 2020-01-01)
        public static string HumanIcon { get; set; } -> Will draw a defined string as a "human"
        public static float InfectionProbability { get; set; } -> Sets the probability of an infection
        public static float InfectionRadius { get; set; } -> Which radius is needed to get infected

You can adjust all those settings in the Program.cs with the "RunTimeConfig" parameter!

### Usage
---
Change all the settings in the Program.cs as you like and compile. 
The output can be executed without any NuGet packages. 
Its based on plain DotNet Core 3.1 code.


### Realistic? 
---
No. And... yes? 
This project is a really brutal way of showing how a pandemic will look.
Its nothing compared to a real one - but it does look cool!

### Can I use this project?
---
Sure. I would love to see some pull-requests on this project to enhance the way it looks.
But if you fork it and make it your own - why not?