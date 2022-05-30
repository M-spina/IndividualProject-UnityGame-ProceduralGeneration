using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// this class inherites from Collidable to check for collision
// this class is used to create a position that would heal the player when collided 
public class HealingArea : Collidable
{
    public int healingAmount = 1; // we create variables for the healing amountwe want to heal the player and we also want to avoid healing the player every frame 
    private float helaingCoolDown = 1.0f; // so we implmented a healing cool down variable 
    private float lastHeal;

    // the overriden method check to see if the player has collided with the object  
    protected override void OnCollide(Collider2D coll) 
    {
       if(coll.name != "Player")
           return;
        
        if (Time.time - lastHeal > helaingCoolDown) // if the player is detected  it will run down the cooldown timer and heal the player by a set amount and reset the timer 
        {
            lastHeal = Time.time;
            GameManager.instance.player.Heal(healingAmount);
        }
    }
}
