//===================================================
//     Copyright 2018 Dyllin Edward Reid Black
//
//===================================================


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The class handles the units of Scramble. It may be likely that this class will
//have to stop being static, depending upon the implementation of multithreading.

namespace Scramble
{
    public static class UnitManager
    {
        public static Ship selectedShip { private set; get; }           //The current ship selected by the player.

        //Called to order the selected ship to attempt to move towards the mouse position.
        public static void OrderToMove(Vector3 worldMousePos)
        {
            //First check if there even is a selected ship.
            if(selectedShip == null)
            {
                //If a ship is not selected, something went wrong.
                Debug.Log("Attempting to order a null ship to move.");
            }
            else
            {
                //Try to add a new MovementOrder to the selected ship. We're trying here because the player might be attempting to move
                //to an index that is null within the star system's array.
                try
                {
                    selectedShip.AddOrder(new MovementOrder(new IntVec2(selectedShip.currentSystem.systemObjects[Mathf.RoundToInt(worldMousePos.x - DisplayManager.drawXOffset) + Settings.systemRadius, Mathf.RoundToInt(worldMousePos.y - DisplayManager.drawYOffset) + Settings.systemRadius].posX,
                                                                        selectedShip.currentSystem.systemObjects[Mathf.RoundToInt(worldMousePos.x - DisplayManager.drawXOffset) + Settings.systemRadius, Mathf.RoundToInt(worldMousePos.y - DisplayManager.drawYOffset) + Settings.systemRadius].posY),
                                                                        selectedShip.currentSystem));
                }
                catch
                {
                    //If the player is trying to move to a null space, we need to not do so.
                    Debug.Log("Attempting to order ship to move to null space.");
                }
            }
        }

        //Sets the selectedShip to a specified Ship.
        public static void SetSelectedShip(Ship selectedShip)
        {
            UnitManager.selectedShip = selectedShip;
        }

        //Called to specifically update the movement points of the selected ship. Temporary.
        public static void UpdateMovementPoints()
        {
            UnitManager.selectedShip.RefreshMovementPoints();
        }

        //Called to specifically process the orders of the selected ship. Temporary.
        public static void ProcessOrders()
        {
            UnitManager.selectedShip.ProcessOrders();
        }
    }
}