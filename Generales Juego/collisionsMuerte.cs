using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionsMuerte : MonoBehaviour {

    [SerializeField]
    private Player player;

    private void OnTriggerEnter2D(Collider2D otro)
    {
            if (otro.isTrigger == true) //lo que colisiona no es un trigger:
            {
            if (otro.CompareTag("Player"))
            {
                player = otro.GetComponent<Player>();
                AnimatorStateInfo info = player.animator.GetCurrentAnimatorStateInfo(0);

                if (!info.IsName("muerte dcha") && !info.IsName("muerte izq"))

                    if (!info.IsName("muerte dcha") && !info.IsName("muerte izq"))
                    otro.SendMessage("Muerte");
            }
            }
        }
    

}
