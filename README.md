# **A**ugmented **R**eality in **C**hemnitz for **H**istoric, **I**nclusive, and **V**irtual **E**xperiences
This repository contains the proof-of-concept implementation of an authoring tool that allows to conveniently upload media (i.e. images) to a pre-built web application while running.

A virtual exhibition of an exemplary historically and culturally significant sight-the Mikveh in Chemnitz, Germany-was created.

The project is substantially based on [WebXR export](https://github.com/De-Panther/unity-webxr-export?tab=readme-ov-file) by [de-panther](https://github.com/De-Panther).

Additionally, connections to a filesystem and a database are established via php scripts.

---

## Getting Started
1. Download the repository and open it as a Unity project (v2022.3.24f1) via the Unity Hub. <br> At this stage, everything will be setup and ready to build a web application.
2. ```File > Build Settings > Platform: WebGL > Build > choose directory > confirm```. According to the ```Assets > WebGLTemplate```, the directory of choice now contains several files and folders that make the app.
3. Upload the entire content to your preferred webhosting platform or run it locally, e.g. using [WAMP](https://wampserver.aviatechno.net/).
4. In any case, a database must be set up with a table of the following structure:

    |id|datapath|posX|posY|posZ|rotX|rotY|rotZ|
    |:--:|:--------:|:----:|:----:|:----:|:----:|:----:|:----:|
    |(int)|(UNI, varchar)|(float)|(float)|(float)|(float)|(float)|(float)|

5. The credentials to connect to the database are to be set within ```UnityDBCommunication.php```, which is to be found in the root directory of the application and respectively the ```Assets > WebGLTemplate``` directory.

You can now visit the website where the application will start with the desktop version and two buttons to choose either AR or VR inline-sessions.

**!** Note that WebXR requires https connections. Look into ```setupLocalDevEnvironment.md ?!``` on how to set it up.

---
## Important Components & Options
- ```Edit > Project Settings > XR Plug-in Management > WebXR```
- ```LoadResourcesFromURL.cs```
- ```ActionBasedControllerManager.cs``` and respectively [Input Action Assets](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/ActionAssets.html) in general
