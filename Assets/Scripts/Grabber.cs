using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    public GameObject grabedob;
    bool grabed = false;
    public GameObject hand;
    public Animator anim;
    public PlayerMove playerMove;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (grabedob && Input.GetButtonDown("Fire2") && grabed)
        {
            grabedob.transform.parent = null;
            grabedob.GetComponent<Rigidbody>().isKinematic = false;
            grabedob.GetComponent<Collider>().isTrigger = false;
            grabed = false;
            grabedob = null;
            playerMove.grabedob = null;
        }
        if (grabedob && Input.GetButtonDown("Fire2"))
        {
            grabedob.transform.parent = hand.transform;
            grabedob.transform.localPosition = Vector3.zero;
            grabedob.GetComponent<Rigidbody>().isKinematic = true;
            grabedob.GetComponent<Collider>().isTrigger = true;
            grabed = true;

        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Grab"))
        {

            grabedob = other.gameObject;
            playerMove.grabedob = grabedob.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Grab"))
        {

            grabedob = null;
            playerMove.grabedob = null;
        }
    }
}
