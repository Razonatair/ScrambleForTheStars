//===================================================
//     Copyright 2018 Dyllin Edward Reid Black
//
//===================================================


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This class handles pathfinding on the stellar scale, meaning that it won't handle battle pathfinding.
 * At least, that is the current concept for now.*/

namespace Scramble
{
    public class StellarPathfinder
    {
        private IntVec2 pathPos;                    //A custom object that just contains two integers.
        private List<IntVec2> generatedPath;        //A list of IntVec2 that would act as a series of directions where to move.

        private Ship selectedShip;                  //A temporary reference to the ship we're working with.
        private StarSystem targetSystem;            //A reference to the star system the ship wants to go to.
        private IntVec2 targetPos;                  //Where the ship is going to within the system it is going to.
        private int systemRadius;                   //A recorded reference to Settings.systemRadius, used since I think it may be faster.

        //The constructor for this class. Just assigns systemRadius for now.
        public StellarPathfinder()
        {
            systemRadius = Settings.systemRadius;
        }

        //The method that gets called whenever anything wants to path.
        public void Path(Ship ship, StarSystem system, IntVec2 pos)
        {
            //The pathfinder once built a list of positions to move to recursively, but I decided against that,
            //for now at least.
            generatedPath = new List<IntVec2>();

            selectedShip = ship;
            targetSystem = system;
            targetPos = pos;

            //Stores where we're at at the current moment, converted from raw ship position in the array to 'true' position in the star system.
            pathPos = new IntVec2(selectedShip.currentPosInSys.x - systemRadius, selectedShip.currentPosInSys.y - systemRadius);

            //First of all, if we're already in the target star system, there's no point trying to path interstellarly.
            if (targetSystem == selectedShip.currentSystem)
            {
                if (pathPos.x != targetPos.x || pathPos.y != targetPos.y) //Stops the game from pathing unnecessarily if the ship is already at the target.
                {
                    PathInSystem();
                }
            }
            else
            {
                //For interstellar pathfinding, a working hyperdrive is necessary.
                if(selectedShip.hyperDriveIsFunctional)
                {
                    PathBetweenSystems(); //I haven't yet started work on this function.
                }
                else
                {
                    Debug.Log("Attempting to path between Star Systems with hyperspace incapable ship.");
                }
            }
        }

