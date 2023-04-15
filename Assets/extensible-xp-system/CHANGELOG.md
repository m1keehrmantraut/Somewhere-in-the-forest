# Changelog

## v1.0.4 (2020-06-10)
- Fixed: Warnings in the package.
- Fixed: RequiredXP not being initialized causing a "Max level reached" failure.


## v1.0.3 (2020-05-23)
- Fixed: Files are now in the example sample. Unity Package Manager made the scene read-only which made it impossible to open.


## v1.0.2 (2020-05-23)
- Updated: Installation and How-To-Use information in README and documentation files.
- Added: FAQ information in the demo scene.
- Cleanup: Re-restructure folders (again).
- Cleanup: Moved everything under the same namespace.
- Fixed: Dotfiles to help preserve .meta files.
- Removed: Unused Tests.


## v1.0.1 (2020-05-19)
- Updated: Remade everything as a Unity Package instead of a unity project (removed unnecessary files, restructured all folders, etc).
- Updated: Renamed ScriptableObject menu name from "Saucy" -> "Extensible XP System".
- Updated: Structure of the CHANGELOG file.
- Updated: Image links and minor changes to README and documentation files.


## v1.0.0 (2019-07-24)  
Public release!

- Added: Documentation both in README and a PDF.  
- Added: Credits for RuntimeSet and Square XP formula calculation.
- Fixed: Made XP grant radius field public.
- Updated: Removed duplicate XP formula method.
- Removed: Starting XP until I figured out how to use it properly.


## v0.1.1 (2019-07-23)
- Fixed: Code now compatible with versions v5.6.7f1 and v2017.4.30f1 ([2ecbf2c8](https://gitlab.com/Saucyminator/unity-package-extensible-xp-system/commit/2ecbf2c8a7d1347bba436e17d75de0f0fa20987a))
  - **NOTE**: For versions v5.6.7f1 or v2017.4.30f1 the prefabs does not work because of Unity's new prefab workflow introduced in v2018.3. Code runs fine though. If you are running these versions just skip importing the prefabs.


## v0.1.0 (2019-07-23)
- Initial commit.
- Five different XP formulas available.
- Two different XP grant methods available.
- Two different XP receive methods available (where XP is saved).
