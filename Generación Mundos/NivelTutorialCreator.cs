using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Para que se pueda ejecutar desde el editor y así ver el nivel creado en Unity sin tener que ejecutarlo
[ExecuteInEditMode]

public class NivelTutorialCreator : MonoBehaviour {

    #region Declaración de las variables

    /// <summary>
    /// Boolean que impide que se cree más de una vez el nivel 
    /// </summary>
    [Tooltip ("Poner en false si se ha creado ya el nivel")][SerializeField]
    private bool crearNivel = true;

    /// <summary>
    /// Array de tiles de mezclas
    /// </summary>
    [SerializeField]
    private GameObject[] mezclas;

    /// <summary>
    /// Array de tiles de techos
    /// </summary>
    [SerializeField]
    private GameObject[] techos;

    /// <summary>
    /// Array de tiles de suelos
    /// </summary>
    [SerializeField]
    private GameObject[] suelos;

    /// <summary>
    /// Tile de muerte
    /// </summary>
    [SerializeField]
    private GameObject muerte;

    /// <summary>
    /// Array de las plataformas
    /// </summary>
    [SerializeField]
    private GameObject[] plataformas;

    /// <summary>
    /// Array de los laterales
    /// </summary>
    [SerializeField]
    private GameObject[] laterales;

    /// <summary>
    /// Array de los tiles de esquinas
    /// </summary>
    [SerializeField]
    private GameObject[] esquinas;

    /// <summary>
    /// Array de los tiles especiales
    /// </summary>
    [SerializeField]
    private GameObject[] especiales;

    /// <summary>
    /// Tamaño de cada tile
    /// </summary>
    [SerializeField]
    private float tamanio = 2.4f; 

    /// <summary>
    /// Número de filas que tiene el tablero del nivel
    /// </summary>
    private const int FILAS = 36;
   
    /// <summary>
    /// Número de columnas que tiene el tablero del nivel
    /// </summary>
    private const int COLUMNAS = 70;

    /// <summary>
    /// Transform del tablero, el objeto padre del nivel
    /// </summary>
    private Transform tablero;
  
    /// <summary>
    /// Array con los prefabs a instanciar en cada slot del tablero
    /// </summary>
    private GameObject[,] tiles = new GameObject[FILAS, COLUMNAS];

    #endregion


