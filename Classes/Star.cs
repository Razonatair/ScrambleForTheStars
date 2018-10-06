//===================================================
//     Copyright 2018 Dyllin Edward Reid Black
//
//===================================================


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class defines a StellarObject of type Star.

namespace Scramble
{
    public class Star : StellarObject
    {
        //The basic constructor for the class.
        public Star(int posX, int posY)
        {
            this.rootTemplate = StellarObjectTemplateDict.GetTemplate("StellarObject_Star");

            this.posX = posX;
            this.posY = posY;
        }
    }
}