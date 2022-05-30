using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collidable : MonoBehaviour
{
    public ContactFilter2D filter;

    private BoxCollider2D _boxCollider;

    private Collider2D[] hits = new Collider2D[10]; // array of things that an object can collide with.
    // Start is called before the first frame update
    protected virtual void Start()
    {
        _boxCollider = GetComponent<BoxCollider2D>(); // sets a boxcollider to assign collision area
        
    }

    protected virtual void Update()
    {
        
        // collision work by checking if the is a collider over lap between the collider of this object and any other game object
        _boxCollider.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++) // we also want to free this array when nothing is touching so that other object can enter the array
        {
            if (hits[i] == null)
            {
                continue;
            }
            OnCollide(hits[i]);

            hits[i] = null;
        }
    }


    protected virtual void OnCollide(Collider2D coll) // this a method that tells us that the an object doesnt have a on collide methos
    {
        Debug.Log("OnCollide was not implemented in " + this.name);
    }
}
