using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox : Collidable
{
   //Damage
   public int damage;
   public float pushForce;

   protected override void OnCollide(Collider2D coll)
   {
       // if goes bad this prob problem
      if (coll.CompareTag("Player") && coll.name == "Player")
      {
       // Create a Damage object , before sending it to the player
       Damage dmg = gameObject.AddComponent<Damage>();
       dmg.damageAmount = damage;
       dmg.origin = transform.position;
       dmg.pushForce = pushForce;
       
       coll.SendMessage("ReceiveDamage",dmg);
       Destroy(dmg);
      }
   }
}
