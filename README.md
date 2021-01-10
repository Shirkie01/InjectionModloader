# InjectionModloader
A basic injection mod loader for non-IL2CPP Unity games.

## How to Use
Copy InjectionModLoaderFW.dll to the [GAME]_Data/Managed folder. Create a "Mods" folder in the game's root directory. Place any mods in this folder.

Use an IL editor to inject a call to ModLoader.LoadMods() in Assembly-CSharp.dll.
