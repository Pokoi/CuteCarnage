using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {

    
    [SerializeField]
    private float parallaxSpeedHorizontal;
    [SerializeField]
    private float parallaxSpeedVertical;

    private Transform camTransform;
    private Transform[] capas;    
    
    private float lastCameraX;
    private float lastCameraY;
   

	// Use this for initialization
	void Start () {
        camTransform = Camera.main.transform;
       

        lastCameraX = camTransform.position.x;
        lastCameraY = camTransform.position.y;
    }
	
	// Update is called once per frame
	void Update () {
        float deltaX = camTransform.position.x - lastCameraX;
        float deltaY = camTransform.position.y - lastCameraY;

        transform.position += Vector3.right *(deltaX * parallaxSpeedHorizontal);
        transform.position += Vector3.up * (deltaY * parallaxSpeedVertical);

        lastCameraX = camTransform.position.x;
        lastCameraY = camTransform.position.y;
        
    }

   
}

