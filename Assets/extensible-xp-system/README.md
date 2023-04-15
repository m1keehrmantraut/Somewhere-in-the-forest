# Extensible XP System (v1.0.2)
Extensible XP System aims for a simple XP system that you can expand upon. Quickly change XP formulas and how the XP should be calculated.


## How to install
You can either:

- (Recommended) Install it through the Unity Package Manager.
- Clone the repository.
- Download the .unitypackage from the repository [located under Releases](https://gitlab.com/Saucyminator/unity-package-extensible-xp-system/-/releases).
- Download it via the [asset store](https://assetstore.unity.com/packages/slug/151021).


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
More in-depth documentation is [available here](Documentation~/Extensible%20XP%20System.md).


## Credits
- RuntimeSet created by [Ryan Hipple for Unite 2017](https://github.com/roboryantron/Unite2017/blob/master/Assets/Code/Sets/RuntimeSet.cs).
- Square XP formula created by [aaaaaaaaaaaa](https://gamedev.stackexchange.com/a/13639).


## Feedback
If you have any issues please create an issue.

Suggestions or constructive criticism are appreciated. Pull requests for additional XP formulas, grant/receive methods is also welcomed.


## Changelog
For changes please view the [CHANGELOG](CHANGELOG.md) file.


## License
[MIT License](LICENSE.md)
