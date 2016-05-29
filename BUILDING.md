To build MyHalp library you need:
 - Visual Studio, I recommend you to use 2015 version.
 - Installed Unity3D 5.0+ 

Build steps:
 - Open MyHalp.sln wih Visual Studio.
 - Solution Window > PPM on MyHalp project > Properties > [Build] > and change the Output path to something else, eg.: X:\Your\Projet\Path\Assets\Plugins
 - Now, we need to reference UnityEngine.dll, In the solution window go to References > delete the UnityEngine, click PPM on Refrences > Add Reference > 
 [Browse] > click browse and go to the unity install dir then Editor\Data\Managed and click on the UnityEngine.dll. Eg.: X:\_Apps\Unity 5.4\Editor\Data\Managed\UnityEngine.dll
 - It is done! Now click (re)build solution, and voila!