        //Handles pathfinding within a system. Pathfinding in space while ignoring orbital mechanics is fairly simple
        //if you assume the ship can't run into anything.
        private void PathInSystem()
        {
            //First, check is the target is left or right of me.
            if (pathPos.x > targetPos.x)
            {
                //Then check if the target is above or below me.
                if (pathPos.y > targetPos.y)
                {
                    //Target is left and down of me, check if I can move left and down. Ships cannot travel beyond the system radius without
                    //entering hyperspace and jumping to another star system.
                    if (((pathPos.x - 1) * (pathPos.x - 1)) + ((pathPos.y - 1) * (pathPos.y - 1)) < (systemRadius * systemRadius))
                    {
                        //Left and down is a valid move.
                        selectedShip.currentSystem.systemObjects[pathPos.x + systemRadius, pathPos.y + systemRadius].RemoveShip(selectedShip); //I may combine this method and the following eventually.
                        selectedShip.currentSystem.systemObjects[pathPos.x + systemRadius - 1, pathPos.y + systemRadius - 1].AddShip(selectedShip);
                        selectedShip.SetRotation(135);  //Sets the rendered rotation of the ship.
                    }
                    else
                    {
                        //Left and down is not a valid move, move left instead.
                        selectedShip.currentSystem.systemObjects[pathPos.x + systemRadius, pathPos.y + systemRadius].RemoveShip(selectedShip);
                        selectedShip.currentSystem.systemObjects[pathPos.x + systemRadius - 1, pathPos.y + systemRadius].AddShip(selectedShip);
                        selectedShip.SetRotation(90);

                    }
                }
                else
                {
                    //Check if the target is on the same level as me.
                    if (pathPos.y == targetPos.y)
                    {
                        //Target is left and on my level. Moving left is definitely a valid move.
                        selectedShip.currentSystem.systemObjects[pathPos.x + systemRadius, pathPos.y + systemRadius].RemoveShip(selectedShip);
                        selectedShip.currentSystem.systemObjects[pathPos.x + systemRadius - 1, pathPos.y + systemRadius].AddShip(selectedShip);
                        selectedShip.SetRotation(90);
                    }
                    else
                    {
                        //Target is left and above me. Check if I can move left and up.
                        if (((pathPos.x - 1) * (pathPos.x - 1)) + ((pathPos.y + 1) * (pathPos.y + 1)) < (systemRadius * systemRadius))
                        {
                            //Moving left and up is a valid move.
                            selectedShip.currentSystem.systemObjects[pathPos.x + systemRadius, pathPos.y + systemRadius].RemoveShip(selectedShip);
                            selectedShip.currentSystem.systemObjects[pathPos.x + systemRadius - 1, pathPos.y + systemRadius + 1].AddShip(selectedShip);
                            selectedShip.SetRotation(45);
                        }
                        else
                        {
                            //Moving left and up is not a valid move, move left instead.
                            selectedShip.currentSystem.systemObjects[pathPos.x + systemRadius, pathPos.y + systemRadius].RemoveShip(selectedShip);
                            selectedShip.currentSystem.systemObjects[pathPos.x + systemRadius - 1, pathPos.y + systemRadius].AddShip(selectedShip);
                            selectedShip.SetRotation(90);
                        }
                    }
                }
            }
            else
            {
                //Check if the target is directly above or below me.
                if (pathPos.x == targetPos.x)
                {
                    //Check if the target is below me.
                    if (pathPos.y > targetPos.y)
                    {
                        //Target is on same X and below me. Moving down is definitely a valid move.
                        selectedShip.currentSystem.systemObjects[pathPos.x + systemRadius, pathPos.y + systemRadius].RemoveShip(selectedShip);
                        selectedShip.currentSystem.systemObjects[pathPos.x + systemRadius, pathPos.y + systemRadius - 1].AddShip(selectedShip);
                        selectedShip.SetRotation(180);
                    }
                    else
                    {
                        //Target is on same X and above me, since if it were same X && Y, would not have reached this point of code.
                        //Moving up is definitely a valid move.
                        selectedShip.currentSystem.systemObjects[pathPos.x + systemRadius, pathPos.y + systemRadius].RemoveShip(selectedShip);
                        selectedShip.currentSystem.systemObjects[pathPos.x + systemRadius, pathPos.y + systemRadius + 1].AddShip(selectedShip);
                        selectedShip.SetRotation(0);
                    }
                }
                else
                {
                    //Check if the target is below me.
                    if (pathPos.y > targetPos.y)
                    {
                        //Target is right and below me. Check if I can move right and down.
                        if (((pathPos.x + 1) * (pathPos.x + 1)) + ((pathPos.y - 1) * (pathPos.y - 1)) < (systemRadius * systemRadius))
                        {
                            //Moving right and down is a valid move.
                            selectedShip.currentSystem.systemObjects[pathPos.x + systemRadius, pathPos.y + systemRadius].RemoveShip(selectedShip);
                            selectedShip.currentSystem.systemObjects[pathPos.x + systemRadius + 1, pathPos.y + systemRadius - 1].AddShip(selectedShip);
                            selectedShip.SetRotation(225);
                        }
                        else
                        {
                            //Moving right and down is not a valid move, move right.
                            selectedShip.currentSystem.systemObjects[pathPos.x + systemRadius, pathPos.y + systemRadius].RemoveShip(selectedShip);
                            selectedShip.currentSystem.systemObjects[pathPos.x + systemRadius + 1, pathPos.y + systemRadius].AddShip(selectedShip);
                            selectedShip.SetRotation(270);
                        }
                    }
                    else
                    {
                        //Check if the target is on the same level as me.
                        if (pathPos.y == targetPos.y)
                        {
                            //Target is right and on my level. Moving right is definitely a valid move.
                            selectedShip.currentSystem.systemObjects[pathPos.x + systemRadius, pathPos.y + systemRadius].RemoveShip(selectedShip);
                            selectedShip.currentSystem.systemObjects[pathPos.x + systemRadius + 1, pathPos.y + systemRadius].AddShip(selectedShip);
                            selectedShip.SetRotation(270);
                        }
                        else
                        {
                            //Target is right and above me, check if I can move right and up.
                            if (((pathPos.x + 1) * (pathPos.x + 1)) + ((pathPos.y + 1) * (pathPos.y + 1)) < (systemRadius * systemRadius))
                            {
                                //Moving right and up is a valid move.
                                selectedShip.currentSystem.systemObjects[pathPos.x + systemRadius, pathPos.y + systemRadius].RemoveShip(selectedShip);
                                selectedShip.currentSystem.systemObjects[pathPos.x + systemRadius + 1, pathPos.y + systemRadius + 1].AddShip(selectedShip);
                                selectedShip.SetRotation(315);
                            }
                            else
                            {
                                //Moving right and up is not a valid move, move right instead.
                                selectedShip.currentSystem.systemObjects[pathPos.x + systemRadius, pathPos.y + systemRadius].RemoveShip(selectedShip);
                                selectedShip.currentSystem.systemObjects[pathPos.x + systemRadius + 1, pathPos.y + systemRadius].AddShip(selectedShip);
                                selectedShip.SetRotation(270);
                            }
                        }
                    }
                }
            }
        }

