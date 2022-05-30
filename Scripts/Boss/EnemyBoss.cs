using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : Enemy
{
    public float[] ObjectSpeed = {2.7f,-2.7f};
    public float[] distance = {0.25f,0.5f};
    public Transform[] Object;

    private void Update() // The boss would have object orbiting around the it self 
    {
        for(int i = 0; i< Object.Length; i++)
        {
            Object[i].position = transform.position + new Vector3(-MathF.Cos(Time.time * ObjectSpeed[i]) * distance[i],
                Mathf.Sin(Time.time * ObjectSpeed[i]) * distance[i], 0);
        }
        
    }
}
