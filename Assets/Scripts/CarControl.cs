using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControl : MonoBehaviour
{
    public WheelCollider DD;
    public WheelCollider DE;
    public WheelCollider TD;
    public WheelCollider TE;
    private Vector3 mov;
    public float MotorTorque = 100;
    public float BrakeTorque = 100;
    public Rigidbody rdb;
    public AudioSource aud;
    public bool onBoard = false;
    public Transform SteeringWeel;
    // Start is called before the first frame update
    void Start()
    {
        rdb.centerOfMass += new Vector3(0, -0.5f, 0);
    }
    private void Update()
    {
        if (onBoard)
        {
            mov = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            SteeringWeel.localRotation = Quaternion.Euler(24, 0, mov.x * -150);
        }
        else
        {
            mov = new Vector3(mov.x, 0, 0);
        }
    }


    void FixedUpdate()
    {
        float brake = 0;
        if (mov.z < 0)
        {
            brake = Mathf.Abs(mov.z)* BrakeTorque;
            mov.z = 0;
        }

        float motorrpm =1 + Mathf.Abs(mov.z) / 10 + (TD.rpm + TE.rpm) / 2000;
        aud.pitch = Mathf.Clamp(motorrpm,1,2f);

        TD.motorTorque = MotorTorque * mov.z;
        TE.motorTorque = MotorTorque * mov.z;
        DD.steerAngle = mov.x * 40;
        DE.steerAngle = mov.x * 40;

        DD.brakeTorque = brake*2;
        DE.brakeTorque = brake*2;
        TD.brakeTorque = brake;
        TE.brakeTorque = brake;
    }
}
