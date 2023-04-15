# Extensible XP System (v1.0.4)
Extensible XP System aims for a simple XP system that you can expand upon. Quickly change XP formulas and how the XP should be calculated.


## Table of contents
1. [How to use](#how-to-use)
2. [What is included](#what-is-included)
3. [Changelog](#changelog)
3. [Credits](#credits)
3. [Feedback](#feedback)
3. [Contact & Support](#contact--support)


## How to use
1. Add the `XPGranter.cs`-component to an enemy.  
  a. The component expects a XP Grant Method (basically the *WHO* the enemy should be granting XP to).  
  b. You can use the `XP grant method - All in runtime set`-ScriptableObject to grant XP to all in a runtime set.  
2. Add the `XPReceiver.cs`-component to a player.  
  a. Assign `Runtime set - Can receive XP`-ScriptableObject which makes this GameObject one of the *WHO* that receives XP.  
  b. **!Important!** Assign a XP calculation method (this is the *WHERE* the XP gets saved).  
  c. The events can be used to update your UI of changes.  
3. Hook up the GrantXP() function in the XPGranter component on your enemy. You can for example call the function when the enemy dies.
4. Done! Have fun!

_**NOTE​**: For versions v5.6 or v2017.4: The prefabs doesn't work because of Unity's new prefab workflow introduced in v2018.3. Code runs fine though. If you are running these versions just skip importing the prefabs (they are very simple prefabs)._

_**NOTE​**: If you are only after the XP system then everything you need is in the `/Runtime/XP` folder. And if you don't want to use RuntimeSets: find and remove the `canReceiveXPSet` references from `XPReceiver.cs` file._


## What is included

### /Runtime/RuntimeSets
ScriptableObjects that keeps a list of references so other MonoBehaviours can reference it on runtime. The lists can be used for anything you'd like, a common one is keeping a list of GameObjects (included).

### /Runtime/UI
Scripts that the UI uses for displaying XP. Not needed for the XP system to function.

### /Runtime/XP/Data/XP formulas
Contains all formulas on how XP is calculated. These scripts are ScriptableObjects that you create in your project and you (or your designers) can change the values in the inspector. You can expand and create your own formula, just inherit from DataXPFormula.

**Xp Per Level**: Base field that you can define however you’d like. In the receive methods I’ve created the required XP for the next level is increased by Xp Per Level (100).

![](https://dev.saucy.se/unity/extensible-xp-system/images/xp_formula.png)

### /Runtime/XP/Data/XP grants
ScriptableObjects that contain how XP should be granted. I've included two examples I think are the most used methods.  
- **DataXPGrantAllInRuntimeSet**: Grants XP to all objects (in this example all players).  
- **DataXPGrantInAnArea**: Checks everything in a sphere for objects that implements the IXPReceive interface. You can change the layers which the script should look for IXPReceiver interfaces.  

**Radius**: The radius how far from the GameObject that has XPGranter.cs will look for colliders with the IXPReceive interface (in this case we don’t use it so we set it to 0. It is mainly used in DataXPGrantInAnArea, but you can use it for your own methods).  
**Can Receive XP Set**: The runtime set we’ll loop through and grant XP to the items in the list.  
**Layers To Check For Receive XP**: Here you can specify which layers to check for the IXPReceive interface.  

![](https://dev.saucy.se/unity/extensible-xp-system/images/xp_grant_method.png)

### /Runtime/XP/Data/XP receivers
**NOTE**: The ScriptableObject assets that are created from these scripts are the ones that store the XP.
These scripts do the heavy lifting. Based on the XP formula selected it calculates the current XP, current level, etc.

In these assets is also where you assign the maximum level. A difficulty multiplier is included if you want to use it (higher value increases the XP gained).

I've included two XP receiving methods that I think are the most used XP system out there.  
- **Normal method**: Works like World of Warcraft does their XP. It keeps all the XP you’ve gained and just increases the required XP for the next level. Does not reset the current XP.  
- **Alternative method**: Works like Horizon: Zero Dawn does their XP. It resets current XP to zero after you gained a level and increases the required XP to level up.  

**Max Level**: Maximum level, we can set this in the inspector.  
**Difficulty Multiplier**: A multiplier we use when adding current XP and acquired XP.  
**Selected XP Formula**: Which XP formula to base the calculations on.  
**Set Progress To Zero On Reaching Max Level**: Set progress to zero upon hitting max level (similar to how World of Warcraft does it).  

![](https://dev.saucy.se/unity/extensible-xp-system/images/xp_receive_method.png)

### /Runtime/XP/Interfaces
"Contracts" of how XP must be granted and received.

### /Runtime/XP/XPGranter
The script that grants XP. Could be added to an enemy, and hooked up that when it dies it grants XP. Call XPGranter.GrantXP() to grant XP.

I've also included an Interval field that calls GrantXP() each specified interval (runs an IEnumerator, and the default value is OFF and 1 second).

**Experience**: The amount of XP to grant the receiver.  
**Use Interval**: Enabled grants XP each interval.  
**Interval**: How often XP should be granted (seconds).  
**Xp Grant Method**: Which XP Grant method that should be used when calling GrantXP(). This can easily be changed by just drag & dropping another XP Grant method in the inspector.  

![](https://dev.saucy.se/unity/extensible-xp-system/images/xp_granter.png)

### /Runtime/XP/XPReceiver
The script that receives XP. Could be added to a player, and depending on how you are granting XP this GameObject receives if the criteria is fulfilled.

Has UnityEvents that you can easily hook up to UI (for example to display something when the player levels up).

**Can Receive XP Set**: Reference to a RuntimeSet that this script adds itself to, so it can receive XP.  
**Reset XP On Enable**: Reset the acquired XP back to zero on OnEnable.  
**Xp Calculation Method**: The XP calculation method that is going to save the XP, calculate current XP and level, calculate missing XP, etc.  

Below are UnityEvents that you can use to call things like the UI, other GameObjects to let them know XP has changed, the player has gained a level, etc.  
**On XP Changed ()**: Whenever gained XP has been added to the acquired XP this event is called.  
**On Level Up ()**: Whenever a new level is reached this event is called.  
**On Level Max Reached ()**: When maximum level is reached this event is called.  
**On XP Reset ()**: Whenever the XP is reset this event is called.  

![](https://dev.saucy.se/unity/extensible-xp-system/images/xp_receiver.png)

### /Samples~/Example/Data
ScriptableObject assets that act like data containers for: XP formulas, XP grant methods, and XP receive methods (more on those below).

### /Samples~/Example/Prefabs
- **UI prefabs**: Used for showing how the package works.
- **XP prefabs**: Simple GameObjects that have the XP components.

### /Samples~/Example/Scenes
Demo scene for showing how the package works.


## Changelog
### v1.0.4 (2020-06-10)
- Fixed: Warnings in the package.
- Fixed: RequiredXP not being initialized causing a "Max level reached" failure.

### v1.0.3 (2020-05-23)
- Fixed: Files are now in the example sample. Unity Package Manager made the scene read-only which made it impossible to open.

### v1.0.2 (2020-05-23)
- Updated: Installation and How-To-Use information in README and documentation files.
- Added: FAQ information in the demo scene.
- Cleanup: Re-restructure folders (again).
- Cleanup: Moved everything under the same namespace.
- Fixed: Dotfiles to help preserve .meta files.
- Removed: Unused Tests.

### v1.0.1 (2020-05-19)
- Updated: Remade everything as a Unity Package instead of a unity project (removed unnecessary files, restructured all folders, etc).
- Updated: Renamed ScriptableObject menu name from "Saucy" -> "Extensible XP System".
- Updated: Structure of the CHANGELOG file.
- Updated: Image links and minor changes to README and documentation files.

### v1.0.0 (2019-07-24)  
Public release!

- Added: Documentation both in README and a PDF.  
- Added: Credits for RuntimeSet and Square XP formula calculation.
- Fixed: Made XP grant radius field public.
- Updated: Removed duplicate XP formula method.
- Removed: Starting XP until I figured out how to use it properly.


## Credits
- RuntimeSet created by [​Ryan Hipple for Unite 2017](https://github.com/roboryantron/Unite2017/blob/master/Assets/Code/Sets/RuntimeSet.cs)
- Square XP formula created by [​aaaaaaaaaaaa​](https://gamedev.stackexchange.com/questions/13638/algorithm-for-dynamically-calculating-a-level-based-on-experience-points/13639#13639)


## Feedback
If you have any issues please create an issue on the project's repository ([​found here​](https://gitlab.com/Saucyminator/unity-package-extensible-xp-system/issues)).

Suggestions or constructive criticism are appreciated. Pull requests for additional XP formulas, grant/receive methods is also welcomed.


## Contact & Support
Email: ​support@saucy.se  
Website: [​https://saucy.se](https://saucy.se/)  
Repository url: [​https://gitlab.com/Saucyminator/unity-package-extensible-xp-system](https://gitlab.com/Saucyminator/unity-package-extensible-xp-system)  
Issues: [​https://gitlab.com/Saucyminator/unity-package-extensible-xp-system/issues](https://gitlab.com/Saucyminator/unity-package-extensible-xp-system/issues)  
MIT License  
