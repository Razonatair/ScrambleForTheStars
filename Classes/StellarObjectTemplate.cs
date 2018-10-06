//===================================================
//     Copyright 2018 Dyllin Edward Reid Black
//
//===================================================


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class will hold a variety of variables common to all StellarObjects,
//and is constructed by the TemplateManager from the xml database.

namespace Scramble
{
    public class StellarObjectTemplate
    {
        public string graphicName { private set; get; }     //The name of the material used for this StellarObject, such as "Planet"

        private bool finalized;                             //Defines whether or not this template is done and thus essentially readonly.
        
        //The basic constructor for the class, called if one wants to build it up through
        //the methods as opposed to directly handing it arguments.
        public StellarObjectTemplate()
        {
            graphicName = null;
            finalized = false;
        }

        //The more specific constructor that requires arguments for all variables of the template.
        public StellarObjectTemplate(string graphicName)
        {
            this.graphicName = graphicName;
        }

        //Allows the graphicName of the template to be assigned if the class is unfinalized.
        public void SetGraphicName(string graphicName)
        {
            if (!finalized)
            {
                this.graphicName = graphicName;
            }
            else
            {
                Debug.Log("Attempting to assign graphicName to finalized StellarObjectTemplate.");
            }
        }

        //Marks the template as finalized. No further changes can be made once this is called.
        public void Finalized()
        {
            finalized = true;
        }
    }
}