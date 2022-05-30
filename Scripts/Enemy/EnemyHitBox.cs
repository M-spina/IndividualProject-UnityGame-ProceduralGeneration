using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// this class inherits form Collidable so that when it collides with an object it will report it 
public class EnemyHitBox : Collidable
{
   //Damage
   public int damage;
   public float pushForce;

   protected override void OnCollide(Collider2D coll) // we override the base method and instead of reporting it will run a custom method thet would create a damage object  
   { // this would be taken in by the player and the dmg report is detroyed 
       
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
