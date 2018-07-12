using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carronna : MonoBehaviour {

    #region Declaración de variables

    [Tooltip ("Prefab de la carroña")]
    /// <summary>
    /// GameObject de la carroña
    /// </summary>
    [SerializeField]
    private GameObject carronna;

    /// <summary>
    /// Vida que cura la carroña
    /// </summary>
    private int vidaACurar = 1;

    #endregion

    /// <summary>
    /// Método que destruye la carroña
    /// </summary>
    public void destruir()
    {
        Destroy(carronna);
    }

    /// <summary>
    /// Método que devuelve la cantidad de vida que cura esta carroña al ser consumida
    /// </summary>
    /// <returns> Vida que cura al consumirse </returns>
    public int GetVida()
    {
        return vidaACurar;
    }

}
