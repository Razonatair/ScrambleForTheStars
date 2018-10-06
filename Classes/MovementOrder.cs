//===================================================
//     Copyright 2018 Dyllin Edward Reid Black
//
//===================================================


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class defines an Order of type MovementOrder.

namespace Scramble
{
    public class MovementOrder : Order
    {
        public IntVec2 targetPos { private set; get; }          //Defines where the target is within...
        public StarSystem targetSystem { private set; get; }    //...the system that the target is in.

        //The basic constructor for this class.
        public MovementOrder(IntVec2 targetPos, StarSystem targetSystem)
        {
            orderType = "MovementOrder";
            this.targetPos = targetPos;
            this.targetSystem = targetSystem;
        }
    }
}