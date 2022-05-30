using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
   public Sprite emptyChest;
   public int CoinMax = 5;
   private int CointAmount;
   protected override void OnCollect()
   {
      if (!collected)
      {
         base.OnCollect();
         GetComponent<SpriteRenderer>().sprite = emptyChest;
         CointAmount = pickRandomNum(CoinMax);
         Debug.Log("Grant "+ CointAmount + " coins");
         GameManager.instance.coins += CointAmount;
         GameManager.instance.ShowText("+" + CointAmount + " coins",25,Color.yellow, transform.position,Vector3.up*20,1.5f );
      }

   }

   private int pickRandomNum(int Coin)
   {
      int randomNum = Random.Range(1, Coin + 1);
      return randomNum;
   }
}
