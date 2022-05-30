using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingArea : Collidable
{
    public int healingAmount = 1;
    private float helaingCoolDown = 1.0f;
    private float lastHeal;


    protected override void OnCollide(Collider2D coll)
    {
       if(coll.name != "Player")
           return;
        
        if (Time.time - lastHeal > helaingCoolDown)
        {
            lastHeal = Time.time;
            GameManager.instance.player.Heal(healingAmount);
        }
    }
}