        //UNTESTED recursive function to build a path to follow. Probably broken, and not
        //as well commented.
        private void PathInSystemRecursively()
        {
            if (pathPos.x != targetPos.x && pathPos.y != targetPos.y) //Don't bother continuing to path if I'm already at the target.
            {
                if (pathPos.x > targetPos.x)
                {
                    if (pathPos.y > targetPos.y)
                    {
                        //Target is left and above me.
                        if (((pathPos.x - 1) * (pathPos.x - 1)) + ((pathPos.y - 1) * (pathPos.y - 1)) < (systemRadius * systemRadius))
                        {
                            //Left and down is a valid move.
                            generatedPath.Add(new IntVec2(pathPos.x - 1, pathPos.y - 1));
                            pathPos.x--;
                            pathPos.y--;
                            PathInSystemRecursively();
                        }
                        else
                        {
                            //Left and down is not a valid move, move left instead.
                            generatedPath.Add(new IntVec2(pathPos.x - 1, pathPos.y));
                            pathPos.x--;
                            PathInSystemRecursively();
                        }
                    }
                    else
                    {
                        if (pathPos.y == targetPos.y)
                        {
                            //Target is left and on my level. Moving left is definitely a valid move.
                            generatedPath.Add(new IntVec2(pathPos.x - 1, pathPos.y));
                            pathPos.x--;
                            PathInSystemRecursively();
                        }
                        else
                        {
                            //Target is left and above me.
                            if (((pathPos.x - 1) * (pathPos.x - 1)) + ((pathPos.y + 1) * (pathPos.y + 1)) < (systemRadius * systemRadius))
                            {
                                //Moving left and up is a valid move.
                                generatedPath.Add(new IntVec2(pathPos.x - 1, pathPos.y + 1));
                                pathPos.x--;
                                pathPos.y--;
                                PathInSystemRecursively();
                            }
                            else
                            {
                                //Moving left and up is not a valid move, move left instead.
                                generatedPath.Add(new IntVec2(pathPos.x - 1, pathPos.y));
                                pathPos.x--;
                                PathInSystemRecursively();
                            }
                        }
                    }
                }
                else
                {
                    if (pathPos.x == targetPos.x)
                    {
                        if (pathPos.y > targetPos.y)
                        {
                            //Target is on same X and below me. Moving down is definitely a valid move.
                            generatedPath.Add(new IntVec2(pathPos.x, pathPos.y - 1));
                            pathPos.y--;
                            PathInSystemRecursively();
                        }
                        else
                        {
                            //Target is on same X and above me, since if it were same X && Y, would not have reached this point of code.
                            //Moving up is definitely a valid move.
                            generatedPath.Add(new IntVec2(pathPos.x, pathPos.y + 1));
                            pathPos.y++;
                            PathInSystemRecursively();
                        }
                    }
                    else
                    {
                        if (pathPos.y > targetPos.y)
                        {
                            //Target is right and below me.
                            if (((pathPos.x + 1) * (pathPos.x + 1)) + ((pathPos.y - 1) * (pathPos.y - 1)) < (systemRadius * systemRadius))
                            {
                                //Moving right and down is a valid move.
                                generatedPath.Add(new IntVec2(pathPos.x + 1, pathPos.y - 1));
                                pathPos.x++;
                                pathPos.y--;
                                PathInSystemRecursively();
                            }
                            else
                            {
                                //Moving right and down is not a valid move, move right.
                                generatedPath.Add(new IntVec2(pathPos.x + 1, pathPos.y));
                                pathPos.x++;
                                PathInSystemRecursively();
                            }
                        }
                        else
                        {
                            if (pathPos.y == targetPos.y)
                            {
                                //Target is right and on my level. Moving right is definitely a valid move.
                                generatedPath.Add(new IntVec2(pathPos.x + 1, pathPos.y));
                                pathPos.x++;
                                PathInSystemRecursively();
                            }
                            else
                            {
                                //Target is right and above me.
                                if (((pathPos.x + 1) * (pathPos.x + 1)) + ((pathPos.y + 1) * (pathPos.y + 1)) < (systemRadius * systemRadius))
                                {
                                    //Moving right and up is a valid move.
                                    generatedPath.Add(new IntVec2(pathPos.x + 1, pathPos.y + 1));
                                    pathPos.x++;
                                    pathPos.y++;
                                    PathInSystemRecursively();
                                }
                                else
                                {
                                    //Moving right and up is not a valid move, move right.
                                    generatedPath.Add(new IntVec2(pathPos.x + 1, pathPos.y));
                                    pathPos.x++;
                                    PathInSystemRecursively();
                                }
                            }
                        }
                    }
                }
            }
        }

        //Called whenever a ship needs to travel between star systems. For now, this method is unbuilt.
        private void PathBetweenSystems()
        {
            //Need to handle pathing between systems here, and take into account mass relay type
            //structures as well eventually.
        }
    }
}