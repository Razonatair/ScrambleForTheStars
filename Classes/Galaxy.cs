//===================================================
//     Copyright 2018 Dyllin Edward Reid Black
//
//===================================================


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class contains all the data pertinent for the overall Galaxy, which contains an array
//of StarSystems. This class is far from finished.

namespace Scramble
{
    public static class Galaxy
    {
        private static int galaxyDiam { set; get; }                         //Defines the diameter of the galaxy. Unused currently.
        private static int starNum { set; get; }                            //Defines how many stars are present in the galaxy.

        private static bool starGenerated = false;                          //Used for generating the galaxy, stored whether or not a star was
                                                                            //successfully created.
        private static int starPosX { set; get; }                           //These might be used to store the current star system position.
        private static int starPosY { set; get; }

        public static StarSystem[,] galaxyArray { private set; get; }       //The array that holds the locations of the StarSystems of the Galaxy.

        private static bool generated;                                      //Defines whether or not the galaxy has been generated yet.

        //Called to randomly generate the galaxy.
        public static void RandomGenerate(int galaxyDiam, int starNum)
        {
            //First check if the galaxy has already been generated.
            if (!generated)
            {
                Galaxy.starNum = starNum;

                //Ensure that the handed galaxy diameter is above zero.
                if (galaxyDiam > 0)
                {
                    Galaxy.galaxyDiam = galaxyDiam;
                    galaxyArray = new StarSystem[galaxyDiam, galaxyDiam];

                    //Loop until we've generated as many stars as we're told to.
                    for (int i = 0; i < Galaxy.starNum; i++)
                    {
                        //Until a star is generated, keep trying.
                        while (!starGenerated)
                        {
                            //Repeatedly pick a random point in the galaxy...
                            starPosX = Random.Range(0, galaxyDiam);
                            starPosY = Random.Range(0, galaxyDiam);

                            //...and check if the point already has a star present.
                            if (Galaxy.galaxyArray[starPosX, starPosY] == null)
                            {
                                //If not, generate a star there.
                                Galaxy.galaxyArray[starPosX, starPosY] = new StarSystem();
                                starGenerated = true;
                            }
                        }

                        //Reset starGenerated for the next star to generate.
                        starGenerated = false;
                    }
                }
                else
                {
                    //Zero and negative numbers are unacceptable parameters.
                    Debug.Log("Attempting RandomGenerate of Galaxy with zero or negative GalaxyDiam.");
                }

                //Mark generated true to avoid all kinds of potential problems later.
                generated = true;
            }
            else
            {
                //If it has, something went wrong.
                Debug.Log("Attempting to generate an already generated Galaxy.");
            }
        }
    }
}