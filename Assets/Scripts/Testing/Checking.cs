using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checking : MonoBehaviour
{
    
    public Transform target;

    void Start()
    {
        Vector3 localPose = gameObject.transform.position;
        // localPose.z += .05f;
        // gameObject.transform.position = localPose;

        gameObject.transform.Translate(new Vector3(localPose.x,localPose.y-0.05f,localPose.z),Space.Self);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
