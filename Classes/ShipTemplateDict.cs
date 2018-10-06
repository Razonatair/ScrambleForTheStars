//===================================================
//     Copyright 2018 Dyllin Edward Reid Black
//
//===================================================


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class holds a dictionary of references to ShipTemplates.

namespace Scramble
{
    public static class ShipTemplateDict
    {
        private static Dictionary<string, ShipTemplate> shipTemplateDict;   //The crucial dictionary of ShipTemplates, stores templates based on string.

        //Initializes the dictionary and prepares it for use.
        public static void Initialize()
        {
            //If the dictionary is already initialized, something went wrong.
            if (shipTemplateDict != null)
            {
                Debug.Log("Attempting to initialize already initialized ShipTemplateDict.");
            }
            else
            {
                //Else, initialize the dictionary.
                shipTemplateDict = new Dictionary<string, ShipTemplate>();
            }
        }

        //Returns a ShipTemplate based upon a string.
        public static ShipTemplate GetTemplate(string templateName)
        {
            //Try to return the ShipTemplate.
            try
            {
                return shipTemplateDict[templateName];
            }
            catch
            {
                //Otherwise, the template wasn't found.
                Debug.Log(templateName + " not found in the ShipTemplateDict.");
                return null;
            }
        }

        //Assigns a template to the dictionary based upon a string.
        public static void SetTemplate(string templateName, ShipTemplate template)
        {
            //First check if the dictionary is null.
            if (shipTemplateDict != null)
            {
                //Then check if the dictionary already contains the given string.
                if (shipTemplateDict.ContainsKey(templateName))
                {
                    //Replacing the old ShipTemplate with the new one.
                    shipTemplateDict[templateName] = template;
                }
                else
                {
                    //Else, add the template to the dictionary.
                    shipTemplateDict.Add(templateName, template);
                }
            }
            else
            {
                //If the dictionary is null, something went wrong.
                Debug.Log("Attempting to set material in uninitialized ShipTemplateDict.");
            }
        }

    }
}