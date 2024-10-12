## 🔴About
Sive 2, depicting a vast survival game where you need to manage your hunger and thirst to continue your journey. You objective is Hunt for your survivability and to escape the dangerous island. I forged the HUD, inventory system, hotbar shortcut, Drag and drop handler, Slot generator, Item scriptable object, and also created the terrain. Here's a small portion details about Sive2 development presented
<br>

## 🕹️Play Game
Currently in development phase
<br>

## 👤Developer & Contributions
- Nicholas Van Lukman (Game Programmer)
<br>

## 📂Files description

```
├── Sive2                             # Contain everything needed for Please Survive to works.
   ├── .vscode                        # Contains configuration files for Visual Studio Code (VSCode) when it's used as the code editor for the project.
      ├── extensions.json             # Contains settings and configurations for debugging, code formatting, and IntelliSense. This folder is related to Visual Studio Code integration.
      ├── launch.json                 # Contains the configuration necessary to start debugging Unity C# scripts within VSCode.                     
      ├── setting.json                # Contains workspace-specific settings for VSCode that are applied when working within the Unity project.
   ├── Assets                         # Contains every assets that have been worked with unity to create the game like the scripts and the art.
      ├── Art                         # Contains all the game art like the sprites, background, even the character.
      ├── Animation                   # Contains every animation clip and animator controller that played when the game start.
      ├── Sounds and Music            # Contains every sound used for the game like music and sound effects.
      ├── Scripts                     # Contains all scripts needed to make the gane get goings like PlayerMovement scripts.
      ├── Prefabs                     # Contains every pre-configured, reusable game object that you can instantiate (create copies of) in your game scene.
      ├── Scenes                      # Contains all scenes that exist in the game for it to interconnected with each other like MainMenu, Gameplay, etc
      ├── ThirdParty Packages         # Contains the Package Manager from unity registry or unity asset store assets for game purposes.
      ├── Items                       # Contains items in game survival, Saved data as scriptableobject for easier sorting out.
      ├── Recipes                     # Contains recipe for the survival game and the scriptableobject data to match the craftable tools.
      ├── Models                      # Contains models for every 3D object in the game like the drop bags, the fps arms, even storage box.
   ├── Packages                       # Contains game packages that responsible for managing external libraries and packages used in your project.
      ├── Manifest.json               # Contains the lists of all the packages that your project depends on and their versions.
      ├── Packages-lock.json          # Contains packages that ensuring your project always uses the same versions of all dependencies and their sub-dependencies.
   ├── Project Settings               # Contains the configuration of your game to control the quality settings, icon, or even the cursor settings
├── README.md                         # The description of Please Survive file from About til the developers and the contribution for this game.
```
      

<br>

## 🕹️Game controls

The following controls are bound in-game, for gameplay and testing.

| Key Binding       | Function          |
| ----------------- | ----------------- |
| W,A,S,D           | Standard movement |
| Tab             | Inventory              |
| Space             | Jump           |

<br>

##  📜Scripts and Features

- In this game, we collect player performance data and store it in Google Sheets using Unity Networking WWW by submitting a form.
- The leaderboard is sorted A-Z and handled by Looker Studio for display on the screen.
- The Saturation Changer is used to control the environment’s tone and weather effects through color adjustments in the game.

|  Script       | Description                                                  |
| ------------------- | ------------------------------------------------------------ |
| `GameManager.cs` | Manages the game flow such as timers, difficulty levels, networking, etc. |
| CheckPointManager.cs | Handles the location and management of checkpoints in the game. |
| `SaturationChanger.cs`  | Controls the saturation levels of the 3D environment via post-processing. |
| `UIHandler.cs`  | Manages various UI elements and organizes them into sequences. |
| `etc`  | |

<br>

## 🔥How to open up the project on Unity Editor
This game was developed using **Unity Editor 2021.3.11f1**, and we recommend that you download this specific version because using different ones, especially older versions, might result in problems

![image](https://github.com/fajarnadril/Project-Stir/assets/36891062/1d44502b-1dfb-424c-97d1-6b1a93616ffc)


You are **required to download several assets from the Unity Asset Store** to properly operate this game. All assets should be placed in the **3rdParty** folder. The assets that need to be downloaded are as follows:

**Download Here:**
- Japanese City Megapack (URP 2020) : https://assetstore.unity.com/packages/3d/environments/japanese-city-modular-pack-v1-4-239043
- Logitech SDK : https://assetstore.unity.com/packages/tools/integration/logitech-gaming-sdk-6630
- RainMaker :  https://assetstore.unity.com/packages/vfx/particles/environment/rain-maker-2d-and-3d-rain-particle-system-for-unity-34938
- RealisticCarControllerV3 : https://assetstore.unity.com/packages/tools/physics/realistic-car-controller-16296
