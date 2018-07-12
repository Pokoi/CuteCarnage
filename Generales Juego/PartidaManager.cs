using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartidaManager : MonoBehaviour {

    //variable para la ralentización del juego
    [SerializeField] private float ralentizacion;
    [SerializeField] private float duracionRalentizacion;

    private float timeInicial;




   public void Ralentizacion()
    {
        timeInicial = Time.time;
        while (Time.time <= (timeInicial + duracionRalentizacion)) Time.timeScale = ralentizacion;
        Time.timeScale = 1.0f;
    }

}
