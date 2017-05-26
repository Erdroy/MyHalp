# MyHalp - Unity 5 helper library
Unity 5 helper library, contains some useful stuff for better and easier developement.

## Main Features:
 - **MyPreloader** - Editor extension that generates `MyAssets` class, with all given assets to preload, call MyAssets.Init() to load all of them at once - using async loading.
 - **MyConsole** - Console window for fast development or even ready projects.
 - **MyCommands** - Class for registering commands and executing by string (eg.: 'volume master 0.2') or name and object array as parameters.
 - **MyLogger** - `MyLogger.Add("Wouw, LOGS! MUCH logs. Love logs.")`;
 - **MyTimer** - Allows to run methods with delay or interval, wub wub wub wub, error.
 - **MyJob** - Threaded jobs.
 - **MyJobQueue** - Queued jobs with progress.
 - **MyDispatcher** - Call any method/deleate from any thread in the main thread - Wow! What a great thing.
 - **MyComponent** - Custom Unity behaviour class.
 - **MyInput** - Custom Input class that supports binding, FixedUpdate (via .FixedGet) and more.
 - **MyObjectPool** - Pooled objects, request 'new' gameobject without performace hit at any time.
 - **MyInstancer** - Creates new instance of given component, and assings it to a gameobject.
 - **MyMath** - MyVector2/3/4, MyQuaternion, MyMatrix/3x3/4x4 and much more (using SharpDX.Mathematics).

## Incoming Features (under developement):
 - **MyCooker** - Editor extension which helps building applications in specific configurations at once (eg.: game and server with define symbols). 
 - **MyProfiler** - Runtime profiling tool.
 
## Planned Features:
 - **MyFade** - Smooth fading between scenes and cameras.
 - **MyMessage** - Show nice messages on UI by simply calling MyMessage.Show("Hello, World!");
 - **MyRCon** - Remote MyCommand execution.
 - **MyLightProbeGenerator** - Editor extension that helps placing (procedurally) light probes.
 - **MyBundle** - Editor extension that helps creating asset bundles and loading them during runtime.

## You are using MyHalp? [Let us know!](https://github.com/Erdroy/MyHalp/issues) 
You can add an attachment logo of size 180x80 pixels.

## Support/Contact
If you have found an **issue** or you have some **suggestions**, send it in the **[Issues](https://github.com/Erdroy/MyHalp/issues)** section.
Also, You can join my **[Discord server](https://discord.gg/ybJaGtb)** to get real-time support.
Use as template: [MurderShadow is using MyHalp!](https://github.com/Erdroy/MyHalp/issues/2)

## [Examples](https://github.com/Erdroy/MyHalpExamples)

## [Trello](https://trello.com/b/2FMBpoOG/myhalp)

## Who is using MyHalp?
[MurderShadow](http://store.steampowered.com/app/506690) - online fast-paced shooter on Steam. <br>
![logo](https://cloud.githubusercontent.com/assets/7634316/26507479/cbf91cd4-4250-11e7-8bfc-9ff427489e72.png)

<br><br><br>
MyHalp (c) 2016-2017 Damian 'Erdroy' Korczowski
