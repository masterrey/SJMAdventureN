using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliControl : MonoBehaviour
{
    public GameObject rotor, tailrotor;
    private Vector3 mov;
    public float MotorTorque = 100;
    public Rigidbody rdb;
    public AudioSource aud;
    public bool onBoard = false;
    public Transform manche;

    // Start is called before the first frame update
    void Start()
    {
       
            rdb.centerOfMass += new Vector3(0, 0, 2);

           
    }

    void FixedUpdate()
    {
        if (onBoard)
        {
            rdb.AddRelativeForce(Vector3.up * MotorTorque);

            if (Input.GetButton("Fire3"))
            {
                rdb.AddForce(Vector3.up * 3000);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(onBoard)
            rotor.transform.Rotate(0, 160, 0);
    }
}
