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
        
        // collision work
        _boxCollider.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
            {
                continue;
            }
            OnCollide(hits[i]);

            hits[i] = null;
        }
    }


    protected virtual void OnCollide(Collider2D coll)
    {
        Debug.Log("OnCollide was not implemented in " + this.name);
    }
}
