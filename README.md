# ScrambleForTheStars
Contained in this repository are publicly shown classes for an upcoming multi-threaded, turn-based strategy game in the vein of GalCiv 3 and Space Empires 4/5, written in C# using the Unity engine for rendering purposes only. I'm not really using the GameObject framework of Unity or much else that Unity provides except loading textures from Resources folders. It's much faster to not use the heavy GameObject framework when the game is dealing with thousands to hundreds of thousands of objects.

DisplayManager primarily deals with rendering the player's screen and what they can interact with. It's mainly a demonstration of rendering in Unity using a non-Game Object framework and instead directly calling the GPU to render meshes.

StellarPathfinder contains the methods called for pathfinding around the game world. Eventually when the game is multi-threaded, there will likely be a StellarPathfinder for each player/AI in order to ensure thread safety. That's why I haven't written that class to be static.

This repository exists merely for demonstration purposes. Most of this code is straightforward if you give some thought to it, but I'd still you rather not copy it for your purposes as I intend to develop this game to the point that I can market and sell it on platforms such as Steam.
