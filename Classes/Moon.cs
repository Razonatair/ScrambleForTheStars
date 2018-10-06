//===================================================
//     Copyright 2018 Dyllin Edward Reid Black
//
//===================================================


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class defines a StellarObject with a type of Moon.

namespace Scramble
{
    public class Moon : StellarObject
    {
        //The basic constructor for the class.
        public Moon(int posX, int posY)
        {
            this.rootTemplate = StellarObjectTemplateDict.GetTemplate("StellarObject_Moon");

            this.posX = posX;
            this.posY = posY;
        }
    }
}