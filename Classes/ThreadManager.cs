//===================================================
//     Copyright 2018 Dyllin Edward Reid Black
//
//===================================================


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

//This class will handle the creation and assigning of threads.
//It has not been implemented yet.

namespace Scramble
{
    public static class ThreadManager
    {
        private static Thread[] threadArray;        //The array which will hold all threads used by the game.

        private static bool initialized;            //Defines whether or not this class has been initialized yet.

        //Initializes the class and prepares it for use.
        public static void Initialize()
        {
            //If we're not initialized, run through initialization.
            if (!initialized)
            {
                threadArray = new Thread[Settings.threadNumber];
                initialized = true;
            }
            else //Else, hassle the programmer for trying to initialize something they already did.
            {
                Debug.Log("Attempting to initialize already initialized ThreadManager.");
            }
        }
    }
}