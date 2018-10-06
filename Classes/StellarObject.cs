//===================================================
//     Copyright 2018 Dyllin Edward Reid Black
//
//===================================================


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class defines an instantiated StellarObject such as a Planet, Moon,
//or EmptySpace. It is likely that this class will be overhauled if Tiles are
//implemented 'beneath' StellarObjects in star systems' maps.

namespace Scramble
{
    public class StellarObject
    {
        public int posX { protected set; get; }                             //The position of the StellarObject within the
        public int posY { protected set; get; }                             //star System, zero being the center of the system.

        public string name { protected set; get; }                          //The custom name of the StellarObject.

        public StellarObjectTemplate rootTemplate { protected set; get; }   //The StellarObjectTemplate that this StellarObject is defined
                                                                            //by, such as a Planet or Star.

        public StarSystem rootSystem { protected set; get; }                //The star system this StellarObject is a part of.

        private List<Ship> shipsHere;                                       //A list of ships present in the StellarObject.
        private int displayedShip;                                            //Which ship to display out of the list of ships.

        //Adds a specified ship to the list of ships in the StellarObject.
        public void AddShip(Ship ship)
        {
            //First check if the list is null.
            if(shipsHere == null)
            {
                //If it is, add the ship to the list after instantiating the list, 
                //then update the ship's position to this StellarObject's position.
                shipsHere = new List<Ship>();
                shipsHere.Add(ship);
                ship.UpdateSystemPos(posX, posY);
            }
            else
            {
                //If the ship already exists, add the ship and update its position.
                shipsHere.Add(ship);
                ship.UpdateSystemPos(posX, posY);
            }
        }

        //Remove a specified ship from the system.
        public void RemoveShip(Ship ship)
        {
            //If the list of ships is null, something went wrong.
            if(shipsHere == null)
            {
                Debug.Log("A ship is not present in StellarObject " + posX + ", " + posY);
            }
            else
            {
                //Check to see if the specified ship inhabits the list of ships here.
                if(shipsHere.Contains(ship))
                {
                    //If it does, remove it.
                    shipsHere.Remove(ship);
                    //Furthermore, if the number of ships present is now zero,
                    //we want to stop using the memory that the List once occupied.
                    //However, I think C# won't release the memory until
                    //the garbage collector is activated unfortunately.
                    if(shipsHere.Count == 0)
                    {
                        shipsHere = null;
                    }
                }
                else //Otherwise, the ship wasn't found in the list, so something went wrong.
                {
                    Debug.Log(ship.customName + " not found in StellarObject " + posX + ", " + posY);
                }
            }
        }

        //Returns whether or not any ships are present in the StellarObject.
        public bool ShipsPresent()
        {
            if(shipsHere == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //Returns whatever ship is to be displayed by the game.
        public Ship GetDisplayedShip()
        {
            //If there aren't any ships here, then something went wrong.
            if (shipsHere == null)
            {
                Debug.Log("There are no ships in StellarObject " + posX + ", " + posY);
                return null; 
            }
            else //Otherwise, return the displayed ship from the list of ships.
            {
                return shipsHere[displayedShip];
            }
        }

        //This method is called to cycle through the displayed ships.
        public void IncrementDisplayedShip()
        {
            //If there aren't anything ships here, something went wrong.
            if(shipsHere == null)
            {
                Debug.Log("There is no reason to increment the displayed ship in StellarObject " + posX + ", " + posY);
                return;
            }
            else //Otherwise, increment the displayed ship.
            {
                displayedShip++;
                if(displayedShip > shipsHere.Count)
                {
                    //If the incremented value exceeds the number of ships present, loop back to zero.
                    displayedShip = 0;
                }
            }
        }
    }
}