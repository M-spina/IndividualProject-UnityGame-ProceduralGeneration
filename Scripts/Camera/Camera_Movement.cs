using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Movement : MonoBehaviour
{
    private Transform target; // we want to get the x,y coordinates of the target so when can update the position of the player
    [SerializeField] private float SmoothSpeed;
    private void Start()
    {
        target = GameObject.Find("Player").transform;

        //target = GameObject.FindGameObjectWithTag("Fighter").GetComponent<Transform>();
        // are target variable would get the game-object with the tag transform coordinates
    }

    private void LateUpdate() // late update is called after all update funtions are called used to order script executions 
    {
        //MARKER method 1 traditional method to move the camera
        //transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        // updates the cameras position with the targets x,y coordinates. We cant use the target's z as i wouldn't be able to see the player  
        
        
        //MARKER Method 2  smoothly move the camera 
        // first parameter is the current position 
        // second parameter is the target's position
        // third parameter is the speed
        transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), SmoothSpeed * Time.deltaTime );
        
    }
}
