using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// this class is only used to set an object Transform component the same as the spaen point
public class SpawnPoint : MonoBehaviour
{
    private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = target.position;
    }
}