    /// <summary>
    /// Inicilización de los slots del tablero con sus prefabs correspondientes
    /// </summary>
    private void Awake()
    {
        #region ESQUINAS


        #region Tiles "esq_sup_izd_a_01"

        tiles [0, 33] = tiles [1, 2] = tiles [1, 32] = tiles [2, 1] = tiles [2, 31] = tiles [2, 55] =
        tiles [3, 54] = tiles [4, 53] = tiles [11, 8] = tiles [11, 18] = tiles [12, 6] = tiles [12, 16] = 
        tiles [13, 5] = tiles [13, 14] = tiles [14, 4] = tiles [14, 13] = tiles [14, 59] = tiles [15, 3] =
        tiles [15, 58] = tiles [16, 2] = tiles [16, 11] = tiles [16, 57]= tiles [17, 1] = tiles [17, 10] = 
        tiles [17, 56] = tiles [18, 9] = tiles [18,55] = tiles [19, 54] = tiles [20, 53] = tiles [30, 37] =
        tiles [31, 36] = esquinas[9];

        #endregion

        #region Tiles "esq_inf_izd_a_01"

        tiles[6, 1] = tiles[7, 2] = tiles[26, 0] = tiles[27, 1] = tiles[29, 16] = tiles[30, 26] = tiles[31, 27] = tiles[32, 28] =
        tiles[33, 29] = tiles[34, 30] = tiles[35, 31] = esquinas[3];

        #endregion

        #region Tiles "esq_sup_dch_a_01_01"
        tiles [0, 44] = tiles[1, 46] = tiles[2, 47] = tiles[3, 48] = tiles[4, 49] = tiles[4, 61] = tiles[3, 60] =
        tiles[12, 36] = tiles[13, 37] = tiles[14, 38] = tiles[15, 39] = tiles[17, 40] = tiles[18, 41] = tiles[20, 42] =
        tiles[18, 52] = tiles[16, 69] = tiles[15, 68] = tiles[14, 67] = tiles[19, 0] = tiles[26, 28] = tiles[27, 29] =
        tiles[28, 30] = tiles[29, 31] = tiles[30, 32] = tiles[31, 33] = tiles[30, 51] = tiles[31, 52] = tiles [25, 27] =esquinas[6];

        #endregion

        #region Tiles "esq_sup_dch_a_02_01"

        tiles [25, 27] = tiles [32, 55] = esquinas[7];

        #endregion

        #region Tiles "esq_inf_dch_a_02_01"

        tiles [24, 55] = esquinas[1];

        #endregion

        #region Tiles "esq_inf_dch_a_01_01"

        tiles[35, 55] = esquinas[0];

        #endregion

        #region Tiles "esq_inf_izd_b_01_01"

        tiles [5, 36] = tiles [5, 38] = tiles [5, 40] = tiles [5, 42] = tiles [7, 22] = tiles [7,26] = esquinas[4];

        #endregion

        #region Tiles "esq_inf_izd_b_01_02"

        tiles[5, 43] = tiles[7, 51]= esquinas[5];

        #endregion

        #region Tiles "esq_inf_dch_a_03_01"

        tiles[6, 34] = tiles [7, 33] = tiles[7, 61] = tiles[7, 33] = tiles[16, 10] = tiles[17, 9] = tiles[18, 8] = tiles[19, 7] =
        tiles[20, 6] = tiles[23, 56] = tiles[22, 57] = tiles[21, 58] = tiles[20, 59] = tiles[19, 69] = esquinas[2];

        #endregion

        #region Tiles "esq_sup_dch_a_03_01"

        tiles[1, 12] = tiles[2, 13] = tiles[2, 59] = tiles[11, 12] = tiles[11, 34] = esquinas[8];

        #endregion

        #endregion

        #region Tiles "muerte"

        tiles[7, 37] = tiles[7, 38] = tiles[7, 39] = tiles[7, 40] = tiles[7, 41] = tiles[9, 23] = tiles[9, 25] = tiles[24, 10] =
        tiles[24, 11] = tiles[24, 12] = tiles[24, 13] = tiles[24, 14] = tiles[24, 15] = tiles[24, 19] = tiles[24, 20] = tiles[24, 21] =
        tiles[24, 22] = tiles[24, 23] = tiles[24, 24] = tiles[24, 25] = tiles[24, 26] = tiles[24, 27] = tiles[24, 28] = tiles[24, 29] =
        tiles[24, 30] = tiles[24, 31] = tiles[24, 32] = tiles[24, 33] = tiles[24, 34] = tiles[24, 35] = tiles[24, 36] = tiles[24, 37] =
        tiles[24, 38] = tiles[24, 39] = tiles[35, 41] = tiles[35, 42] = tiles[35, 43] = tiles[35, 44] = tiles[35, 45] = tiles[35, 46] =
        muerte;

        #endregion

        #region TECHOS

        #region Tiles "techo_01"

        tiles[0, 34] = tiles [0, 37] = tiles [0, 40] = tiles [0, 43] = tiles [1, 3] = tiles [1, 6] = tiles [1, 9] =
        tiles [1, 45] = tiles [2, 56] = tiles [3, 14] = tiles [3, 17] = tiles [3, 20] = tiles [3, 23] = tiles [3, 26] =
        tiles [3, 29] = tiles [5, 50] = tiles [6, 39] = tiles [11, 9] = tiles [11, 19] = tiles [11, 22] = tiles [11, 25] =
        tiles [11, 28] = tiles [11, 31] = tiles [12, 7] = tiles [12, 17] = tiles [12, 35] = tiles [13, 15] = tiles [14, 60] =
        tiles [14, 63] = tiles [14, 66] = tiles [22, 6] = tiles [25, 4] = tiles [25, 7] = tiles [25, 10] = tiles [25, 13] =
        tiles [25, 19] = tiles [25, 22] = tiles [25, 25] = tiles [30, 38] = tiles [30, 41] = tiles [30, 44] = tiles [30, 47] =
        tiles [30, 50] = tiles [32, 34] = tiles [32, 53] = techos[0];

        #endregion

        #region Tiles "techo_02"

        tiles[0, 35] = tiles[0, 38] = tiles[0, 41]  = tiles[1, 4] = tiles[1, 7] = tiles[1, 10] =
        tiles[2, 57] = tiles[3, 15] = tiles[3, 18] = tiles[3, 21] = tiles[3, 24] = tiles[3, 27] =
        tiles[3, 30] = tiles[5, 51] = tiles[11, 10] = tiles[11, 20] = tiles[11, 23] = tiles[11, 26] =
        tiles[11, 29] = tiles[11, 32] = tiles[14, 61] =
        tiles[14, 64] = tiles[22, 7] = tiles[21, 42] = tiles [25, 5] = tiles[25, 8] = tiles[25, 11] = tiles[25, 14] =
        tiles[25, 20] = tiles[25, 23] = tiles[25, 26] = tiles[30, 39] = tiles[30, 42] = tiles[30, 45] = tiles[30, 48] =
        tiles[32, 35] = tiles[32, 54] = techos[1];

        #endregion

        #region Tiles "techo_03"

        tiles[0, 36] = tiles[0, 39] = tiles[0, 42] = tiles[1, 5] = tiles[1, 8] = tiles[1, 11] =
        tiles[2, 58] = tiles[3, 16] = tiles[3, 19] = tiles[3, 22] = tiles[3, 25] = tiles[3, 28] =
        tiles[5, 52] = tiles[11, 11] = tiles[11, 21] = tiles[11, 24] = tiles[11, 27] =
        tiles[11, 30] = tiles[11, 33] = tiles[14, 62] =
        tiles[14, 65] = tiles[22, 8] = tiles[25, 6] = tiles[25, 9] = tiles[25, 12] = tiles[25, 15] =
        tiles[25, 21] = tiles[25, 24] = tiles[30, 40] = tiles[30, 43] = tiles[30, 46] = tiles[30, 49] =
        techos[2];

        #endregion

        #endregion

        #region SUELOS

        #region Tiles "suelo_01"

        tiles[5,35] = tiles[5, 39] = tiles[24, 11] =
        tiles[7, 3] = tiles[7, 6] = tiles[7, 9] = tiles[7, 12] = tiles[7, 15] = tiles[7, 18] = tiles[7, 21] = tiles[7, 27] =
        tiles[7, 30] = tiles[7, 52] = tiles[7, 55] = tiles[7, 58] = tiles[15, 11] = tiles[19, 60] = tiles[19, 63] = tiles[19, 66] =
        tiles[24, 5] = tiles [24, 8] = tiles[24, 41] = tiles[24, 44] = tiles[24, 47] = tiles[24, 50] = tiles[24, 53] =
        tiles[27, 2] = tiles[27, 5] = tiles[27, 8] = tiles[27, 11] = tiles[27, 14] = tiles[29, 17] = tiles[29, 20] =
        tiles[29, 23] = tiles[35, 32] = tiles[35, 35] = tiles[35, 38] = tiles[35, 48] = tiles[35, 51] = tiles[35, 54] = suelos[0] ;

        #endregion

        #region Tiles "suelo_02"

        tiles[7, 4] = tiles[7, 7] = tiles[7, 10] = tiles[7, 13] = tiles[7, 16] = tiles[7, 19] = tiles[7, 28] =
        tiles[7, 31] = tiles[7, 53] = tiles[7, 56] = tiles[7, 59] = tiles[19, 61] = tiles[19, 64] = tiles[19, 67] =
        tiles[24, 6] = tiles[24, 42] = tiles[24, 45] = tiles[24, 48] = tiles[24, 51] = tiles[24, 54] =
        tiles[27, 3] = tiles[27, 6] = tiles[27, 9] = tiles[27, 12] = tiles[27, 15] = tiles[29, 18] = tiles[29, 21] =
        tiles[29, 24] = tiles[35, 33] = tiles[35, 36] = tiles[35, 39] = tiles[35, 49] = tiles[35, 52] = suelos[1];

        #endregion

        #region Tiles "suelo_03"

        tiles[7, 5] = tiles[7, 8] = tiles[7, 11] = tiles[7, 14] = tiles[7, 17] = tiles[7, 20] = tiles[7, 29] =
        tiles[7, 32] = tiles[7, 54] = tiles[7, 57] = tiles[7, 60] = tiles[19, 62] = tiles[19, 65] = tiles[19, 68] =
        tiles [24, 4] = tiles[24, 7] = tiles[24, 43] = tiles[24, 46] = tiles[24, 49] = tiles[24, 52] =
        tiles[27, 4] = tiles[27, 7] = tiles[27, 10] = tiles[27, 13] = tiles[29, 19] = tiles[29, 22] =
        tiles[29, 25] = tiles[35, 34] = tiles[35, 37] = tiles[35, 50] = tiles[35, 53] = suelos[2];

        #endregion

        #endregion

        #region ESPECIALES

        #region Tiles "especial_esquina_01"

        tiles[6,2] = tiles [26, 1] = tiles[30, 27] = tiles[31, 28] = tiles[32, 29] =
        tiles[33, 30] = tiles[34, 31] = especiales[0];

        #endregion

        #region Tiles "especial_esquina_02"

        tiles [1, 33] = tiles [2, 2] = tiles [2, 32] = tiles[3, 55] = tiles[4, 54] = tiles[13, 6] = tiles[14, 5] =
        tiles[14, 14] = tiles[15, 4] = tiles[15, 13] = tiles[15, 59] = tiles[16, 3] = tiles[16, 12] = tiles[16, 58] =
        tiles[2, 32] = tiles[17, 2] = tiles[17, 11] = tiles[17, 57] = tiles[18, 10] = tiles[18, 56] = tiles[19, 55] =
        tiles[20, 54] = tiles[21, 53] = tiles[31, 37] = especiales[1];

        #endregion

        #region Tiles "especial_esquina_03"

        tiles [2, 12] = especiales[2];

        #endregion

        #region Tiles "especial_esquina_05"

        tiles[3, 59] = especiales[4];

        #endregion

        #region Tiles "especial_esquina_04"

        tiles [2, 46] = tiles[3, 47] = tiles[4, 48] = tiles[4, 60] = tiles[13, 36] = tiles[14, 37] = tiles[15, 38] = tiles[15, 67] =
        tiles [16, 68] = tiles[17, 39] = tiles[18, 40] = tiles[19, 1] = tiles[20, 41] = tiles[21, 42] = tiles[26, 27] = tiles[27, 28] =
        tiles[28, 29] = tiles[29, 30] = tiles[30, 31] = tiles[31, 32] = tiles[31, 51] = especiales[3];

        #endregion

        #region Tiles "especial_esquina_06"

        tiles[7, 36] = tiles[7, 42] = tiles[9, 22] = tiles[9, 26] = especiales[5];

        #endregion

        #region Tiles "especial_esquina_08"

        tiles[6, 38] = tiles[6, 40] = especiales[7];

        #endregion

        #region Tiles "especial_esquina_09"

        tiles[24, 9] = tiles[24, 40] = tiles[35, 47] = especiales[8];

        #endregion

        #region Tiles "especial_esquina_07"

        tiles [6, 33] = tiles [5, 34] = tiles[15, 10] = tiles[16, 9] = tiles[17, 8] = tiles[18, 7] = tiles[19, 6] = tiles[19, 59] =
        tiles[20, 5] = tiles[20, 58] = tiles[21, 57] = tiles[22, 56] = tiles[23, 55] = especiales[6];

        #endregion

        

        #endregion

        #region LATERALES

        #region Tiles "lateral_01"

        tiles[3, 1] = tiles[5, 1] = tiles [5, 61] = tiles [6, 36] = tiles[6, 42] = tiles[6, 61] = tiles[7, 43] =
        tiles[8, 22] = tiles[8, 26] = tiles[9, 43] = tiles[9, 51] = tiles[11, 43] = tiles[11, 51] = tiles[13, 43] = tiles[13, 51] =
        tiles[15, 43] = tiles[15, 51] = tiles[17, 43] = tiles[17, 51] = tiles[19, 43] = tiles[19, 52] = tiles[17, 69] = tiles[18, 69] =
        tiles[12, 12] = tiles[14, 12] = tiles[16, 39] = tiles[19, 41] = tiles[18, 1] = tiles[19, 9] = tiles[20, 0] = tiles[21, 0] =
        tiles[21, 9] = tiles[23, 0] = tiles[25, 0] = tiles[28, 16] = tiles[33, 55] = tiles[34, 55] = laterales[0];

        #endregion

        #region Tiles "lateral_02"

        tiles[4, 1] = tiles[6, 43] = tiles[8, 43] = tiles[8, 51] = tiles[10, 43] = tiles[10, 51] = tiles[12, 43] = tiles[12, 51] =
        tiles[14, 43] = tiles[14, 51] = tiles[16, 43] = tiles[16, 51] = tiles[18, 43] = tiles[20, 43] = tiles[20, 52] = tiles [13, 12] =
        tiles[20, 9] = tiles[21, 5] = tiles[22, 0] = tiles[24, 0] = laterales[1];

        #endregion

        #endregion

        #region MEZCLAS

        #region Tiles "mezcla_04"

        tiles[1, 44] = tiles[5, 49] = tiles[12, 34] = tiles[22, 5] = tiles[25, 3] = tiles[25, 18] = tiles[32, 33] = tiles[32, 52] = mezclas[3];

        #endregion

        #region Tiles "mezcla_01"

        tiles[3, 13] = mezclas[0];

        #endregion

        #region Tiles "mezcla_02"

        tiles[4, 13] = mezclas[1];

        #endregion

        #region Tiles "mezcla_03"

        tiles[3, 31] = tiles[32, 36] = mezclas[2];

        #endregion

        #region Tiles "mezcla_05"

        tiles[5, 53] = tiles[22, 9] = tiles[25, 16] = mezclas[4];

        #endregion

        #region Tiles "mezcla_08"

        tiles[7, 24] = tiles[23, 3] = mezclas[7];

        #endregion

        #region Tiles "mezcla_07"

        tiles[8, 24] = mezclas[6];

        #endregion

        #region Tiles "mezcla_06"

        tiles[9, 24] = mezclas[5];

        #endregion

        #region Tiles "mezcla_09"

        tiles[18, 51] = mezclas[8];

        #endregion

        #region Tiles "mezcla_10"

        tiles[21, 52] = mezclas[9];

        #endregion

        #region Tiles "mezcla_11"

        tiles[21, 43] = mezclas[10];

        #endregion

        #region Tiles "mezcla_15"

        tiles[24, 16] = mezclas[14];

        #endregion

        #region Tiles "mezcla_14"

        tiles[24, 18] = mezclas[14];

        #endregion

        #region Tiles "mezcla_19"

        tiles[24, 3] = mezclas[18];

        #endregion

        #region Tiles "mezcla_18"

        tiles[27, 16] = mezclas[17];

        #endregion

        #region Tiles "mezcla_17"

        tiles[29, 26] = mezclas[16];

        #endregion

        #region Tiles "mezcla_13"

        tiles[35, 40] = mezclas[12];

        #endregion

        #region Tiles "mezcla_12"

        tiles[12, 8] = tiles[12, 18] = tiles[13, 16] = mezclas[11];

        #endregion

        #region Tiles "mezcla_16"

        tiles[15, 12] = mezclas[15];

        #endregion

        #endregion

        #region PLATAFORMAS

        #region Tiles "platf_01"

        tiles [ 5, 44] = tiles[7, 45] = tiles[7, 47] = tiles[9, 50] = tiles[10, 47] = tiles[12, 46] =
        tiles[13, 49] = tiles[14, 44] = tiles[15, 47] = tiles[17, 50] = tiles[18, 44] = tiles[20, 46] = tiles[19, 49] =
        tiles[22, 45] = tiles[15, 30] = tiles[19, 34] = tiles[19, 28] = tiles[19, 23] = tiles[21, 25] =
        tiles[21, 32] = tiles[22, 18] = tiles[22, 22] = tiles[22, 27] = tiles[23, 38] = tiles[24, 2] = tiles[33, 42] = 
        plataformas[0];

        #endregion

        #region Tiles "platf_02"

        tiles[33, 43] = tiles[33, 44] = tiles[22, 36] = tiles[22, 35] = tiles[21, 30] = tiles[21, 31] = tiles[15, 23] =
        tiles[15, 24] = tiles[15, 16] = tiles[15, 17] = tiles[16, 33] = tiles[16, 34] = tiles[17, 36] = tiles[17, 37] =
        tiles[17, 26] = tiles[17, 27] = tiles[17, 20] = tiles[17, 19] = tiles[22, 13] = tiles[18, 14] = plataformas[1];

        #endregion


        #endregion

    }


