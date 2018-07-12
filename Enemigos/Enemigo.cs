using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Enemigo : MonoBehaviour
{

    #region Declaración variables

    #region Variables necesarias del bloque de animaciones
    /// <summary>
    /// Esqueleto de Spine del enemigo
    /// </summary>
    [Tooltip("Esqueleto de Spine del enemigo")]
    [SerializeField]
    private SkeletonAnimation enemigoAnimation;

    /// <summary>
    /// Animación actual del enemigo
    /// </summary>
    private string animacionActual = "";

    #endregion

    #region Variables identificadoras

    public enum Tribu { inicial, luz, oscuridad };

    /// <summary>
    /// Tribu a la que pertenece el enemigo
    /// </summary>
    [Tooltip("Tribu a la que pertenece el enemigo")]
    [SerializeField]
    private Tribu tribu;

    public enum TipoEnemigo { basico, volador, bloqueo };
    /// <summary>
    /// Tipo de enemigo
    /// </summary>
    [Tooltip("Tipo de enemigo")]
    [SerializeField]
    private TipoEnemigo tipoEnemigo;

    /// <summary>
    /// Vida actual del enemigo
    /// </summary>
    private int vida;

    /// <summary>
    /// Vida máxima del enemigo
    /// </summary>
    private int vidaMax;

    /// <summary>
    /// Daño del enemigo
    /// </summary>         
    private int danno;

    #endregion

    #region Variables relativas al movimiento

    /// <summary>
    /// Velocidad de desplazamiento del enemigo
    /// </summary>
    [Tooltip("Velocidad de desplazamiento de este enemigo")]
    [SerializeField]
    private float moveSpeed = 3.2f;

    /// <summary>
    /// GameObject de este enemigo
    /// </summary>
    [Tooltip("GameObject de este enemigo")]
    [SerializeField]
    private GameObject este;

    /// <summary>
    /// Distancia máxima de reconocimiento del personaje
    /// </summary>
    [Tooltip("Distancia máxima de reconocimiento del personaje")]
    [SerializeField]
    private float distanciaMaxima;

    /// <summary>
    /// Distancia mínima de reconocimiento del personaje
    /// </summary>
    [Tooltip("Distancia mínima de reconocimiento del personaje")]
    [SerializeField]
    private float distanciaMinima;

    /// <summary>
    /// Distancia entre el personaje y el enemigo
    /// </summary>
    private Vector3 distancia;

    /// <summary>
    /// Transform del jugador
    /// </summary>
    [Tooltip("Transform del personaje del jugador")]
    [SerializeField]
    private Transform target;

    /// <summary>
    /// Transform que tiene el enemigo en los pies
    /// </summary>
    [Tooltip("Transform que tiene el enemigo en los pies")]
    [SerializeField]
    Transform piesesitosMalos;

    /// <summary>
    /// Capa del suelo
    /// </summary>
    [Tooltip("Capa del suelo")]
    [SerializeField]
    LayerMask suelo;

    /// <summary>
    /// Referencia al script Player del jugador
    /// </summary>
    private Player player;

    /// <summary>
    /// Es true si el enemigo no se va a caer y false si se va a caer
    /// </summary>
    private bool noTeCaigasEnemigo;

    /// <summary>
    /// Distancia entre el personaje y el enemigo en el eje X estando el personaje a la derecha del enemigo
    /// </summary>
    private float distanciaTar_TransX;

    /// <summary>
    /// Distancia entre el personaje y el enemigo en el eje X estando el personaje a la izquierda del enemigo
    /// </summary>
    private float distanciaTrans_TarX;

    /// <summary>
    /// Distancia entre el personaje y el enemigo en el eje Y estando el personaje encima del enemigo
    /// </summary>
    private float distanciaTar_TransY;

    /// <summary>
    /// Distancia entre el personaje y el enemigo en el eje Y estando el personaje debajo del enemigo
    /// </summary>
    private float distanciaTrans_TarY;

    /// <summary>
    /// ¿Está el enemigo moviéndose?
    /// </summary>
    private bool moviendo;

    /// <summary>
    /// Posición inicial del movimiento de patrulla
    /// </summary>
    private Vector3 puntoInicialPatrulla;

    /// <summary>
    /// Posición final del movimiento de patrulla
    /// </summary>
    private Vector3 puntoFinalPatrulla;

    /// <summary>
    /// ¿Va hacia el punto final?
    /// </summary>
    private bool direccionPuntoFinalPatrulla;

    #endregion

    #region Variables relativas a los ataques

    /// <summary>
    /// Collider del ataque a melé
    /// </summary>
    [Tooltip("Collider del ataque a melé")]
    [SerializeField]
    private Collider2D colliderMele;

    /// <summary>
    /// Prefab del proyectil 
    /// </summary>
    [Tooltip("Prefab del proyectil")]
    [SerializeField]
    private GameObject prefabProyectil;

    /// <summary>
    /// Frecuencia de disparo
    /// </summary>
    [Tooltip("Frecuencia de disparo")]
    [SerializeField]
    private float fireRate = 0.7f;

    /// <summary>
    /// ¿Está el enemigo atacando?
    /// </summary>
    private bool atacando;

    private bool atEnemigoTutorial;

    /// <summary>
    /// Timer del ataque
    /// </summary>
    private float timerAtaque = 0;

    /// <summary>
    /// CD del ataque
    /// </summary>
    private float cdAtaque = 0.3f;

    /// <summary>
    /// Timer del disparo
    /// </summary>
    private float nextFire = 0.0f;

    /// <summary>
    /// Prefab de la carroña
    /// </summary>
    [Tooltip("Prefab de la carroña")]
    [SerializeField]
    private GameObject prefabCarronna;
    #endregion

    #endregion

    #region Awake, Update, Start

    private void Awake()
    {
        player = target.gameObject.GetComponent<Player>();
        if (tribu == Tribu.inicial)
        {
            if (tipoEnemigo == TipoEnemigo.basico) vida = vidaMax = 3;
            else if (tipoEnemigo == TipoEnemigo.volador) vida = vidaMax = 3;
            else if (tipoEnemigo == TipoEnemigo.bloqueo) vida = vidaMax = 3;
            danno = 1;
        }

        atacando = false;
        moviendo = false;
        if (tipoEnemigo == TipoEnemigo.basico) colliderMele.enabled = false;

        puntoInicialPatrulla = GetComponent<Transform>().position;
        puntoFinalPatrulla = new Vector3(puntoInicialPatrulla.x + 9, puntoInicialPatrulla.y + 1, puntoInicialPatrulla.z);
        direccionPuntoFinalPatrulla = true;
    }

    private void Update()
    {
        if (tipoEnemigo == TipoEnemigo.basico)
            noTeCaigasEnemigo = Physics2D.OverlapCircle(piesesitosMalos.position, 0.2f, suelo);

        distancia = target.position - transform.position;
        distancia = distancia.normalized;
        distanciaTar_TransX = target.position.x - transform.position.x;
        distanciaTrans_TarX = transform.position.x - target.position.x;
        distanciaTar_TransY = target.position.y - transform.position.y;
        distanciaTrans_TarY = transform.position.y - target.position.y;

        if (distanciaTar_TransX < distanciaMaxima && distanciaTar_TransX > distanciaMinima)
        {
            transform.rotation = Quaternion.identity;
            if (tipoEnemigo == TipoEnemigo.volador) MovimientoSeguimiento();
            if (tipoEnemigo == TipoEnemigo.basico && noTeCaigasEnemigo) MovimientoSeguimiento();
        }

        if (distanciaTrans_TarX < distanciaMaxima && distanciaTrans_TarX > distanciaMinima)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            if (tipoEnemigo == TipoEnemigo.volador) MovimientoSeguimiento();
            if (tipoEnemigo == TipoEnemigo.basico && noTeCaigasEnemigo) MovimientoSeguimiento();
        }

        if (distanciaTar_TransX > distanciaMaxima || distanciaTrans_TarX > distanciaMaxima)
        {
            if (tipoEnemigo == TipoEnemigo.volador) MovimientoPatrol();
        }

        if ((Mathf.Abs(distanciaTar_TransX) <= distanciaMinima || Mathf.Abs(distanciaTar_TransX) <= distanciaMinima) && (Mathf.Abs(distanciaTrans_TarY) <= distanciaMinima || Mathf.Abs(distanciaTar_TransY) <= distanciaMinima))
        {
            moviendo = false;
            if (tipoEnemigo == TipoEnemigo.basico)
                AtMele();
            else if (tipoEnemigo == TipoEnemigo.volador || tipoEnemigo == TipoEnemigo.bloqueo)
            {

                if (Time.time > nextFire && player.GetVida() > 0)
                {
                    nextFire = Time.time + fireRate;
                    AtDisparo();
                }
            }
        }
        
    }
    #endregion

    #region Movimiento
    
    /// <summary>
    /// Método que controla el sequimiento al jugador
    /// </summary>
    public void MovimientoSeguimiento()
    {
        moviendo = true;
        SetAnimacion("walk", true);
        Vector3 deltaMovement = distancia * moveSpeed * Time.deltaTime;
        deltaMovement.y = 0;
        transform.Translate(deltaMovement, Space.World);
    }

    /// <summary>
    /// Método que controla el movimiento de patrulla de los enemigos
    /// </summary>
    public void MovimientoPatrol()
    {
        if (Vector3.Distance(transform.position, puntoFinalPatrulla) >= 0.5f && direccionPuntoFinalPatrulla)
        {
            Debug.Log("Inicia el movimiento a punto final");
            transform.rotation = Quaternion.identity;
            SetAnimacion("walk", true);
            transform.position = Vector3.MoveTowards(transform.position, puntoFinalPatrulla, moveSpeed * Time.deltaTime);
        }
                
        if (Vector3.Distance(transform.position, puntoInicialPatrulla) >= 0.5f && !direccionPuntoFinalPatrulla)
        {
            Debug.Log("Inicia el movimiento a punto inicial");
            transform.rotation = Quaternion.Euler(0, 180, 0);
            SetAnimacion("walk", true);
            transform.position = Vector3.MoveTowards(transform.position, puntoInicialPatrulla, moveSpeed * Time.deltaTime);            
        }

        if (Vector3.Distance(transform.position, puntoFinalPatrulla) <= 0.6f) direccionPuntoFinalPatrulla = false;
        if (Vector3.Distance(transform.position, puntoInicialPatrulla) <= 0.6f) direccionPuntoFinalPatrulla = true;

    }

    #endregion

    #region Animaciones

    
    private void SetAnimacion(string name, bool loop)
    {
        if (name == animacionActual) return;
        enemigoAnimation.state.SetAnimation(0, name, loop);
        animacionActual = name;
    }

    #endregion

    #region Ataques

    /// <summary>
    /// Ataque melé de los enemigos
    /// </summary>
    public void AtMele()
    {
        Ataque(colliderMele);
        SetAnimacion("ataque", false);
    }

    /// <summary>
    /// Ataque a distancia de los enemigos
    /// </summary>
    public void AtDisparo()
    {
        if (tipoEnemigo == TipoEnemigo.volador)
            SetAnimacion("ataque", false);//Reproducimos la animación
        else if (tipoEnemigo == TipoEnemigo.bloqueo)
            SetAnimacion("animation", false);
        GameObject nuevoProyectil = Instantiate(prefabProyectil, new Vector3(transform.position.x, transform.position.y + 2.8f, transform.position.z), Quaternion.identity) as GameObject;
    }

    /// <summary>
    /// Método genérico de los ataques. Establece el CD, cambia los estados y activa su collider
    /// </summary>
    /// <param name="collider"> Collider del ataque que activar </param>
    private void Ataque(Collider2D collider)
    {
        atacando = true;
        timerAtaque = cdAtaque;
        collider.enabled = true;

        if (timerAtaque > 0) timerAtaque -= Time.deltaTime;
        else
        {
            atacando = false;
            collider.enabled = false;
        }
    }

    #endregion

    #region Recibir daño y muerte

    /// <summary>
    /// Método que controla la reducción de daño al recibir daño
    /// </summary>
    /// <param name="dannoRecibido">Cantidad de daño recibido</param>
    public void RecibirDanno(int dannoRecibido)
    {
        vida -= dannoRecibido;
        if (vida == 0)
        {
            Muerte();
        }
        Debug.Log(vida + "enemigo");

    }

    /// <summary>
    /// Método de muerte del enemigo
    /// </summary>
    public void Muerte()
    {
        GameObject carronna = Instantiate(prefabCarronna, transform.position, Quaternion.identity) as GameObject;
        este.SetActive(false);

    }

    #endregion

    #region Getters y Setters

    /// <summary>
    /// Devuelve si el enemigo está atacando o no 
    /// </summary>
    /// <returns></returns>
    public bool GetAtacando()
    {
        return atacando;
    }

    /// <summary>
    /// Devuelve el daño que hace este enemigo
    /// </summary>
    /// <returns></returns>
    public int GetDanno()
    {
        return danno;
    }

    /// <summary>
    /// Devuelve el tipo de enemigo que es este enemigo
    /// </summary>
    /// <returns></returns>
    public TipoEnemigo GetTipo()
    {
        return tipoEnemigo;
    }

    #endregion

}
