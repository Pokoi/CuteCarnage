using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    #region Declaración de variables

    #region Variables de referencias a otros scripts

    /// <summary>
    /// Referencia al script Partida Manager
    /// </summary>
    private PartidaManager PartidaManager;
    #endregion

    #region Variables necesarias para el movimiento

    #region Variables para andar
    /// <summary>
    /// Velocidad de movimiento del personaje
    /// </summary>
    [Tooltip("Velocidad de movimiento del personaje")]
    [SerializeField]
    private float speed = 5;

    /// <summary>
    /// Transform del personaje
    /// </summary>    
    private Transform playerTransform;

    /// <summary>
    /// Eje X 
    /// </summary>
    private float ejex;

    #endregion

    #region Variables para el dash

    /// <summary>
    /// Fuerza que se aplica al personaje para el dash
    /// </summary>
    [Tooltip("Fuerza del dash del jugador")]
    [SerializeField]
    private float fuerzaDash = 3000;

    /// <summary>
    /// Input para realizar el dash
    /// </summary>
    [Tooltip("Tecla del dash")]
    [SerializeField]
    private KeyCode teclaDash;

    /// <summary>
    /// ¿Está el personaje realizando el dash?
    /// </summary>
    private bool dasheando;

    /// <summary>
    /// ¿Está el personaje mirando hacia la derecha?
    /// </summary>
    private bool mirandoDerecha;
    #endregion

    #region Variables para el salto
    /// <summary>
    /// Input para realizar el salto
    /// </summary>
    [Tooltip("Tecla para el salto")]
    [SerializeField]
    private KeyCode teclaSalto;

    /// <summary>
    /// Fuerza que se le aplica al personaje para realizar el salto
    /// </summary>
    [Tooltip("Fuerza del salto")]
    [SerializeField]
    private float fuerzaSalto;

    /// <summary>
    /// Trnasform en los pies del personaje
    /// </summary>
    [Tooltip("Transform que hay a los pies del personaje")]
    [SerializeField]
    private Transform pies;

    /// <summary>
    /// Capa del suelo
    /// </summary>
    [Tooltip("Capa del suelo")]
    [SerializeField]
    private LayerMask suelo;

    /// <summary>
    /// Valores del círculo que comprueba si está el personaje en el suelo
    /// </summary>
    private float radioCirculo = 0.3f;

    /// <summary>
    /// ¿Está el personaje en el suelo?
    /// </summary>
    private bool enSuelo;
    #endregion

    #region Variables para el deslizamiento
    /* // Variables necesarias deslizar
     [SerializeField]
     private Transform paredCheck;//transform en el lateral del personaje
     [SerializeField]
     private LayerMask pared;
     [SerializeField]
     private float velocidadDeslizar;
     private bool enPared;*/
    #endregion

    #region Variables para agacharse

    /// <summary>
    /// Tecla para agacharse
    /// </summary>
    [Tooltip("Tecla para agacharse")]
    [SerializeField]
    private KeyCode teclaAgachar;

    /// <summary>
    /// ¿Está el jugador agachado?
    /// </summary>
    private bool agachando = false;

    #endregion

    #endregion

    #region Variables necesarias para las animaciones

    /// <summary>
    /// Esqueleto de Spine 1
    /// </summary>
    [Tooltip("Esqueleto de Spine del personaje")]
    [SerializeField]
    private SkeletonAnimation skeleton;

    /// <summary>
    /// GameObject que contiene el esqueleto de Spine 1
    /// </summary>
    [Tooltip("GameObject que contiene el esqueleto de Spine")]
    [SerializeField]
    private GameObject skeletonFatherObject;

    /// <summary>
    /// Nombre de la animación actual que está reproduciendo el personaje
    /// </summary>
    private string animacionActual = "";

    /// <summary>
    /// Animator del personaje
    /// </summary>
    public Animator animator;

    AnimatorStateInfo info;

    #endregion

    #region Variables necesarias para los ataques

    /// <summary>
    /// Collider del arañazo
    /// </summary>
    [Tooltip("Collider del arañazo")]
    [SerializeField]
    private Collider2D colliderArannazo;

    /// <summary>
    /// Collider de probar carne
    /// </summary>
    [Tooltip("Collider de probar carne")]
    [SerializeField]
    private Collider2D colliderProbarCarne;

    /// <summary>
    /// Input asignado al arañazo
    /// </summary>
    [Tooltip("Tecla para el arañazo")]
    [SerializeField]
    private KeyCode teclaArannazo;

    /// <summary>
    /// Input asignado a probar carne
    /// </summary>
    [Tooltip("Tecla para probar carne")]
    [SerializeField]
    private KeyCode teclaProbarCarne;

    /// <summary>
    /// ¿El personaje está atacando?
    /// </summary>
    private bool atacando;

    /// <summary>
    /// ¿Está arañando?
    /// </summary>
    private bool arannazo;

    /// <summary>
    /// ¿Está probando carne?
    /// </summary>
    private bool probarCarne;

    /// <summary>
    /// Timer de probar carne
    /// </summary>
    private float timerProbarCarne = 0;

    /// <summary>
    /// Cooldown de probar carne
    /// </summary>
    private bool cdProbarCarne;

    /// <summary>
    /// Cantidad del cooldown de probar carne
    /// </summary>
    private float cdProbarCarneAmount;

    /// <summary>
    /// Tiempo antes de aplicar la ralentización
    /// </summary>
    private float timeInicial;

    /// <summary>
    /// Tiempo antes de aplicar la ralentización
    /// </summary>
    [SerializeField]
    private Image cdProbarCarneImage;


    #endregion

    #region Parámetros del personaje y miscelánea

    /// <summary>
    /// Rigidbody del personaje
    /// </summary>
    private Rigidbody2D rb;

    /// <summary>
    /// Transform del último punto de respawn
    /// </summary>
    [Tooltip("Último punto de respawn")]
    [SerializeField]
    private Transform lastRespawn;

    [SerializeField]
    private Image rellenoVialImage;

    /// <summary>
    /// Prefab del punto de respawn 
    /// </summary>         
    [SerializeField]
    private GameObject respawn;

    /// <summary>
    /// Vida actual del personaje
    /// </summary>
    private int vida;

    /// <summary>
    /// Vida máxima del personaje
    /// </summary>
    private int vidaMax;

    /// <summary>
    /// Maná actual del personaje
    /// </summary>
    private int mana;

    /// <summary>
    /// Maná máximo del personaje
    /// </summary>
    private int manaMax;

    /// <summary>
    /// Monedas del personaje
    /// </summary>
    private int monedas;

    /// <summary>
    /// Rabia actual del personaje
    /// </summary>
    [SerializeField] private float rabia;

    /// <summary>
    /// Rabia máxima del personaje
    /// </summary>
    private float rabiaMax;

    /// <summary>
    /// Daño que ejerce el personaje
    /// </summary>
    [Tooltip("Daño que hace el personaje")]
    [SerializeField]
    private int danno;

    /// <summary>
    /// Nombre del personaje
    /// </summary>
    private string nombre;

    #endregion

    #endregion

    #region Awake, Start, Update

    private void Awake()
    {
        //Inicialización de los valores de vida, mana y daño 
        vida = vidaMax = 5;
        mana = manaMax = 7;
        danno = 1;
        monedas = 0;
        nombre = "";

        //Inicialización valores ataque
        atacando = false;
        arannazo = false;

        //Inicialización valores salto
        enSuelo = false;

        cdProbarCarneImage.gameObject.SetActive(false);
        cdProbarCarne = false;
        cdProbarCarneAmount = 0;
        colliderArannazo.enabled = false;
        colliderProbarCarne.enabled = false;
        animator = transform.GetChild(0).GetComponent<Animator>();
        
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        playerTransform = transform;
        animator.SetBool("mirarDerecha", true);
        rabiaMax = 10f;
       
        
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(ejex, GetComponent<Rigidbody2D>().velocity.y);
        enSuelo = Physics2D.OverlapCircle(pies.position, radioCirculo, suelo);//Comprueba si estas en el suelo

    }

    void Update()
    {

        
        ejex = Input.GetAxis("Horizontal") * speed;
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("muerte dcha") || animator.GetCurrentAnimatorStateInfo(0).IsName("muerte izq")) ejex = 0;
        if (ejex == 0)
            animator.SetBool("correr", false);
        if (mirandoDerecha)
            animator.SetBool("mirarDerecha", true);
        else
            animator.SetBool("mirarDerecha", false);
        if (ejex != 0 && enSuelo) Movimiento(); //Si pulsas la tecla de movimiento       
        if (Input.GetKeyDown(teclaArannazo)) Arannazo();//Si pulsas la tecla de ataque arañazo
        if (Input.GetKeyDown(teclaProbarCarne)) ProbarCarne(); //Si pulsas la tecla de probar carne
        if (Input.GetKeyDown(teclaAgachar)) Agachar("inicio");
        if (Input.GetKeyUp(teclaAgachar)) Agachar("final");
        if (Input.GetKeyDown(teclaSalto) && enSuelo) animator.SetTrigger("saltar"); //si pulsas la tecla de salto       
        if (cdProbarCarne)
        {
            cdProbarCarneAmount -= Time.deltaTime * 10;
            cdProbarCarneImage.GetComponent<Image>().fillAmount = cdProbarCarneAmount / 100;
        }

        if (cdProbarCarneImage.GetComponent<Image>().fillAmount == 0) cdProbarCarne = false;
        rellenoVialImage.GetComponent<Image>().fillAmount = rabia/rabiaMax;

        

        

    }



    #endregion

    #region Movimiento

    /// <summary>
    /// Método que controla el movimiento de andar 
    /// </summary>
    public void Movimiento()
    {
        if (ejex > 0)
        {
            playerTransform.localRotation = Quaternion.Euler(0, 0, 0);
            animator.SetBool("correr", true);
            mirandoDerecha = true;
            //if (!enSuelo && enPared) Deslizar();
        }

        if (ejex < 0)
        {
            playerTransform.localRotation = Quaternion.Euler(0, 180, 0);
            animator.SetBool("correr", true);
            mirandoDerecha = false;
            //if (!enSuelo && enPared) Deslizar();
        }
    }

    /// <summary>
    /// Método que controla el acto de agacharse
    /// </summary>
    /// <param name="fase">Fase del acto de agacharse en la que se encuentra</param>
    public void Agachar(string fase)
    {
        if (fase == "inicio") animator.SetTrigger("agachar");

        if (fase == "final")
        {
            animator.SetTrigger("transicionLevantar");
        }

    }

    /// <summary>
    /// Método que controla los teletransportes
    /// </summary>
    /// <param name="destino">Transform del destino del teletransporte</param>
    public void Tp(Transform destino)
    {
        transform.position = destino.position;
    }

    /// <summary>
    /// Método que controla el dash
    /// </summary>
    public void Dash()
    {
        dasheando = true;
        if (mirandoDerecha)
        {
            rb.AddForce(new Vector2(ejex + fuerzaDash, rb.velocity.y));

        }
        else
        {
            rb.AddForce(new Vector2(ejex - fuerzaDash, rb.velocity.y));

        }
        //CD de dash? o cuantas veces se pude usar?
    }


    /// <summary>
    /// Método que controla el salto
    /// </summary>
    public void Saltar()
    {
        rb.velocity = new Vector2(rb.velocity.x, fuerzaSalto);
        
    }

    /// <summary>
    /// Método que controla el deslizamiento vertical por las paredes
    /// </summary>
    /* public void Deslizar()
     {
         rb.velocity = new Vector2(rb.velocity.x, velocidadDeslizar);
         // SetAnimacion("Slide", true);
         if (Input.GetKeyDown(teclaSalto) && enPared) Saltar();//Si apretamos saltar mientras deslizamos
     }*/

    #endregion

    #region Animaciones

    /// <summary>
    /// Método que controla el cambio de las animaciones
    /// </summary>
    /// <param name="name"> Nombre de la animación </param>
    /// <param name="loop"> ¿Se reproduce en bucle? </param>
    /// <param name="esqueleto"> Esqueleto de Spine al que pertenece dicha animación </param>
    private void SetAnimacion(string name, bool loop, SkeletonAnimation esqueleto, bool add = false)
    {
        if (name == animacionActual) return;
        if (!add) esqueleto.state.SetAnimation(0, name, loop);
        else esqueleto.state.AddAnimation(0, name, loop, 0);

        animacionActual = name;
    }

    #endregion

    #region Ataques

    /// <summary>
    /// Método que realiza el ataque del arañazo
    /// </summary>
    public void Arannazo()
    {
        arannazo = true;
        Ataque(arannazo, colliderArannazo);
        colliderArannazo.enabled = true;
        animator.SetTrigger("atacar");//Reproducimos la animación de arañazo
        atacando = true;
        Invoke("DesactivarColliderAtaque", 0.8f);
    }

    /// <summary>
    /// Método que desactiva el collider del ataque
    /// </summary>
    private void DesactivarColliderAtaque()
    {
        Debug.Log("adios collider");
        colliderArannazo.enabled = false;
        arannazo = true;
    }

    /// <summary>
    /// Método que desactiva el collider de probar carne
    /// </summary>
    private void DesactivarColliderProbarCarne()
    {
        Debug.Log("adios collider");
        colliderProbarCarne.enabled = false;
    }

    /// <summary>
    /// Método que controla comer carne
    /// </summary>
    public void ProbarCarne()
    {
        if (!cdProbarCarne)
        {
            probarCarne = true;
            colliderProbarCarne.enabled = true;
            animator.SetTrigger("comer"); //Reproducimos la animación de probar carne
            cdProbarCarneImage.gameObject.SetActive(true);
            cdProbarCarneAmount = 100;
            cdProbarCarne = true;
            Curar(1);
            atacando = true;
            Invoke("DesactivarColliderProbarCarne", 0.8f);
        }

    }

    /// <summary>
    /// Método genérico para todos los ataques. Establece el CD, cambia los estados y activa su trigger
    /// </summary>
    /// <param name="estado"> Booleano de dicho ataque</param>
    /// <param name="trigger"> Trigger del ataque en cuestión</param>
    private void Ataque(bool estado, Collider2D trigger)
    {
        estado = true;
        trigger.enabled = true;
        atacando = false;
        estado = false;
        trigger.enabled = false;

    }

    #endregion

    #region Recibir daño y muerte

    /// <summary>
    /// Método que controla la reducción de vida al recibir daño 
    /// </summary>
    /// <param name="dannoRecibido"> Daño que recibe el personaje </param>
    public void RecibirDanno(int dannoRecibido)
    {
        if (vida > 1)
        {
            animator.SetTrigger("recibirDanno");
        }
        vida--;

        if (vida == 0)
        {
            animator.SetTrigger("morir");
        }

        Debug.Log(vida + "player");
    }

    /// <summary>
    /// Método que controla la muerte del personaje
    /// </summary>
    public void Muerte()
    {
       
        Tp(lastRespawn);
        vida = vidaMax;
    }

    #endregion

    #region Getters, setters y miscelánea

    /// <summary>
    /// Método que devuelve la vida del personaje
    /// </summary>
    /// <returns></returns>
    public int GetVida()
    {
        return vida;
    }

    /// <summary>
    /// Método que devuelve el maná del personaje
    /// </summary>
    /// <returns></returns>
    public int GetMana()
    {
        return mana;
    }

    /// <summary>
    /// Método que devuelve el valor del boolean que indica si está realizando un arañazo
    /// </summary>
    /// <returns></returns>
    public bool GetArannazo()
    {
        return arannazo;
    }

    /// <summary>
    /// Método que devuelve el valor del boolean que indica si está realizando probarCarne
    /// </summary>
    /// <returns></returns>
    public bool GetProbarCarne()
    {
        return probarCarne;
    }

    /// <summary>
    /// Método que devuelve el daño que hace el personaje
    /// </summary>
    /// <returns></returns>
	public int GetDanno()
    {

        return danno;
    }

    /// <summary>
    /// Método que devuelve las monedas que posee el personaje
    /// </summary>
    /// <returns></returns>
    int GetMonedas()
    {
        return monedas;
    }

    /// <summary>
    /// Método que devuelve el nombre del personaje
    /// </summary>
    /// <returns></returns>
    string GetNombre()
    {
        return nombre;
    }

    /// <summary>
    /// Método que establece el nombre del personaje
    /// </summary>
    /// <param name="_nombre"> Nombre que recibe el personaje </param>
    void SetNombre(string _nombre)
    {
        nombre = _nombre;
    }

    /// <summary>
    /// Método que amplía la vida máxima del personaje
    /// </summary>
    /// <param name="potenciador">Cantidad de vida que se amplía</param>
    public void AmplioSalud(int potenciador)
    {
        vidaMax += potenciador;
    }

    /// <summary>
    /// Método que amplía la rabia máxima del personaje
    /// </summary>
    /// <param name="potenciador"> Cantidad de rabia que se amplía </param>
    public void AmplioRabia(int potenciador)
    {
        rabiaMax += potenciador;
    }

    /// <summary>
    /// Método que restaura parte de la vida del personaje
    /// </summary>
    /// <param name="cura"> Cantidad de vida que se restaura</param>
    public void Curar(int cura)
    {
        vida += cura;
        if (vida > vidaMax) vida = vidaMax;
    }

    /// <summary>
    /// Método que regenera parte del maná del personaje
    /// </summary>
    /// <param name="regenMana"> Cantidad de maná que se regenera</param>
    public void RegenerarMana(int regenMana)
    {
        mana += regenMana;
        if (mana > manaMax) mana = manaMax;
    }

    /// <summary>
    /// Método que aumenta la cantidad de monedas actuales del personaje
    /// </summary>
    /// <param name="monedasGanadas"> Cantidad de monedas que se aumenta </param>
    public void SubirMonedas(int monedasGanadas)
    {
        monedas += monedasGanadas;
    }

    /// <summary>
    /// Método que genera un punto de respawn 
    /// </summary>
    void GenerarPuntoRespawn()
    {
        Destroy(lastRespawn.parent);
        lastRespawn = Instantiate(respawn).transform;
    }
    #endregion

}
