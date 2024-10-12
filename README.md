<p align="center">
  <img width="100%" alt="prostir" src="https://github.com/fajarnadril/Project-Stir/assets/36891062/612d0ce6-1bc7-4727-869b-f1d3762496c7">
  </br>
</p>

## 🔴About
**ProjectStir [Pre-Alpha]** is a driving simulation application meticulously crafted to assess driving skills performance using a game-centric approach. Within this application, drivers or users are tasked with navigating a cityscape, all the while gathering vital checkpoints. The performance evaluation revolves around quantifying the number of **checkpoints** successfully acquired within specific time constraints. This performance assessment also encompasses a metric denoted as **damage**, which quantifies the extent of vehicular wear and tear sustained throughout the driving experience.

The game is set against an urban backdrop in Asia, replete with one-way streets capable of accommodating only one vehicle at any given moment. Enhanced user performance, characterized by a greater accumulation of checkpoints and minimal vehicle damage, leads to superior overall outcomes. Project-Stir can be operated using various input devices, including a keyboard, joystick, or steering wheel controller.

Within this gaming environment, an user data collection process is employed, employing a Leaderboard-based, game-oriented methodology. The feature for gathering performance data is initiated through data submission following a player's successful game completion. The variables collected encompass **Player Nickname**, **Checkpoint**, and **Damage** data. This performance data is seamlessly integrated with Looker Studio, a public-accessible data visualization tool. This data collection methodology is easily accessible for entertainment purposes or, if necessary, for research applications. Access to this data is available via https://lookerstudio.google.com/reporting/b00ab0ae-0caf-4b2b-855f-a3c7b88dc9f7



<br>

## 🕹️Download Game
Itch.io : https://binusgat.itch.io/project-stir

<br>

## ▶️ Video Gameplay
<img src="https://github.com/KXLVXN7/KXLVXN7/blob/main/gif/pstir.gif" alt="1" style="width:50%;height:auto;">
View Full Gameplay : https://youtu.be/UWn3G_PZ_fA


<br>

## 👤Developer & Contributions
- Kelvin - Unity Programmer Main Mechanic
- David Ang - Unity Programmer Game Mechanic 
- Muhamad Fajar - Unity Technical Artist
- Thomas Galih Satria - Visual Designer
- Galih Dea - Game Designer

<br>

## 📂Files description

```
├── Project-Stir                      # In this Folder, containing all the Unity project files, to be opened by a Unity Editor
   ├── ...
   ├── Assets                         #  In this Folder, it contains all our code, assets, scenes, etcwas not automatically created by Unity
      ├── ...
      ├── 3rdParty                   # In this folder, there are several packages that you must add via Unity Package Manager
      ├── Scenes                     # In this folder, there are scenes. You can open these scenes to play the game via Unity
      ├── ....
   ├── ...
      
```
<br>

## 🕹️Game controls

The following controls are bound in-game, for gameplay and testing.

| Key Binding       | Function          |
| ----------------- | ----------------- |
| W,A,S,D           | Standard movement |
| F             | NOS              |
| Space             | Hand Break            |

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
