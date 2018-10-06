//===================================================
//     Copyright 2018 Dyllin Edward Reid Black
//
//===================================================


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//One of the few actual GameObjects that Scramble uses. This class is essentially the root of
//all of classes, the overlord of the entire program.

namespace Scramble
{
    public class Game : MonoBehaviour
    {
        //Called upon starting by the Unity engine.
        void Start()
        {
            Settings.LoadSettings();                    //Loads the settings of the game.

            QualitySettings.vSyncCount = 60;             //Sets the frame rate cap used by Unity.

            StellarObjectTemplateDict.Initialize();     //Starts up all the crucial dictionaries we need.
            ShipTemplateDict.Initialize();
            MatDict.Initialize();

            StellarPathfinderList.Initialize();         //Starts up the list of StellarPathfinders.

            TemplateManager.BuildDicts();               //Orders the TemplateManager to build all the template dictionaries.

            DisplayManager.CreateDrawMesh();            //Order the DisplayManager to draw the mesh that is used for rendering.

            Galaxy.RandomGenerate(1, 1);                //Orders the Galaxy to randomly generate a 1 by 1 galaxy.

            //For testing purposes, add a simple ship object to the center of the only star system in the galaxy.
            Galaxy.galaxyArray[0, 0].systemObjects[15, 15].AddShip(new Ship("BasicShip", Galaxy.galaxyArray[0, 0], new IntVec2(15,15)));
        }

        //Called every frame by the Unity engine.
        void Update()
        {
            InputManager.Step();                        //Orders the InputManager to run this frame.
            

            DisplayManager.DrawPlayScreen();            //Orders the DisplayManager to draw the next frame.
        }
    }
}