using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// class chest inherits form collectable that hadles collidion dection form a gameobject and the player 
public class Chest : Collectable
{
   public Sprite emptyChest; // assign sprites to use when the chest is open and coint amoun to give to the player 
   public int CoinMax = 5;
   private int CointAmount;
   protected override void OnCollect()
   {
      if (!collected) // if the object is collected then it will call the base mathod of Oncollect which is found in the Collectable class
      {
         base.OnCollect(); // after we run the base method we change the current sprite of the object and pick a random amount of coins to give to the player 
         GetComponent<SpriteRenderer>().sprite = emptyChest;
         CointAmount = pickRandomNum(CoinMax);
         Debug.Log("Grant "+ CointAmount + " coins");
         GameManager.instance.coins += CointAmount; // we then display the amount uisng a Game manager intance 
         GameManager.instance.ShowText("+" + CointAmount + " coins",25,Color.yellow, transform.position,Vector3.up*20,1.5f );
      }

   }
// this method pick a random coin amount to give to the player 
   private int pickRandomNum(int Coin)
   {
      int randomNum = Random.Range(1, Coin + 1);
      return randomNum;
   }
}
