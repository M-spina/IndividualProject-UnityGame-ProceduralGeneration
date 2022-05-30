using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// this is the enemy class that inherites form mover class that handles movement and damage 
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
    
    
// this method is called first during run time  and assigns impotant variables, such as assing the current player psotion in the game world 
// storring a starting position for the enemy and getting its hitbox for collision
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
            // if the colider in the array is of tag player and has the name of player we know he have hit the player 
            if (hits[i].CompareTag("Player") && hits[i].name == "Player")
            {
                collideWithPlayer = true; // we set the veribale to be true and this cause the enmey to be damaged form a method in fighter that the enemy inherits through mover
            }

            hits[i] = null;
        }
        
    }

    protected override void Death() // when the death method is called it destroy the object and give exp to the player through the game manager  and this diplays the exp as text in the world
    {
        base.Death();
        Destroy(gameObject);
        //GameManager.instance.exp += xpValue;
        GameManager.instance.GrantExp(ExpValue);
        GameManager.instance.ShowText("+" + ExpValue + " xp",30,Color.magenta, transform.position,Vector3.up*30,1.0f );
    }
}
