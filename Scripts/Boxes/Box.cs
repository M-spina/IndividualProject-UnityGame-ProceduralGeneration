using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : Fighter
{
    public Sprite Damage;
    
    protected override void ReceiveDamage(Damage dmg)
    {
        if (Time.time - LastInvincable > InvinciblityTime)
        {
            LastInvincable = Time.time;
            hitpoint -= dmg.damageAmount;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce; // the vector between the receiver and the sender
            
            
            // multiplied by the push force 
            
            GameManager.instance.ShowText(dmg.damageAmount.ToString(),25,Color.red, transform.position,Vector3.up*20,0.5f);
            // we are chaning this 
            if (hitpoint <= (maxHitpoint/2))
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = Damage;
                if (hitpoint <= 0)
                {
                    hitpoint = 0;
                    Death();
                }
                
            }
        }
    }

    protected override void Death()
    { 
        Destroy(gameObject);
    }
}
