using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudController : MonoBehaviour {
    [SerializeField]
    private Player player;

    [SerializeField]
    private GameObject prefabOrbe;

    [SerializeField]
    private Vector3 sumatorioPosicion;
   

	// Use this for initialization
	void Start () {       

        sumatorioPosicion = new Vector3(40,0,0);
        
	}

    // Update is called once per frame
    void Update()
    {

        if (player.GetVida() != transform.childCount)
        {

            for (int i = 0; i < transform.childCount; i++)
            {
                GameObject go = transform.GetChild(i).gameObject;
                Destroy(go);                
            }

            for (int i = 0; i < player.GetVida(); i++)
            {                
                GameObject go = Instantiate(prefabOrbe, transform.position, Quaternion.identity);                              
                go.transform.SetParent(transform);
            }

            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.transform.position = transform.position + (sumatorioPosicion * (i + 1));
            }


        }
    }
}
