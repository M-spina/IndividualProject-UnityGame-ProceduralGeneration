using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Collidable
{
    // Start is called before the first frame update
    public float speed;
    private Transform PlayerTransform;
    private Vector2 traget;
    
    public int damage;
    public float pushForce;


    protected override void Start()
    {
        base.Start();
        PlayerTransform = GameManager.instance.player.transform;
        var position = PlayerTransform.position;
        traget = new Vector2(position.x, position.y);
    }

    protected override void Update()
    {
        
        transform.position = Vector2.MoveTowards(transform.position,traget,speed*Time.deltaTime);
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
