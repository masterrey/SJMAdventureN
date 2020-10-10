using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SensorLoading : MonoBehaviour
{
    public string levelname;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            if (SceneManager.sceneCount<2)
            {
                SceneManager.LoadSceneAsync(levelname, LoadSceneMode.Additive);
                print("carregando");
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Vector3.Distance(transform.position, other.transform.position) > GetComponent<SphereCollider>().radius*.8f )
            {
                print("descarregando");
                SceneManager.UnloadSceneAsync(levelname);
            }
        }
    }
}
