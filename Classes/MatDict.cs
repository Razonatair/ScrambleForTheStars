//===================================================
//     Copyright 2018 Dyllin Edward Reid Black
//
//===================================================


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class contains the Material Dictionary that stores materials for Scramble,
//which are essentially textures in Unity terms with some extra data.

namespace Scramble
{
    public static class MatDict
    {
        private static Dictionary<string, Material> matDict;    //The crucial dictionary of this class, stores materials with a string key.

        //Initializes the class's variables.
        public static void Initialize()
        {
            //Check if the dictionary isn't null.
            if(matDict != null)
            {
                //If it isn't, something went wrong.
                Debug.Log("Attempting to initialize already initialized MatDict.");
            }
            else
            {
                //Otherwise, initialize the dictionary.
                matDict = new Dictionary<string, Material>();
            }
        }

        //Returns a requested material based on a string key.
        public static Material GetMaterial(string matName)
        {
            //Try to return the requested material.
            try
            {
                return matDict[matName];
            }
            catch
            {
                //If it's not found, inform us of this fact.
                Debug.Log(matName + " not found in the MatDict.");
                return null;
            }
        }

        //Assign a material to the dictionary based upon a string key.
        public static void SetMaterial(string matName, Material mat)
        {
            //Ensure that the dictionary isn't null.
            if(matDict != null)
            {
                //Then check if the dictionary already contains the given string.
                if(matDict.ContainsKey(matName))
                {
                    //If it does, replace the existing material with the new one.
                    matDict[matName] = mat;
                }
                else
                {
                    //Otherwise, simply add the material to the dictionary.
                    matDict.Add(matName, mat);
                }
            }
            else
            {
                //If the dictionary is null, something went wrong.
                Debug.Log("Attempting to set material in uninitialized MatDict.");
            }
        }
    }
}