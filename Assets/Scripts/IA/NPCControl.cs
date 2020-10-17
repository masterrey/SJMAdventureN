using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NPCControl : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject[] checkpoints;
    public Animator anim;
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
        checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        StartCoroutine(Idle());
    }

    void SetState(States current)
    {
        anim.SetInteger("State",(int)current);
        StartCoroutine(current.ToString());
    }

    IEnumerator Idle()
    {
        state = States.Idle;
        float waittime = Random.Range(1, 15);

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
        while (Vector3.Distance(objective.transform.position, transform.position) > 2)
        {
            anim.SetFloat("Vel", agent.velocity.magnitude);
            yield return new WaitForFixedUpdate();
        }
        SetState(States.Idle);
    }


}
