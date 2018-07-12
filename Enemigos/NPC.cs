using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class NPC : MonoBehaviour {

    //Variables necesarias BLOQUE ANIMACIONES
    [SerializeField] private SkeletonAnimation npcAnimation;
    private string animacionActual = "";

    [SerializeField]
    private float distanciaMaxima;
    [SerializeField]
    private float distanciaMinima;
    private Vector3 distancia;
    private float distanciaTar_TransX;
    private float distanciaTrans_TarX;
   
    [SerializeField]
    private Transform target;


	// Update is called once per frame
	void Update () {

        distancia = target.position - transform.position;
        distancia = distancia.normalized;
        distanciaTar_TransX = target.position.x - transform.position.x;
        distanciaTrans_TarX = transform.position.x - target.position.x;

        if (distanciaTar_TransX < distanciaMaxima && distanciaTar_TransX > distanciaMinima)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        if (distanciaTrans_TarX < distanciaMaxima && distanciaTrans_TarX > distanciaMinima)
        {
            transform.rotation = Quaternion.identity;
        }

    }

    //método que controla el cambio de animaciones
    private void SetAnimacion (string name, bool loop) 
    {
        if (name == animacionActual) return;
        npcAnimation.state.SetAnimation(0, name, loop);
        animacionActual = name; 
    }

   
}
