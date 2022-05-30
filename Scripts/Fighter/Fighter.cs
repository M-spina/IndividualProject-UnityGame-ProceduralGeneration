using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// this class is used to ceate metods that could damage and destroy an object to fighting object such as player  enemis and destroyable objects

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
    
    // fighters can receive damage by checking if their invinciblity timer is up this is ot prevent damage form contantly beeing recived 
    protected virtual void ReceiveDamage(Damage dmg)
    {
        if (Time.time - LastInvincable > InvinciblityTime)
        {
            LastInvincable = Time.time; // reset the invinciblity timer and reduce the object hitpoints 
            hitpoint -= dmg.damageAmount;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce; // the vector between the receiver and the sender
            // multiplied by the push force  to push the object that recived damage away
            // display text that the object has been damaged 
            GameManager.instance.ShowText(dmg.damageAmount.ToString(),25,Color.red, transform.position,Vector3.up*20,0.5f);
            // if hit poiyns hit zero the class calls the Death method
            if (hitpoint <= 0)
            {
                hitpoint = 0;
                Death();
            }
        }
    }
   // inherited class can override this method do somthing different 
    protected virtual void Death()
    {
        
    }

}
