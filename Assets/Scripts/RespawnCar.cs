using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnCar : MonoBehaviour
{

    public GameObject Prefabcar;
    public GameObject Instancecar;
    // Start is called before the first frame update


    private void OnTriggerEnter(Collider other)
    {
        if (!Instancecar)
        {
            if (other.CompareTag("PlayerRadar"))
            {
                Instancecar = Instantiate(Prefabcar, transform.position, transform.rotation);
            }
        }

    }

}
