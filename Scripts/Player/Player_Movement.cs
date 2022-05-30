using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : Mover
{
     //private BoxCollider2D boxCollider;
    
     public float PlayerSpeed;
    
     //private Vector3 moveDelta; // keeps track of delta movement in between frames
     //private RaycastHit2D hit;
     private Vector2 _movement;
     private Animator _animator;
     private int[] levelHealth = {11,12,13,14,15,16,17,19,20,21};
     private int level = 0;
     public bool Alive = true;
     public AudioSource deathSoundEffects;

     // Start is called before the first frame update
     
     protected override void Start()
     {
         base.Start();
         _animator = GetComponent<Animator>();
         //DontDestroyOnLoad(gameObject);
     }

     protected override void ReceiveDamage(Damage dmg)
     {
         if(!Alive)
             return;
         
         base.ReceiveDamage(dmg);
         
         GameManager.instance.OnHealthChange();
         
     }

     protected override void Death()
     {
         Alive = false;
         GameManager.instance.deathMenuAnime.SetTrigger("Show");
         deathSoundEffects.Play();
     }


     private void FixedUpdate()
    {
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");

       //if (_movement != Vector2.zero)
        //{
        _animator.SetFloat("Horizontal",_movement.x);
        _animator.SetFloat("Vertical",_movement.y);
        //}
        _animator.SetFloat("Speed",_movement.sqrMagnitude);

        if (Alive)
            UpdateMotor(new Vector3(_movement.x,_movement.y,0),PlayerSpeed);
        
        //throw new NotImplementedException();
    }

    public void OnLevelUp()
    {
        //Debug.Log("mass");
        //maxHitpoint++;
        
        maxHitpoint = levelHealth[level];
        level++;
        
        hitpoint = maxHitpoint;
        GameManager.instance.OnHealthChange();
    }

    public void SetLevel(int level)
    {
        for(int i = 0; i < level; i++)
            OnLevelUp();
    }

    public void respawn()
    {
        //hitpoint = maxHitpoint;
        Heal(maxHitpoint);
        Alive = true;
        LastInvincable = Time.time;
        pushDirection = Vector3.zero;
    }

    

    public void Heal(int healingAmount)
    {
        if(hitpoint == maxHitpoint)
            return;

        hitpoint += healingAmount;
        if (hitpoint > maxHitpoint)
            hitpoint = maxHitpoint;
        
        GameManager.instance.ShowText( "+ " + healingAmount.ToString() + " hp",25,Color.green, transform.position,Vector3.up*20,1.0f);
        GameManager.instance.OnHealthChange();

    }
    
    

    // Update is called once per frame
    
     

}
