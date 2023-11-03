using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// this class control the box behaviuor it inhertest form fighter a class that has methods to receive damage
public class Box : Fighter
{
    public Sprite Damage; // takes in a sprite we can assigne in the editor to chnage when the box is damaged
    
    protected override void ReceiveDamage(Damage dmg) // overrided the base methods form Fighter class 
    {
        if (Time.time - LastInvincable > InvinciblityTime) // when the boxe's Invinciblity time no in effect we do damage to the box when hit 
        {
            LastInvincable = Time.time;
            hitpoint -= dmg.damageAmount;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce; // the vector between the receiver and the sender
            
            
            
            // this calls an intance of our game manager to display text that the box has recived damage 
            GameManager.instance.ShowText(dmg.damageAmount.ToString(),25,Color.red, transform.position,Vector3.up*20,0.5f);
            // when the box health fall bellow half it's max it will chnage to a damaged sprite 
            if (hitpoint <= (maxHitpoint/2))
            { // when hit point fall bellow a 0 it calls the death method
                this.gameObject.GetComponent<SpriteRenderer>().sprite = Damage;
                if (hitpoint <= 0)
                {
                    hitpoint = 0;
                    Death();
                }
                
            }
        }
    }
// when the death method is called it will destroy the intance of this gameobject
    protected override void Death()
    { 
        Destroy(gameObject);
    }
}
