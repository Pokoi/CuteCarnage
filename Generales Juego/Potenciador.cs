using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Potenciador : MonoBehaviour {

   [SerializeField] private Player Player;
   
    private enum TipoOrbe { orbeSalud, orbeMana, orbeRabia}
    [SerializeField]
    private TipoOrbe tipoOrbe;
    [SerializeField]
    private SkeletonAnimation skeleton;
    [SerializeField]
    private int amplificadorSalud;
    [SerializeField]
    private int amplificadorRabia;
    [SerializeField]
    private int monedas;
    [SerializeField]
    private int mana;
    [SerializeField]
    private int cura;
    private string animacionActual = "";

    // Use this for initialization
    void Start () {
      if(tipoOrbe == TipoOrbe.orbeSalud) SetAnimacion("orbe vida", true, skeleton);
      else if (tipoOrbe == TipoOrbe.orbeRabia)SetAnimacion("orbe manarabia", true, skeleton);

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //método que pasa a player el valor del amplificador de salud al pasar por encima de él
    public void SetAmplificadorSalud()
    {
        Player.AmplioSalud(amplificadorSalud);
        gameObject.SetActive(false);
    }

    //método que pasa a player el valor del amplificador de rabia al pasar por encima de él
    public void SetAmplificadorRabia()
    {
        Player.AmplioRabia(amplificadorRabia);
        gameObject.SetActive(false);
    }

    //método que pasa a player el valor de la cura al pasar por encima de él
    public void SetCurar()
    {
        Player.Curar(cura);
        gameObject.SetActive(false);
    }

    //método que pasa a player el valor del amplificador de maná al pasar por encima de él
    public void SetMana()
    {
        Player.RegenerarMana(mana);
        gameObject.SetActive(false);
    }

    //método que pasa a player el valor de las monedas ganadas al pasar por encima de él
    public void SetMonedas()
    {
        Player.SubirMonedas(monedas);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D otro)
    {
        if (otro.gameObject.CompareTag("Player"))
            if (CompareTag("OrbeSalud")) SetAmplificadorSalud();
            else if (CompareTag("OrbeRabia")) SetAmplificadorRabia();
            //else if (CompareTag("OrbeMana")) SetAmplificadorSalud();
    }

    /// <summary>
    /// Método que controla el cambio de las animaciones
    /// </summary>
    /// <param name="name"> Nombre de la animación </param>
    /// <param name="loop"> ¿Se reproduce en bucle? </param>
    /// <param name="esqueleto"> Esqueleto de Spine al que pertenece dicha animación </param>
    private void SetAnimacion(string name, bool loop, SkeletonAnimation esqueleto)
    {
        if (name == animacionActual) return;
        esqueleto.state.SetAnimation(0, name, loop);
        animacionActual = name;

    }

}
