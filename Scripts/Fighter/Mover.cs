using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : Fighter
{
    
     protected BoxCollider2D boxCollider;
     protected Vector3 moveDelta; // keeps track of delta movement in between frames
     protected RaycastHit2D hit;
     public float ySpeed = 0.7f;
     public float xSpeed = 1.0f;
     private Vector3 OriginalSize;
     
     // Start is called before the first frame update
   protected virtual void Start()
   {
       //
        OriginalSize = transform.localScale;
        boxCollider = GetComponent<BoxCollider2D>();
        
    }
   

    // Update is called once per frame
     protected virtual void UpdateMotor(Vector3 input,float speed)
     {
         // return -1 if (left,down) is pressed , 0 if nothing is pressed , 1 if (right,up) is pressed
         //float x = Input.GetAxisRaw("Horizontal");
         //float y = Input.GetAxisRaw("Vertical");
         
         // reset move delta so that in the new fame it would go to zero if there are no inputs to movement 
         moveDelta = input;
         
         // swap sprite direction, whether you're going right or left
         if (moveDelta.x > 0)
             transform.localScale = OriginalSize; //Vector3.one;
         else if (moveDelta.x < 0)
             //transform.localScale = new Vector3(-1, 1, 1);
             transform.localScale = new Vector3(OriginalSize.x * -1, OriginalSize.y, OriginalSize.z);
         
         // Add pushVector 
         moveDelta += pushDirection;
         
         // Reduce pushForce;
         pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);
         
         // Make sure we can move in this direction, by creating a box there first , if the box returns null we can move
         hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y),
             Mathf.Abs(moveDelta.y * (Time.deltaTime*2)), LayerMask.GetMask("Actor", "Blocking"));
         if (hit.collider == null)
         {
             transform.Translate(0,moveDelta.y*speed*Time.deltaTime,0);
         }
         
         hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x,0),
             Mathf.Abs(moveDelta.x * (Time.deltaTime*2)), LayerMask.GetMask("Actor", "Blocking"));

         if (hit.collider == null)
         {
             transform.Translate(moveDelta.x*speed*Time.deltaTime,0,0);
         }

         // throw new NotImplementedException();
         
     }
}
