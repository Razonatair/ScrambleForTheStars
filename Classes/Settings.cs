//===================================================
//     Copyright 2018 Dyllin Edward Reid Black
//
//===================================================


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

//This class stores settings for Scramble.

namespace Scramble
{
    public static class Settings
    {
        public static int systemRadius { private set; get; }        //Defines the radius of star systems in the game.

        public static int threadNumber { private set; get; }        //Will define the max number of threads the game uses.

        public static int screenResX { private set; get; }          //Will store the user's desired resolution eventually.
        public static int screenResY { private set; get; }

        public static bool autoEndTurn { private set; get; }        //Will store whether or not the user wants to immediately end.
                                                                    //their turn after there is nothing obvious left to do on their turn.

        private static XmlTextReader reader;                        //Used for reading in Settings.xml.

        //Called when the game is starting to load the user's settings into memory.
        public static void LoadSettings()
        {
            //Tries to open Settings.xml, and catches the potential error of the file not existing.
            try
            {
                reader = new XmlTextReader("Assets/Resources/Defs/Settings.xml");
            }
            catch
            {
                Debug.Log("Could not find Settings.xml");
                return;
            }

            //Until the entire file has been read, been reading.
            while (reader.Read())
            {
                //If a start element is detected...
                if (reader.IsStartElement())
                {
                    //...check to see if the name matches any settings, and assign the setting if it does.
                    switch (reader.Name.ToString())
                    {
                        case "systemRadius":
                            systemRadius = reader.ReadElementContentAsInt();
                            break;
                        case "threadNumber":
                            threadNumber = reader.ReadElementContentAsInt();
                            break;
                    }
                }
            }
            //Once we're done reading, ensure the reader has been closed to avoid access issues.
            reader.Close();
        }

        //This method will be used to write to the Settings.xml file eventually.
        public static void SaveSettings()
        {

        }
    }
}