    void Start () {
        //Establecemos que el componente transform del tablero es el componente transform del GameObject que posee el script
        tablero = transform;

        if (crearNivel)
        {
            //Recorremos el tablero generando las instancias
            for (int x = 0; x < FILAS; x++)
            {
                for (int y = 0; y < COLUMNAS; y++)
                {
                    Instanciar(tiles[x, y], x, y, tablero);
                }
            }

            transform.rotation = Quaternion.Euler(0, 0, -90);
        }

    }

    /// <summary>
    /// Método que rellena los slots con instancias del prefab que le corresponda
    /// </summary>
    /// <param name="referencia"> Prefab a instanciar </param>
    /// <param name="posicionX"> Posición en X donde instanciarlo </param>
    /// <param name="posicionY"> Posición en Y donde instanciarlo </param>
    /// <param name="padre"> Transform del tablero para establecer su padre</param>
    void Instanciar( GameObject referencia, float posicionX, float posicionY, Transform padre) {

        //Si el slot tiene que estar vacío no vamos a instanciar nada 
        if (referencia != null)
        {
            //Comprobación de si hay que aplicarle mirror a esta instancia cuando se cree
            bool mirror = ComprobarMirror(posicionX, posicionY);
            

            //Cálculo de la posición en X y en Y de dónde instanciarlo
            posicionX = posicionX * tamanio;
            posicionY = posicionY * tamanio;

            //Creación de la instancia. Le damos el prefab, la posición y la rotación y le decimos que lo instancie como GameObject
            GameObject instancia = Instantiate(referencia, new Vector3(posicionX, posicionY, 0f), Quaternion.Euler(0 , 0, 90)) as GameObject;

            if (mirror) MirrorVertical(instancia.transform);
           

            //Establecemos que el padre de la instancia es el tablero. Esto nos ayuda a organizar la jerarquía de Unity
            instancia.transform.SetParent(padre);
        }                  
    }
    /// <summary>
    /// Método que comprueba si el tile tiene que sufrir un reflejo en base a la cuadrícula del nivel
    /// </summary>
    /// <param name="x"> coordenada en X de la cuadrícula </param>
    /// <param name="y"> coordenada en Y de la cuadrícula </param>
    /// <returns></returns>
    bool ComprobarMirror (float x, float y)
    {
        bool resultado = false;
         
        if (x == 5)
        {
            if (y == 61 || y == 38 || y == 42) resultado = true;
        }

        if (x == 6)
        {
            if (y == 38 || y == 42 || y == 61) resultado = true;
        }

        if (x == 7)
        {
            if (y == 42 || y == 26 || y == 51) resultado = true;
        }

        if (x == 8)
        {
            if (y == 26 || y == 51) resultado = true;
        }

        if (x == 9 || x == 10 || x == 11 || x == 12 || x == 13 || x == 14 || x == 15 || x == 16 || x == 17)
        {
            if (y == 51) resultado = true;
        }

        if (x == 9)
        {
            if (y == 26) resultado = true;
        }

        if (x == 12 || x == 13 || x == 14)
        {
            if (y == 12) resultado = true;
        }

        if (x == 16)
        {
            if (y == 39) resultado = true;
        }

        if (x == 17 || x == 18)
        {
            if (y == 69) resultado = true;
        }

        if (x == 19)
        {
            if (y == 0 || y == 1 || y == 41 || y == 52) resultado = true;
        }

        if (x == 20)
        {
            if (y == 52) resultado = true;
        }

        if (x == 21)
        {
            if (y == 5) resultado = true;
        }        

         if (x == 24)
        {
            if (y == 9 || y == 18) resultado = true;
        }


         if (x == 33 || x == 34)
        {
            if (y == 55) resultado = true;
        }

        return resultado;
    }
       

    /// <summary>
    /// Método que realiza el volteo vertical
    /// </summary>
    /// <param name="tile"> Tile a voltear </param>
    void MirrorVertical (Transform tile)
    {
        tile.transform.rotation = Quaternion.Euler(-180, 0, 90);
    }
    
}
