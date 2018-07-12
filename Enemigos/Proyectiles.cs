using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectiles : MonoBehaviour {

    private float velocidad = 10;
    private Vector3 distancia;
    private int danno = 1;
   

    
    private Transform target;
    

    private Animator animator;

    private void Start()
    {      
        target = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        distancia = target.position - transform.position;
        distancia = distancia.normalized;

        float angle = Mathf.Atan2(distancia.y, distancia.x) * Mathf.Rad2Deg;
        angle += 45;
        transform.rotation = Quaternion.Euler(0, 0, angle);

    }

    void Update()
    {

        
        transform.Translate(distancia * velocidad * Time.deltaTime, Space.World);
        

    }
    private void OnCollisionEnter2D(Collision2D otro)
    {
        
        animator.SetTrigger("splash");
        Destroy(gameObject, 0.2f);

        if(otro.transform.CompareTag("Player")) otro.gameObject.SendMessage("RecibirDanno", danno);


    }

}
