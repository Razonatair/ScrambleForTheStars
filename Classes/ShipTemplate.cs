//===================================================
//     Copyright 2018 Dyllin Edward Reid Black
//
//===================================================


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class defines a number of variables common to a certain type of ship, such as a Frigate.

namespace Scramble
{
    public class ShipTemplate
    {
        public string graphicName { private set; get; }         //The name of the material the ship uses.
        public int movementSpeed { private set; get; }          //How fast it can move.
        public bool hyperDrivePresent { private set; get; }     //Whether or not it has a hyperdrive present.
        public int hyperDriveSpeed { private set; get; }        //How fast it can travel through hyperspace.

        private bool finalized;                                 //Whether or not the template is finalized and essentially readonly.

        //The basic constructor for this class.
        public ShipTemplate()
        {
            finalized = false;
            hyperDrivePresent = true; //Temporarily making all ships hyperspace capable.
            hyperDriveSpeed = 5;
        }

        //Sets the graphicName of the template if the template hasn't been finalized.
        public void SetGraphicName(string graphicName)
        {
            if (!finalized)
            {
                this.graphicName = graphicName;
            }
            else
            {
                Debug.Log("Attempting to assign graphicName to finalized ShipTemplate.");
            }
        }

        //Sets the movementSpeed of the template if the template hasn't been finalized.
        public void SetMovementSpeed(int movementSpeed)
        {
            if (!finalized)
            {
                this.movementSpeed = movementSpeed;
            }
            else
            {
                Debug.Log("Attempting to assign movementSpeed to finalized ShipTemplate.");
            }
        }

        //Finalizes the template. No further changes can be made once called.
        public void Finalized()
        {
            finalized = true;
        }
    }
}