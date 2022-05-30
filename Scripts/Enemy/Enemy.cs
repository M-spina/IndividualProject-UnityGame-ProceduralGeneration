using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    // exp points
    public int ExpValue = 1;
    
    //Logic 
    public float triggerLength = 1; // when a enemy will stop to chase
    public float chaseLenght = 5; // when a enemy wll start chasing

    private bool chase;
    private bool collideWithPlayer;
    private Transform PlayerTransform;
    private Vector3 startingPosition;
    [SerializeField] private float speed;
    
    // CollideBox
    public ContactFilter2D filter;
    private BoxCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];
    
    

    protected override void Start()
    {
        base.Start();
        PlayerTransform = GameManager.instance.player.transform;
        startingPosition = transform.position;
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();  
    }

    private void FixedUpdate()
    {
        // if player in range the chaseLenght and the the triggerLength then we will chase is set to true.

        if (Vector3.Distance(PlayerTransform.position, startingPosition) < chaseLenght)
        {
            if (Vector3.Distance(PlayerTransform.position, startingPosition) < triggerLength)
            {
                chase = true;
            }

            if (chase) // if chase is true and the object is not colliding with the player 
            {
                if (!collideWithPlayer) // we move the object towards the player 
                {
                    UpdateMotor((PlayerTransform.position - transform.position).normalized,speed);
                }
            }
            else // else we move the enemy back to the its original spot 
            {
                UpdateMotor(startingPosition - transform.position,speed);
                
            }

        }
        else
        {
            UpdateMotor(startingPosition - transform.position, speed);
            chase = false;
        }
        // check for overlap with the player 
        collideWithPlayer = false;
        
        boxCollider.OverlapCollider(filter, hits); // overlaps are stored int the hits array 
        for (int i = 0; i < hits.Length; i++) // check the array for the player
        {
            if (hits[i] == null)
            {
                continue;
            }
            // if any go bad this is prob problem
            if (hits[i].CompareTag("Player") && hits[i].name == "Player")
            {
                collideWithPlayer = true;
            }

            hits[i] = null;
        }
        
    }

    protected override void Death()
    {
        base.Death();
        Destroy(gameObject);
        //GameManager.instance.exp += xpValue;
        GameManager.instance.GrantExp(ExpValue);
        GameManager.instance.ShowText("+" + ExpValue + " xp",30,Color.magenta, transform.position,Vector3.up*30,1.0f );
    }
}
