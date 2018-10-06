//===================================================
//     Copyright 2018 Dyllin Edward Reid Black
//
//===================================================


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

//This class handles construction of Scramble's Template Dictionaries, which are things like a graphic of a certain planet or type of ship.

namespace Scramble
{
    public static class TemplateManager
    {
        private static bool stellarObjectTemplateDictBuilt = false;         //Defines whether or not the StellarObjectTemplate dictionary has been built.
        private static bool shipTemplateDictBuilt = false;                  //Defines whether or not the ShipTemplate dictionary has been built.

        private static XmlTextReader reader;                                //The XML reader used to read the XML database.

        private static Material workMat;                                    //The current material we're working with.
        private static StellarObjectTemplate workStellarObjectTemplate;     //The current StellarObject we're working with.
        private static ShipTemplate workShipTemplate;                       //The current ShipTemplate we're working with.

        //Called to build all the game's crucial dictionaries from XML.
        public static void BuildDicts()
        {
            //Calls internal methods within the class for brevity.
            BuildStellarObjectTemplateDict();
            BuildShipTemplateDict();

            //Temporary UI texture set up. Will probably set up a UI material dictionary.
            workMat = new Material(Shader.Find("Sprites/Default"));
            workMat.mainTexture = Resources.Load("Textures/UI/Selection") as Texture2D;
            workMat.mainTexture.filterMode = FilterMode.Bilinear;
            MatDict.SetMaterial("Selection", workMat);
        }

        //Builds the StellarObjectTemplate dictionary from the XML database.
        public static void BuildStellarObjectTemplateDict()
        {
            //First check if the dictionary is already built.
            if(!stellarObjectTemplateDictBuilt)
            {
                //Try to open StellarObjectDefs.xml
                try
                {
                    reader = new XmlTextReader("Assets/Resources/Defs/StellarObjectDefs.xml");
                }
                catch
                {
                    //Catch the file not being found.
                    Debug.Log("Could not find StellarObjectDefs.xml");
                    return;
                }

                //Continue reading the file until the end.
                while (reader.Read())
                {
                    //Check if the read data is a start element.
                    if (reader.IsStartElement())
                    {
                        //If it is, determine what variable it's referring to, and assign it.
                        switch (reader.Name.ToString())
                        {
                            //Start building a new StellarObjectTemplate.
                            case "StellarObjectTemplate":
                                workStellarObjectTemplate = new StellarObjectTemplate();
                                break;

                            //Assign the graphicName.
                            case "graphicName":
                                workStellarObjectTemplate.SetGraphicName(reader.ReadString());
                                break;

                            //Build the template's material.
                            case "graphicPath":
                                workMat = new Material(Shader.Find("Sprites/Default"));
                                workMat.mainTexture = Resources.Load(reader.ReadString()) as Texture2D;
                                workMat.mainTexture.filterMode = FilterMode.Bilinear;
                                MatDict.SetMaterial(workStellarObjectTemplate.graphicName, workMat);
                                break;
                        }
                    }
                    else
                    {
                        //Otherwise, check if the node is an end element.
                        if (reader.NodeType == XmlNodeType.EndElement)
                        {
                            //Check if this is the end of the template.
                            if (reader.Name.Equals("StellarObjectTemplate"))
                            {
                                //If it is, finalize the template and add it to the dictionary.
                                workStellarObjectTemplate.Finalized();
                                StellarObjectTemplateDict.SetTemplate(workStellarObjectTemplate.graphicName, workStellarObjectTemplate);
                            }
                        }
                    }
                }
                //Ensure the file is closed after reading, and mark the StellarObjectTemplate dictionary as built.
                reader.Close();
                stellarObjectTemplateDictBuilt = true;
            }
            else
            {
                //If it's already built, something went wrong.
                Debug.Log("Attempting to build already built StellarObjectTemplateDict.");
            }
        }

        //Builds the ShipTemplate dictionary from the XML database.
        public static void BuildShipTemplateDict()
        {
            //Check that the dictionary hasn't already been built.
            if (!shipTemplateDictBuilt)
            {
                //First try to read ShipDefs.xml
                try
                {
                    reader = new XmlTextReader("Assets/Resources/Defs/ShipDefs.xml");
                }
                catch
                {
                    //Catch the file not being found.
                    Debug.Log("Could not find ShipDefs.xml");
                    return;
                }

                //Read until the end of the file.
                while (reader.Read())
                {
                    //Check if the element is a start element.
                    if (reader.IsStartElement())
                    {
                        //If it is, determine what variable it's referring to, and assign it.
                        switch (reader.Name.ToString())
                        {
                            //Begin a new ShipTemplate.
                            case "ShipTemplate":
                                workShipTemplate = new ShipTemplate();
                                break;

                            //Assign the graphicName to the template.
                            case "graphicName":
                                workShipTemplate.SetGraphicName(reader.ReadString());
                                break;

                            //Build the material for the template and then add it to the material dictionary.
                            case "graphicPath":
                                workMat = new Material(Shader.Find("Sprites/Default"));
                                workMat.mainTexture = Resources.Load(reader.ReadString()) as Texture2D;
                                workMat.mainTexture.filterMode = FilterMode.Bilinear;
                                MatDict.SetMaterial(workShipTemplate.graphicName, workMat);
                                break;

                            //Assign the movementSpeed to the template.
                            case "movementSpeed":
                                workShipTemplate.SetMovementSpeed(int.Parse(reader.ReadString()));
                                break;
                        }
                    }
                    else
                    {
                        //Check if the node type is an end element.
                        if (reader.NodeType == XmlNodeType.EndElement)
                        {
                            //If it is, is this the end of a template?
                            if (reader.Name.Equals("ShipTemplate"))
                            {
                                //Finalize the template and add it to the ShipTemplate dictionary.
                                workShipTemplate.Finalized();
                                ShipTemplateDict.SetTemplate(workShipTemplate.graphicName, workShipTemplate);
                            }
                        }
                    }
                }
                //Close the file after it's been read and mark the dictionary built.
                reader.Close();
                shipTemplateDictBuilt = true;
            }
            else
            {
                //Otherwise, the dictionary has already been built and something went wrong.
                Debug.Log("Attempting to build already built ShipTemplateDict.");
            }

        }
    }
}
