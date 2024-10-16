<p align="center">
  <img width="100%" alt="prostir" src="https://github.com/user-attachments/assets/43ee9a35-2e1c-4c84-a264-a39b1ebcd7fb">
  </br>
</p>

<h1>🍖Sive 2</h1>
</br>




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
| R             | Reload             |
| Left Ctrl             | Crouch              |
| Mouse Click             | Craft, Shoot, etc              |

<br>

##  📜Scripts and Features

You are able to do so many stuff in the game like walking, running, building, crafting, shooting, hunting, and so much more thanks to tons of scripting has been implemented to the game

|  Script       | Description                                                  |
| ------------------- | ------------------------------------------------------------ |
| `InventoryManager.cs` | Responsible for all the inventory things in game like drag and drop, hotbar, etc |
| `ItemScriptableObject.cs` | Responsible for all the items data in Sive2 for example wood, guns, stone, etc |
| `Weapon.cs`  | Responsible for all in game weapon including melee weapon, controlling reload, swing, etc |
| `BuildingHandler.cs`  | Responsible for the building system in game using socket system |
| `CraftingManager.cs`  | Responsible for all the items required and crafting system working in game|
| `etc`  | |

<br>
