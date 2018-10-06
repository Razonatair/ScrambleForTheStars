//===================================================
//     Copyright 2018 Dyllin Edward Reid Black
//
//===================================================


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is the root class for all Orders, such as MovementOrders,
//or ColonizationOrders.

namespace Scramble
{
    public class Order
    {
        public string orderType { protected set; get; }     //Defines a string used to determine what type of Order this is.

    }
}