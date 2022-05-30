using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// the enemy boss class controlls the movement of the orb object around the enmey 
public class EnemyBoss : Enemy
{
    // we take in list of floast for speed and siatnce form the enemy
    // we also take transform components of the orbs  
    public float[] ObjectSpeed = {2.7f,-2.7f};
    public float[] distance = {0.25f,0.5f};
    public Transform[] Object;
    // on every frame  
    private void Update() // The boss would have object orbiting around the it self uisng sin and cos operators to move
    {
        for(int i = 0; i< Object.Length; i++)
        {
            Object[i].position = transform.position + new Vector3(-MathF.Cos(Time.time * ObjectSpeed[i]) * distance[i],
                Mathf.Sin(Time.time * ObjectSpeed[i]) * distance[i], 0);
        }
        
    }
}
