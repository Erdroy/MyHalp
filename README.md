MyHalp - Unity 5 helper library.

Unity 5 helper library, contains some useful methods for better and smooth developement.
Currently under reconstruction, see 'Contribution' for current progress and tasks.

# Main Features:
 - **MyPreloader** - Editor extension that generates `MyAssets` class, with all given assets to preload, call MyAssets.Init() to load all of  once - using async loading
 - **MyConsole** - Console window for fast development or even ready projects.
 - **MyCommands** - Class for registering commands and executing by string(eg.: 'volume master 0.2') or name and object array as parameters.
 - **MyLogger** - `MyLogger.Add("Wouw, LOGS! MUCH logs. Love logs.")`;
 - **MyComponent** - Custom Unity behaviour class.
 - **MyInput** - Custom Input class that supports binding, FixedUpdate(via .FixedGet) and more.
 - **MyObjectPool** - Pooled objects, request 'new' gameobject without performace hit at any time.
 - **MyJob** - Threaded jobs.
 - **MyJobQueue** - Queued jobs with progress.
 - **MyDispatcher** - Call any method/deleate from any thread in the main thread - Wow! What a great thing.
 - **MyInstancer** - Creates new instance of given component, and assings it to a gameobject.
 - **MyMath** - MyVector2/3/4, MyQuaternion, MyMatrix/3x3/4x4 and much more(using SharpDX.Mathematics).

# Incoming Features(under developement):
 - **MyCooker** - Editor extension which helps building applications in specific configurations at once(eg.: game and server with define symbols). 
 - **MyProfiler** - Runtime profiling tool.
 - **MyTimer** - Allow to run methods with delay or interval.
 
# Planned Features:
 - **MyFade** - Smooth fading between scenes and cameras.
 - **MyMessage** - Show nice messages on UI by simply calling MyMessage.Show("Hello, World!");
 - **MyRCon** - Remote MyCommand execution.
 - **MyLightProbeGenerator** - Editor extension that helps placing(procedurally) light probes.
 - **MyBundle** - Editor extension that helps creating asset bundles and loading them during runtime.

# Building:
Check the BUILDING.md file.

# Examples
Examples repo: https://github.com/Erdroy/MyHalpExamples

# Contribution
Feel free to ask, make some suggestins and contribute to the project, much fun(not really)!
Trello page: https://trello.com/b/2FMBpoOG/myhalp

# Contact
Contact, you want to talk with me? Join my discord server:
https://discordapp.com/invite/0kfQL9KtL4XfOK7G

MyHalp (c) 2016-2017 Damian 'Erdroy' Korczowski and contibutors.
