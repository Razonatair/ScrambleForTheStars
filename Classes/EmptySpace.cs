//===================================================
//     Copyright 2018 Dyllin Edward Reid Black
//
//===================================================


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class defines a StellarObject of type EmptySpace.

namespace Scramble
{
    public class EmptySpace : StellarObject
    {
        //The basic constructor for the class.
        public EmptySpace(int posX, int posY)
        {
            this.rootTemplate = StellarObjectTemplateDict.GetTemplate("StellarObject_EmptySpace");

            this.posX = posX;
            this.posY = posY;
        }
    }
}