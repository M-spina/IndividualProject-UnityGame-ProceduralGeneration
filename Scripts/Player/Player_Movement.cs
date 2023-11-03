using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// this class is used to perform spceific player actions and inhertes form the move class that handles movement and collisions 
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
         base.Start(); // we call the base start method form mover and also get the assing the animator to the animator compnent of the object.
         _animator = GetComponent<Animator>();
         //DontDestroyOnLoad(gameObject);
     }
// this method handles the ReceiveDamage by other game objects. This takes in the damage objects that are created by enemeis when they hit the player 
//This would call the an  intance of GameManager that handles the health change of game object inlcuding the player, As long as the player is alive. 
     protected override void ReceiveDamage(Damage dmg)
     {
         if(!Alive)
             return;
         
         base.ReceiveDamage(dmg);
         
         GameManager.instance.OnHealthChange();
         
     }
     // the death method is overriden and set the variable Alive to flace this would cause the GameManager to trigger the death screen.
     protected override void Death()
     {
         Alive = false;
         GameManager.instance.deathMenuAnime.SetTrigger("Show");
         deathSoundEffects.Play();
     }

     // Fixed Update is called a fixed number of times every second  and it handles the player animator to runn the correct animator when moving
     // it also stops movement whe the method detects the player as dead.
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
     // handles what the level up method will do when called. This is called in the Game Manager when the game manager detcets that enough exp is gain to level the player up 
    public void OnLevelUp()
    {
        //Debug.Log("mass");
        //maxHitpoint++;
        
        maxHitpoint = levelHealth[level]; // we increse our maxHit points a set amount  and heal our the player to max health, aslos inceasung the player level
        level++;
        
        hitpoint = maxHitpoint;
        GameManager.instance.OnHealthChange();
    }
    // set the level of the player when the game loads by checking the perbiusly stored current level and updating the  new level
    public void SetLevel(int level)
    {
        for(int i = 0; i < level; i++)
            OnLevelUp();
    }
   // when the player dies they must be respawned so this method adust player stats to respawn the player by setting it health point to full and 
   // making the payer invincible for a bit to prevent pile on damage and push force.
    public void respawn()
    {
        //hitpoint = maxHitpoint;
        Heal(maxHitpoint);
        Alive = true;
        LastInvincable = Time.time;
        pushDirection = Vector3.zero;
    }

    
     // handles the player healing mathod that is called whenthe player collides with a healing point  this heals the player up to max  and displays text to note that the player is being healed
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
