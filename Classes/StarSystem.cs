//===================================================
//     Copyright 2018 Dyllin Edward Reid Black
//
//===================================================


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class contains all the data stored for a Star System, which is essentially an
//array that has null spaces everywhere except for spaces that would be within a circle
//centered on the center of the array. The result is an approximated circle.

namespace Scramble
{
    public class StarSystem
    {
        public StellarObject[,] systemObjects { private set; get; }     //The array that constitutes the star system.
        private int systemRadius;                                       //The radius of the star system. Stored for potential performance reasons.

        //The basic constructor for this class.
        public StarSystem()
        {
            systemRadius = Settings.systemRadius;
            systemObjects = new StellarObject[(systemRadius * 2) + 1, (systemRadius * 2) + 1];      //The additional one means that a system of radius 15 will have a total diameter of 31,
            Randomize();                                                                            //in order to be able to place single star systems' stars at the middle of the system.
        }

        //Randomly assigns StellarObjects as Stars, Planets, etc.
        private void Randomize()
        {
            //Loop through the entire array.
            for(int x = 0; x < ((systemRadius * 2) + 1); x++)
            {
                for(int y = 0; y < ((systemRadius * 2) + 1); y++)
                {
                    //Check if the point in the array we're at is within the bounds of the actual star system before assigning it.
                    if (((x - systemRadius) * (x - systemRadius)) + ((y - systemRadius) * (y - systemRadius)) < (systemRadius * systemRadius))
                    {
                        //If the point is, randomly assign it a type of StellarObject.
                        switch (Random.Range(0, 8))
                        {
                            case 0:
                                systemObjects[x, y] = new Star(x - systemRadius, y - systemRadius);
                                break;

                            case 1:
                                systemObjects[x, y] = new EmptySpace(x - systemRadius, y - systemRadius);
                                break;

                            case 2:
                                systemObjects[x, y] = new Planet(x - systemRadius, y - systemRadius);
                                break;

                            case 3:
                                systemObjects[x, y] = new Moon(x - systemRadius, y - systemRadius);
                                break;

                            case 4:
                                systemObjects[x, y] = new EmptySpace(x - systemRadius, y - systemRadius);
                                break;

                            case 5:
                                systemObjects[x, y] = new EmptySpace(x - systemRadius, y - systemRadius);
                                break;

                            case 6:
                                systemObjects[x, y] = new EmptySpace(x - systemRadius, y - systemRadius);
                                break;

                            case 7:
                                systemObjects[x, y] = new EmptySpace(x - systemRadius, y - systemRadius);
                                break;
                        }
                    }
                }
            }
        }
    }
}