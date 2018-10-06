//===================================================
//     Copyright 2018 Dyllin Edward Reid Black
//
//===================================================


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class specifies characteristics and methods of a StellarObject that is
//also a Planet.

namespace Scramble
{
    public class Planet : StellarObject
    {

        //The standard constructor for now.
        public Planet(int posX, int posY)
        {
            this.rootTemplate = StellarObjectTemplateDict.GetTemplate("StellarObject_Planet");

            this.posX = posX;
            this.posY = posY;
        }
    }
}