//===================================================
//     Copyright 2018 Dyllin Edward Reid Black
//
//===================================================


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The overarching class for units in Scramble. Contains a series of variables and other data
//that is highly important to the functioning of units.

namespace Scramble
{
    public class Ship
    {
        public ShipTemplate rootTemplate { private set; get; }      //The root ShipTemplate of the ship.

        public string customName { private set; get; }              //The custom name of the ship.

        public int rotation { private set; get; }                   //Stores what rotation the ship currently has on the map.
        public int currentMovementSpeed { private set; get; }       //Current movement speed can be altered by loss of engines, etc.
        public int remainingMovementPoints { private set; get; }    //Stores how many movement points the ship has left this turn.
        public bool hyperDriveIsFunctional { private set; get; }    //Determines whether or not the ship's hyperdrive is working.

        public StarSystem currentSystem { private set; get; }       //The current star system the ship is in.
        public IntVec2 currentPosInSys { private set; get; }        //The current position within the star system the ship is at,
                                                                    //in relation to the center of the system.
        public List<Order> orderList { private set; get; }          //A list of Orders that the ship has to work through.

        public int hyperDriveCooldown { private set; get; }         //Determines how long it takes the ship's hyperdrive to cooldown.
        public int turnHyperDriveIsReady { private set; get; }      //Stores what turn the ship's hyperdrive will be ready to be used.

        private bool moving;                                        //Used for pathing purposes.

        //The basic constructor of the class.
        public Ship(string template, StarSystem currentSystem, IntVec2 currentPosInSys)
        {
            rootTemplate = ShipTemplateDict.GetTemplate(template);
            currentMovementSpeed = rootTemplate.movementSpeed;
            remainingMovementPoints = rootTemplate.movementSpeed;
            hyperDriveIsFunctional = true;
            this.currentSystem = currentSystem;
            this.currentPosInSys = currentPosInSys;
        }

        //Returns the turn when the hyperdrive will be ready, or zero if it is ready.
        public int TurnHyperDriveIsReady()
        {
            //If the turn the drive is ready is less than the current turn, it's ready.
            if (turnHyperDriveIsReady < TurnManager.currentTurn)
            {
                return 0;
            }
            else
            {
                //Otherwise, return the turn it will be ready.
                return turnHyperDriveIsReady;
            }
        }

        //Adds an Order to the list of Orders that the ship has.
        public void AddOrder(Order order)
        {
            //First check if the list is null.
            if(orderList == null)
            {
                //If it is, initialize it and add the Order.
                orderList = new List<Order>();
                orderList.Add(order);
            }
            else
            {
                //Otherwise just add the Order.
                orderList.Add(order);
            }

            //If the Order list has exactly one order in it, the ship was just given order, so...
            if (orderList.Count == 1)
            {
                //...check if the Order is a movement Order and that the ship can still move.
                if((orderList[0].orderType == "MovementOrder") && remainingMovementPoints > 0)
                {
                    //Grab a pathfinder and clarify the Order as a MovementOrder.
                    StellarPathfinder pathfinder = StellarPathfinderList.AssignStellarPathfinder();
                    MovementOrder toExecute = (MovementOrder)orderList[0];

                    //Set the ship as moving until otherwise noted.
                    moving = true;
                    
                    //While we're moving...
                    while (moving)
                    {
                        //Path towards the target and decrement the ship's remaining movement points.
                        pathfinder.Path(this, toExecute.targetSystem, toExecute.targetPos);
                        remainingMovementPoints--;

                        //Check if the ship has any movement points left.
                        if (remainingMovementPoints == 0)
                        {
                            //If it does not, stop moving.
                            moving = false;

                            //Check if the ship has reached the target position. If it has, remove the MovementOrder from the list of Orders.
                            if(toExecute.targetPos.x == currentPosInSys.x - Settings.systemRadius && toExecute.targetPos.y == currentPosInSys.y - Settings.systemRadius)
                            {
                                orderList.RemoveAt(0);
                            }
                        }
                        else
                        {
                            //Otherwise, check if the ship has reached the target position. If it has, remove the MovementOrder from the list of Orders and stop moving.
                            //This accounts for the possibility of the ship reaching the target with leftover movement points.
                            if (toExecute.targetPos.x == currentPosInSys.x - Settings.systemRadius && toExecute.targetPos.y == currentPosInSys.y - Settings.systemRadius)
                            {
                                orderList.RemoveAt(0);
                                moving = false;
                            }
                        }
                    }
                }
            }
        }

        //Called whenever the ship exits hyperspace. Essentially starts the hyperdrive's cooldown,
        //and also updates the current system of the ship.
        public void ExitedHyperspace(StarSystem arrivedAtSystem)
        {
            turnHyperDriveIsReady = TurnManager.currentTurn + hyperDriveCooldown;
            currentSystem = arrivedAtSystem;
        }

        //Updates the ship's current position within the system.
        public void UpdateSystemPos(int posX, int posY)
        {
            currentPosInSys.Set(posX + Settings.systemRadius, posY + Settings.systemRadius);
        }

        //Sets the map rotation of the ship.
        public void SetRotation(int rot)
        {
            rotation = rot;
        }

        //Refreshes the movement points of the ship.
        public void RefreshMovementPoints()
        {
            remainingMovementPoints = rootTemplate.movementSpeed;
        }

        //Called whenever the ship is forced to process its Order list.
        public void ProcessOrders()
        {
            //While the ship still has movement points and Orders remain in the list...
            while (remainingMovementPoints > 0 && orderList.Count > 0)
            {
                //Check if the Order at the front of the list is a movement Order.
                if ((orderList[0].orderType == "MovementOrder"))
                {
                    //If it is, grab a pathfinder, and clarify the Order at the front of the list as MovementOrder.
                    StellarPathfinder pathfinder = StellarPathfinderList.AssignStellarPathfinder();
                    MovementOrder toExecute = (MovementOrder)orderList[0];

                    //Sets the ship as moving until otherwise noted.
                    moving = true;

                    //While we're moving...
                    while (moving)
                    {
                        //Path towards the target and decrement the ship's movement points.
                        pathfinder.Path(this, toExecute.targetSystem, toExecute.targetPos);
                        remainingMovementPoints--;

                        //Check if the ship is out of movement points.
                        if (remainingMovementPoints == 0)
                        {
                            //If it has, stop moving.
                            moving = false;

                            //Then check if the target has been reached. If it has, removing the Order from the list.
                            if (toExecute.targetPos.x == currentPosInSys.x - Settings.systemRadius && toExecute.targetPos.y == currentPosInSys.y - Settings.systemRadius)
                            {
                                orderList.RemoveAt(0);
                            }
                        }
                        else
                        {
                            //Otherwise, check if the target has been reached. If it has, remove the Order from the list and stop moving.
                            //This accounts for the ship reaching the target with movement points left over.
                            if (toExecute.targetPos.x == currentPosInSys.x - Settings.systemRadius && toExecute.targetPos.y == currentPosInSys.y - Settings.systemRadius)
                            {
                                orderList.RemoveAt(0);
                                moving = false;
                            }
                        }
                    }
                }
            }
        }
    }
}