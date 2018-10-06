//===================================================
//     Copyright 2018 Dyllin Edward Reid Black
//
//===================================================


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class defines a dictionary of StellarObjectTemplates such as Star and Planet, for example.

namespace Scramble
{
    public static class StellarObjectTemplateDict
    {
        private static Dictionary<string, StellarObjectTemplate> stellarObjectTemplateDict;     //The crucial dictionary, returns a
                                                                                                //StellarObjectTemplate based on a string.

        //Initializes the class and prepares it to be used.
        public static void Initialize()
        {
            //If the dictionary isn't null, the class was already initialized.
            if (stellarObjectTemplateDict != null)
            {
                Debug.Log("Attempting to initialize already initialized StellarObjectTemplateDict.");
            }
            else //Else, initialize the dictionary.
            {
                stellarObjectTemplateDict = new Dictionary<string, StellarObjectTemplate>();
            }
        }

        //Returns a StellarObjectTemplate based on the string handed to it.
        public static StellarObjectTemplate GetTemplate(string templateName)
        {
            //Try to return the requested StellarObjectTemplate.
            try
            {
                return stellarObjectTemplateDict[templateName];
            }
            catch
            {
                //Else, something went wrong and the template wasn't found.
                Debug.Log(templateName + " not found in the StellarObjectTemplateDict.");
                return null;
            }
        }

        //Assigned a StellarObjectTemplate to the dictionary according to a string.
        public static void SetTemplate(string templateName, StellarObjectTemplate template)
        {
            //As long as the dictionary isn't null, continue.
            if (stellarObjectTemplateDict != null)
            {
                //Check if the dictionary already contains the given string.
                if (stellarObjectTemplateDict.ContainsKey(templateName))
                {
                    //If it does, replace it with the given template.
                    stellarObjectTemplateDict[templateName] = template;
                }
                else
                {
                    //Else, assign the template normally.
                    stellarObjectTemplateDict.Add(templateName, template);
                }
            }
            else
            {
                //If the dictionary is null, the class hasn't been initialized yet.
                Debug.Log("Attempting to set material in uninitialized StellarObjectTemplateDict.");
            }
        }
    }
}