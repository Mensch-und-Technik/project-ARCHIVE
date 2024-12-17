# **A**ugmented **R**eality in **C**hemnitz for **H**istoric, **I**nclusive, and **V**irtual **E**xperiences
This repository contains the proof-of-concept implementation of an authoring tool that allows to conveniently upload media (i.e. images) to a pre-built AR/VR web application while running.

A virtual exhibition of an exemplary historically and culturally significant sight-the Mikveh in Chemnitz, Germany-was created.

The project is substantially based on [WebXR export](https://github.com/De-Panther/unity-webxr-export?tab=readme-ov-file) by [de-panther](https://github.com/De-Panther).
Additionally, connections to a filesystem and a database are established via php scripts.

---

## Getting Started
1. Download the repository and open it as a Unity project (v2022.3.24f1) via the Unity Hub. <br> At this stage already, everything will be setup and ready to build a web application (though, without the database; see step 4).
2. ```File > Build Settings > Platform: WebGL > Build > choose directory > confirm```. According to the ```Assets > WebGLTemplate```, the directory of choice now contains several files and folders that make the app.
3. Upload the entire content to your preferred webhosting platform or run it locally, e.g. using [WAMP](https://wampserver.aviatechno.net/).

    **!** Note that WebXR requires https connections. Look into ```setupLocalDevEnvironment.md``` [#TO-DO] on how to set it up.

4. In any case, a database must be set up with a table of the following structure:

    |id|datapath|posX|posY|posZ|rotX|rotY|rotZ|
    |:--:|:--------:|:----:|:----:|:----:|:----:|:----:|:----:|
    |(int)|(UNI, varchar)|(float)|(float)|(float)|(float)|(float)|(float)|

    Currently, the name of the remote database is ```Project_ARCHIVE_DB``` and the respective table is simply called ```objects```. The username is ```Project_ARCHIVE_DB_rw```<br>
    The name of the local database is ```mediainventory``` and the respective table is called ```objects``` as well. Here we used the ```root``` user. **!Be aware that this might be a security issue!**

5. To connect to the database all the credentials mentioned above are to be set within ```UnityDBCommunication.php```, which is to be found in the root directory of the application and respectively the ```Assets > WebGLTemplate``` directory. The password is also stored within the script. This information is not accessible from outside as the file is only readable from the server. Nevertheless, **there are more secure procedures**; please see [Secure Secrets For Web Applications](https://www.tu-chemnitz.de/urz/www/php/secure.html) to upgrade.

You can now visit the website where the application is hosted  (local or remote) and it will start with the desktop version and two buttons to choose either an AR or VR inline-session.

---
## Important Components/Options
- ```LoadResourcesFromURL.cs```
- ```Edit > Project Settings > XR Plug-in Management > WebXR``` (e.g. Hit-Test under AR optional features; Required Reference Space)
- ```ActionBasedControllerManager.cs``` and respectively [Input Action Assets](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/ActionAssets.html) in general
- ```SceneHitTest.cs```
- ```ExplanationSwitch.cs```

---
## Non-interactive build
For a more secure build that does not utilise php and thus no database:
- deactivate component ```LoadResoureFromURL.cs``` and activate component```LoadResoureFromURL_offline.cs```
- deactivate ```UI Sample``` Gameobject
- deactivate ```Info Label``` Gameobject
- deactivate component ```ExplanationSwitch.cs```
- deactivate Gameobjects ```Poke Interactor```, ```Direct Interactor```, ```Ray Interactor``` for both left and right controller

## Further Resources
[Unity + databases tutorial](https://youtu.be/SKbY-0zt2VE?feature=shared)
