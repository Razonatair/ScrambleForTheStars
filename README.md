# ScrambleForTheStars
Contained in this repository are the classes of "Scramble for the Stars", an ultra-alpha prototype for an upcoming multi-threaded, turn-based strategy game in the vein of GalCiv 3 and Space Empires 4/5, written in C# using the Unity engine for rendering purposes only. I'm not really using the GameObject framework of Unity or much else that Unity provides except for loading textures. It's much faster to not use the heavy GameObject framework when the game will be dealing with thousands to hundreds of thousands of objects. It's an objective of mine to also make the game incredibly moddable, to the point that it can basically serve as it's own space strategy game engine for people to tinker with. To this end, I'm working on making most data the game reads from for things like units exposed to the user in XML files.

The prototype works on Windows as far as I tested, both in windowed and fullscreen mode. Sorry Mac and Linux users. The prototype consists of the entire zip archive.

CONTROLS:
Left click to select things, ideally the ship you see in the middle.
Right click to give orders to the ship.
Middle click (I tested with my scrollwheel, your middle mouse might be different?) allows you to drag the view around.
The Enter key ends the turn, refreshing the ship's movement points.
The Escape key ends the program.

I know that the selection icon doesn't follow the ship, and I also know there is weird artifacting when the view goes beyond the edge of the star system. Both are expected. Ideally, it should be nearly impossible for you to break the game without messing directly with the files. I invite you to try to break it within the bounds of the game, unless it just starts and there's nothing but a black screen immediately. I'm pretty sure I fixed that problem though.

By the way, you can customize the movement speed of the ship already by altering /Assets/Resources/Defs/ShipDefs.xml and changing the movementSpeed integer. I wouldn't recommend changing any other values in any of the other folders.

Up to date as of 10/6/18.

This repository exists merely for demonstration and recruiting purposes. Most of this code is straightforward if you give some thought to it, but I'd still you rather not copy it for your purposes as I intend to develop this game to the point that I can market and sell it on platforms such as Steam.
