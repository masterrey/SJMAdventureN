using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NPCCarControl : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject[] checkpoints;

    public WheelCollider DD;
    public WheelCollider DE;
    public WheelCollider TD;
    public WheelCollider TE;
    private Vector3 mov;
    public float MotorTorque = 100;
    public float BrakeTorque = 100;
    public Rigidbody rdb;

    public float stoppedcounter=0;
    public float reartime =0;
    public enum States
    {
        Idle,
        Walk,
        Runaway
    }
    public States state;

    // Start is called before the first frame update
    void Start()
    {
        rdb = GetComponent<Rigidbody>();
        rdb.centerOfMass += new Vector3(0, -0.8f, 0);
        checkpoints = GameObject.FindGameObjectsWithTag("Waypoint");
        StartCoroutine(Idle());
    }

    void SetState(States current)
    {

        StartCoroutine(current.ToString());
    }

    IEnumerator Idle()
    {
        state = States.Idle;
        float waittime = Random.Range(0, 2);
        mov = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(waittime);
        SetState(States.Walk);
    }

    IEnumerator Walk()
    {
        state = States.Walk;
        System.Random r = new System.Random();

        int rand = r.Next(0, checkpoints.Length);
        //GameObject objective = checkpoints[Random.Range(0, checkpoints.Length)];
        GameObject objective = checkpoints[rand];
        agent.SetDestination(objective.transform.position);
        while (Vector3.Distance(objective.transform.position, transform.position) > 20)
        {
            float acell = 1;
            if (rdb.velocity.magnitude < 2)
            {
                stoppedcounter += Time.fixedDeltaTime;
            }
            if (stoppedcounter > 5)
            {
                reartime = 4;
                stoppedcounter = 0;
            }

            if (reartime > 0)
            {
                acell = -1;
                reartime -= Time.fixedDeltaTime;
            }
            agent.transform.localPosition = Vector3.zero;
            agent.transform.localRotation = Quaternion.identity;
            Vector3 dir = agent.steeringTarget - transform.position;
            float ang = Vector3.SignedAngle(dir, transform.forward, Vector3.down);
            ang = Mathf.Clamp(ang, -30, 30);
            mov = new Vector3(ang, 0, acell);
            yield return new WaitForFixedUpdate();

           
        }
        SetState(States.Idle);
    }

    private void FixedUpdate()
    {
        DD.motorTorque = MotorTorque * mov.z;
        DE.motorTorque = MotorTorque * mov.z;
        TD.motorTorque = MotorTorque * mov.z;
        TE.motorTorque = MotorTorque * mov.z;
        DD.steerAngle = mov.x;
        DE.steerAngle = mov.x;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerDelete"))
        {
            Destroy(gameObject);
        }
    }
}
