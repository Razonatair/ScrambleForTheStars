//===================================================
//     Copyright 2018 Dyllin Edward Reid Black
//
//===================================================


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This static class will essentially handle distribution of StellarPathfinders to
//the requesting threads as necessary.

namespace Scramble
{
    public static class StellarPathfinderList
    {
        private static List<StellarPathfinder> list;    //The list of StellarPathfinders.
        private static int index;                       //An index of which StellarPathfinder in the list to send.

        private static bool initialized;                //Determines if the class has been initialized yet.

        //Initializes the class and sets up various important variables before anything
        //else can be done.
        public static void Initialize()
        {
            //If we're not initialized, run through initialization.
            if (!initialized)
            {
                //We need to get the list of StellarPathfinders initialized, so loop through the list up until
                //the max number of threads and initialize them.
                list = new List<StellarPathfinder>();
                for(int i = 0; i < Settings.threadNumber; i++)
                {
                    list.Add(new StellarPathfinder());
                }
                initialized = true;
                index = 0;
            }
            else //Else, hassle the programmer for trying to initialize something already initialized.
            {
                Debug.Log("Attempting to initialize already initialized StellarPathfinderList.");
            }
        }

        //Sends out a reference to the next StellarPathfinder in line.
        public static StellarPathfinder AssignStellarPathfinder()
        {
            //We need to ensure the class has been initialized first.
            if (initialized)
            {
                //Check if the next spot in the index would equal the max number of threads.
                if (index + 1 == Settings.threadNumber)
                {
                    //If it would, loop back to zero.
                    index = 0;
                }
                index++;
                return list[index - 1];
            }
            else //Else if it hasn't, we need to hassle the programmer to initialize it first.
            {
                Debug.Log("Initialize the StellarPathfinderList before calling it.");
                return null;
            }
        }
    }
}