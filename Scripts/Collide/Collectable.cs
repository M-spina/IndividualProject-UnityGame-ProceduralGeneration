using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// this class inherits form Collidable that handles collison for the object 
public class Collectable : Collidable
{
    protected bool collected; // private to all but the child classes 

    protected override void OnCollide(Collider2D coll) // has a overriden method that can be called by child class  
    { //this  method test to see if an object in the collision array has the name of a player and if it is then it calls the Oncollect method
        if (coll.name == "Player")
        {
            OnCollect();
            
        }
    }

    protected virtual void OnCollect() // this method just chnage the bool to true telling a child class that the object can be collected.
    {
        collected = true;
    }
}
