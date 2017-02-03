using UnityEngine;
using System.Collections;

public class ItemControl : MonoBehaviour {

    public GameObject explosionPreFab;


    public void Explode()
    {
        // Instantiate an explosion where this GameOject is located
        Instantiate(explosionPreFab, transform.position, Quaternion.identity);
    }
}
