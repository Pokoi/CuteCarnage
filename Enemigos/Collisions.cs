using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions : MonoBehaviour {
    //ADJUNTAR ESTE SCRIPT A LOS ENEMIGOS Y AL ARAÑAZO
    [SerializeField] private Enemigo esteEnemigo;
    [SerializeField] private Carronna estaCarronna;
    [SerializeField] private Player player;

    private void OnCollisionEnter2D (Collision2D otro)
    {

        if (CompareTag("Enemigo"))
        {
            //Cuando el enemigo ataca al jugador
            if (esteEnemigo.GetAtacando()) //Si está atacando y lo que colisiona no es un trigger:
            {
                if (otro.gameObject.CompareTag("Player")) otro.gameObject.SendMessage("RecibirDanno", (esteEnemigo.GetDanno()));
                              

            }


        }

        else if (CompareTag("Arannazo"))
        {
            if (player.GetArannazo() && otro.gameObject.CompareTag("Enemigo")) otro.gameObject.SendMessage("RecibirDanno", (player.GetDanno()));
           
        }

        else if (CompareTag("Carronna"))
        {
            if (otro.gameObject.CompareTag("ProbarCarne"))
            {
               // if (player.GetProbarCarne())
                //{
                    estaCarronna.destruir();
                    player.Curar(estaCarronna.GetVida());
                
                //}
            }
                
            
        }

       
    }





}
