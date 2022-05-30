using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : Mover
{
    
    // exp points
    public int ExpValue = 1;
    
    [SerializeField] private float speed;
    [SerializeField] private float stoppingDistance;
    [SerializeField] private float retreatDistance;
    
    private bool collideWithPlayer;
    private Transform PlayerTransform;
    
    // hitbox
    private BoxCollider2D hitbox;
    public ContactFilter2D filter;
    private Collider2D[] hits = new Collider2D[10];
    
    //Projectile 
    private float timeBtwShot;
    public float startTimeBtwShot;
    [SerializeField] private GameObject projectile;

    protected override void Start()
    {
        base.Start();
        PlayerTransform = GameManager.instance.player.transform;
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
        timeBtwShot = startTimeBtwShot;
    }

    private void FixedUpdate()
    {
        // if the is outside the stopping distance then the enemy would move towards the player 
        if (Vector2.Distance(transform.position, PlayerTransform.position) > stoppingDistance)
        {
            
            // if chase is true and the object is not colliding with the player 
             // we move the object towards the player 
            
            UpdateMotor((PlayerTransform.position - transform.position).normalized,speed);
            
        }// if the enemy is in between the stopping and retreat distance the enemy should hold it ground
        else if(Vector2.Distance(transform.position, PlayerTransform.position) < stoppingDistance && Vector2.Distance(transform.position, PlayerTransform.position) > retreatDistance)
        {
            transform.position = this.transform.position;
        }// if the is inside the retreat distance then the enemy would move away from the player
        else if(Vector2.Distance(transform.position, PlayerTransform.position) < retreatDistance)
        {
            UpdateMotor((PlayerTransform.position - transform.position).normalized,-speed);
        }
        // a simple count down between shots so the enemy doesnt fire every frame 
        if (timeBtwShot <= 0)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBtwShot = startTimeBtwShot;
        }
        else
        {
            timeBtwShot -= Time.deltaTime;
        }
        
        
        
        collideWithPlayer = false;
        
        
        
        
        
        boxCollider.OverlapCollider(filter, hits); // overlaps are stored int the hits array 
        for (int i = 0; i < hits.Length; i++) // check the array for the player
        {
            if (hits[i] == null)
            {
                continue;
            }
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
