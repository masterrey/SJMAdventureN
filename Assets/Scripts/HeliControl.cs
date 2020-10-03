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
    public float motorpower=0;
    public AudioSource helisound;
    // Start is called before the first frame update
    void Start()
    {
       
            rdb.centerOfMass += new Vector3(0, 0, 2);

           
    }
    void Update()
    {
        motorpower = Mathf.Clamp01(motorpower);
        rotor.transform.Rotate(0, 44 * motorpower, 0);
        helisound.pitch = motorpower;
        helisound.volume = motorpower;

        if (onBoard)
        {
            mov = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            //SteeringWeel.localRotation = Quaternion.Euler(24, 0, mov.x * -150);
        }
        else
        {
            mov = new Vector3(mov.x, 0, 0);
        }
    }
    void FixedUpdate()
    {
        

        if (onBoard)
        {
            rdb.AddRelativeForce(Vector3.up * MotorTorque *motorpower);


            rdb.AddRelativeTorque(Vector3.right * mov.z * 10000);
            rdb.AddRelativeTorque(Vector3.up * mov.x * 10000);

            float contratorquez = Vector3.Dot(Vector3.up, transform.right);

            rdb.AddRelativeTorque(Vector3.forward * contratorquez * -10000);

            motorpower += Time.fixedDeltaTime/5;
           
            if (Input.GetButton("Fire3"))
            {
                rdb.AddForce(Vector3.up * 5000);
            }
        }
        else
        {
            motorpower = Mathf.Lerp(motorpower, 0, Time.fixedDeltaTime/5);
        }
    }
   
   
}
