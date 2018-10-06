//===================================================
//     Copyright 2018 Dyllin Edward Reid Black
//
//===================================================


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is a custom class that describes a 2D vector using only integers.
//I implemented it due to unusual issues with Unity's integer vector class that went
//away the moment I made my own.

namespace Scramble
{
    public class IntVec2
    {
        public int x { set; get; }      //Simply describes the x and y of the vector.
        public int y { set; get; }

        //The usual constructor for this class.
        public IntVec2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        //Sets the x and y to a given x and y.
        public void Set(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}