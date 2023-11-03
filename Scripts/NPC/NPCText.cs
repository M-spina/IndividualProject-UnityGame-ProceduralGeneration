using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// this is a clas that enables an npc object to display text when collided with
public class NPCText : Collidable
{

    public string message; // we set the method in the inspctor 

    private float coolDown = 4.0f; // we dont want keep call for the text to be create so we made cool down timer 
    private float lastShout;
    // we start  calling teh base methiod of our overriden method drecrementing timer
    protected override void Start()
    {
        base.Start();
        lastShout = -coolDown;
    }

    protected override void OnCollide(Collider2D coll) // when the timer has decremented we rest the timer and display the text above the player/npc
    {
        if (Time.time - lastShout > coolDown)
        {
            lastShout = Time.time;
            GameManager.instance.ShowText(message, 25 , Color.white, transform.position + new Vector3(0,0.16f,0),Vector3.zero, coolDown);
        }
        //GameManager.instance.ShowText(message, 25 , Color.white, transform.position,Vector3.zero, 4.0f);
    }
}
