//===================================================
//     Copyright 2018 Dyllin Edward Reid Black
//
//===================================================


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The class currently handles all input from the user, both mouse and keyboard.

namespace Scramble
{
    public static class InputManager
    {
        private static Vector3 worldMousePos;           //The position of the mouse within the game world.
        private static Vector3 savedWorldMousePos;      //A saved position of where the mouse was within the game world.
        private static float zoomAxis;                  //Measures the axis of the scrollwheel for camera zoom purposes.

        private static bool isMiddleMouseDragging;      //Defines whether or not the player is dragging the middle mouse.

        //Called every frame by the Game.
        public static void Step()
        {
            //Assign the position of the mouse to the world position.
            worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //Check for left clicks.
            if (Input.GetMouseButtonDown(0))
            {
                //Attempt to assign a StellarObject at the selected position to the selectedStellarObject.
                DisplayManager.SetSelectedStellarObject(worldMousePos);
            }

            //Check for right clicks.
            if(Input.GetMouseButtonDown(1))
            {
                //Attempt to order a unit to move to the mouse position.
                UnitManager.OrderToMove(worldMousePos);
            }

            //Check for a modified scrollwheel axis.
            if(Input.GetAxis("Mouse ScrollWheel") != zoomAxis)
            {
                //Alter the zoom of the camera.
                zoomAxis = Input.GetAxis("Mouse ScrollWheel");
                DisplayManager.AlterCamOrthoSize(Input.GetAxis("Mouse ScrollWheel"));
            }

            //Check for the scrollwheel being pressed.
            if (Input.GetMouseButtonDown(2))
            {
                //Start middle mouse dragging, and save the mouse position.
                isMiddleMouseDragging = true;
                savedWorldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            //Check if we're still middle mouse dragging for this frame.
            if (isMiddleMouseDragging == true)
            {
                MiddleMouseDrag();
            }
            //Check if the middle mouse button was released.
            if (Input.GetMouseButtonUp(2))
            {
                //If it is, we're going to stop middle mouse dragging.
                isMiddleMouseDragging = false;
            }

            //Check if the enter key has been pressed.
            if (Input.GetKeyDown(KeyCode.Return))
            {
                //For now, just end the turn if it has.
                TurnManager.EndTurn();
            }

            //Check if the escape key has been pressed.
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                //Quit the application if it is.
                Application.Quit();
            }
                    
        }

        //Called when the player is dragging the middle mouse.
        private static void MiddleMouseDrag()
        {
            //Call the DisplayManager to update the drawing offsets.
            DisplayManager.AddToDrawOffsets(((savedWorldMousePos.x - worldMousePos.x) / 30f), ((savedWorldMousePos.y - worldMousePos.y) / 15f));
        }
    }
}