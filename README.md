# **A**ugmented **R**eality in **C**hemnitz for **H**istoric, **I**nclusive, and **V**irtual **E**xperiences
This repository contains the proof-of-concept implementation of an authoring tool that allows to conveniently upload media (i.e. images) to a pre-built AR/VR web application while running.

A virtual exhibition of an exemplary historically and culturally significant sight—the Mikveh in Chemnitz, Germany—was created.

The project is substantially based on [WebXR export](https://github.com/De-Panther/unity-webxr-export?tab=readme-ov-file) by [de-panther](https://github.com/De-Panther).
Additionally, connections to a filesystem and a database are established via php scripts.

---

## Getting Started
1. Download the repository and open it as a Unity project (v2022.3.24f1) via the Unity Hub. <br> At this stage already, everything will be setup and ready to build a web application (though, without the database; see step 4).
2. ```File > Build Settings > Platform: WebGL > Build > choose directory > confirm```. According to the ```Assets > WebGLTemplate```, the directory of choice now contains several files and folders that make the app.
3. Upload the entire content to your preferred webhosting platform or run it locally, e.g. using [WAMP](https://wampserver.aviatechno.net/).

    **!** Note that WebXR requires https connections. Look into [setupLocalDevEnvironment.md](setupLocalDevEnvironment.md) on how to set it up.

4. In any case, a database must be set up with a table of the following structure:

    |id|datapath|posX|posY|posZ|rotX|rotY|rotZ|
    |:--:|:--------:|:----:|:----:|:----:|:----:|:----:|:----:|
    |(int)|(UNI, varchar)|(float)|(float)|(float)|(float)|(float)|(float)|

    Currently, the name of the remote database is ```Project_ARCHIVE_DB``` and the respective table is simply called ```objects```. The username is ```Project_ARCHIVE_DB_rw```<br>
    The name of the local database is ```mediainventory``` and the respective table is called ```objects``` as well. Here we used the ```root``` user. **!Be aware that this is a potential security issue!**

5. To connect to a database all the credentials mentioned above are to be set within ```UnityDBCommunication.php```, which is to be found in the root directory of the application and respectively the ```Assets > WebGLTemplate``` directory. 
    **!** Note that the password is also stored within the script. This information is not accessible from outside as the file is only readable from the server. Nevertheless, **there are more secure procedures**; please see [Secure Secrets For Web Applications](https://www.tu-chemnitz.de/urz/www/php/secure.html) to upgrade.

Through your browser, you can now visit the website where the application is hosted  (local or remote) and it will start with the desktop version and two buttons to choose either an AR or VR inline-session.

---
## Platforms
- The app was tested on the google Pixel 8 mobile phone using chrome browser. Issues were found with Firefox browser.
- To the best of our knowledge apple devices do not support WebXR yet (with the Apple Vision Pro being a potential exception).
- The app was also tested on a Meta Quest 3.
- In general, WebXR AR sessions are designed for monoscopic display only (and the camera is always on, as per the original idea of AR). VR sessions on the other hand are designed for stereoscopic display only. While both AR and VR buttons are enabled on both mobile and HMD (and both devices at least theoretically can handle it), the app only works as expected when selecting the AR session on mobile and the VR session on HMDs. Btw. there is the necessity to have the [user intentionally interact](https://immersive-web.github.io/webxr/#user-intention) with the web app, i.e. clicking a button, which is a security feature of WebXR. It does not work to emulate a button-click.

---
## Important Components/Options
- ```LoadResourcesFromURL.cs```
- ```Edit > Project Settings > XR Plug-in Management > WebXR``` (e.g. Required Reference Space, Hit-Test under AR optional features, etc. )
- ```SceneHitTest.cs``` + SceneHitTest Gameobject
- ```ActionBasedControllerManager.cs``` and respectively [Input Action Assets](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/ActionAssets.html) in general
- ```ExplanationSwitch.cs```

---
## Non-interactive build
For a more secure build that does not utilise php and thus no database:
- deactivate component ```LoadResoureFromURL.cs``` and activate component```LoadResoureFromURL_offline.cs```
- deactivate ```UI Sample``` Gameobject
- deactivate ```Info Label``` Gameobject
- deactivate component ```ExplanationSwitch.cs```
- deactivate Gameobjects ```Poke Interactor```, ```Direct Interactor```, ```Ray Interactor``` for both left and right controller

---
## Further ideas, TO-DOs, bugs-to-be-fixed
- Most importantly: The integration of more sophisticated media other than images or their display in a more appealing (read: dynamic) way than just plain images but maybe animations.
- Looking at 2-dimensional posters from a "virtual" perspective may be the biggest UX/accessibility issue. Perhaps one wants the posters to snap into something like a reader-mode where it appears as if one is browsing a pdf on screen (with all the accessibility feature like pinching to zoom in).
- Scaling the model within the app.
- Implement a timer before the animation starts that lerps the viewer into the mikveh.
- Implement a menu before the scene is even visible (eventually explaining what is about to be seen/happening + how-to-control). Also think of the problem of both AR/VR buttons being enabled even though only one is actually leading to the desired experience (see the issue described above -> platforms).
- *To-be-fixed:* currently the canvas object does not adjust to the content (the raw image holding the content). However the canvas object is the one you interact with with the controllers.
- *To-be-fixed:* Sometimes there is an issue with the position where one spawns when entering AR/VR. This might have to do with the ```Required Reference Frame``` and the offset of the ```XR Origin (XR Rig)``` (see Important Components/Options above).
- *To-be-fixed:* Holding a media object with the right hand controller via direct interaction (i.e., not via ray interaction) and using the other controller to move at the same time makes one fall of the ground or elevate into the sky.
- *To-be-fixed:* If media is deleted from the ```assets``` directory while the app is running and new media is added, this may not spawn at the default position.


---
## Further Resources
[WebXR](https://immersive-web.github.io)
[tu-chemnitz.de phpmyadmin web interface](https://dbwebadmin.hrz.tu-chemnitz.de/phpmyadmin/index.php)
[Unity + databases tutorial](https://youtu.be/SKbY-0zt2VE?feature=shared)
