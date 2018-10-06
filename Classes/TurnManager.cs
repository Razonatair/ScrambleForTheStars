//===================================================
//     Copyright 2018 Dyllin Edward Reid Black
//
//===================================================


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class handles incrementing and storing what turn the game is at.

namespace Scramble
{
    public static class TurnManager
    {
        public static int currentTurn { private set; get; }     //The current turn that the game is at.

        //Initializes the class and prepares it to be used.
        public static void Initialize()
        {
            currentTurn = 0;
        }

        //Ends the current turn of the game.
        public static void EndTurn()
        {
            currentTurn++;
            UnitManager.UpdateMovementPoints();     //Inform the UnitManager to update the movement points of the game's units.
            UnitManager.ProcessOrders();            //Inform it to now process any queued orders the game's units have.
        }
    }
}