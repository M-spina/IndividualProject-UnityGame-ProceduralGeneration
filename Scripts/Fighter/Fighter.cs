using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    // Public fields 
    public int hitpoint = 10;
    public int maxHitpoint = 10;
    public float pushRecoverySpeed = 0.1f;
    
    //Immunity Protection 
    protected float InvinciblityTime = 1.0f;
    protected float LastInvincable;
    
    //Push 
    protected Vector3 pushDirection;
    
    // fighters can receive damage / die
    protected virtual void ReceiveDamage(Damage dmg)
    {
        if (Time.time - LastInvincable > InvinciblityTime)
        {
            LastInvincable = Time.time;
            hitpoint -= dmg.damageAmount;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce; // the vector between the receiver and the sender
            // multiplied by the push force 
            
            GameManager.instance.ShowText(dmg.damageAmount.ToString(),25,Color.red, transform.position,Vector3.up*20,0.5f);

            if (hitpoint <= 0)
            {
                hitpoint = 0;
                Death();
            }
        }
    }

    protected virtual void Death()
    {
        
    }

}
