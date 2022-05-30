using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// this class create a projectile that the enemy shoot to target the player
// it inherites form collidable as it will need to check for collsions agenst the player
public class EnemyProjectile : Collidable
{
    // Start is called before the first frame update
    public float speed;
    private Transform PlayerTransform;
    private Vector2 traget;
    
    public int damage;
    public float pushForce;

    // we override the start method form collidable and run the base method we also store the player psotion in a veriablea called position ans store the  x,y corridnates in a verctor 2 variable
    protected override void Start()
    {
        base.Start();
        PlayerTransform = GameManager.instance.player.transform;
        var position = PlayerTransform.position;
        traget = new Vector2(position.x, position.y);
    }
    // in the update method that is called every frame we move the projectile toward the player uisng the method MoveToward that target
    protected override void Update()
    {
        
        transform.position = Vector2.MoveTowards(transform.position,traget,speed*Time.deltaTime); // when the projectile reaches the target it is destroyed 
        if (transform.position.x == traget.x && transform.position.y == traget.y)
        {
            DestroyProjectile();
        }
    }
    

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
    
}
