using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody rdb;
    public Animator anim;
    Vector3 mov;
    GameObject gamecamera;
    public GameObject cameraAim;

    public delegate bool WantTo(out Transform tranf);
    public WantTo wantToBoard;
    public WantTo wantToOffBoard;
    public enum State
    {
        Combat,
        Board,
        Died
    }
    public State state;

    Transform sterringWeell;

    // Start is called before the first frame update
    void Start()
    {
        gamecamera = Camera.main.gameObject;
        ChangeState();
    }

    IEnumerator Combat()
    {
        while (state == State.Combat)
        {
            cameraAim.transform.forward = gamecamera.transform.forward;
            Vector3 cameraRelativeMov = gamecamera.transform.TransformDirection(mov); //movimento relativo a camera
            rdb.velocity = new Vector3(cameraRelativeMov.x * 4, rdb.velocity.y, cameraRelativeMov.z * 4); //mantenho o y como velocidade
            float radtogo = Vector3.Dot(transform.forward, -gamecamera.transform.right) * 5;
            transform.Rotate(0, radtogo, 0);
            Vector3 locVel = transform.InverseTransformDirection(rdb.velocity).normalized;

            anim.SetFloat("Walk", locVel.z);
            anim.SetFloat("SideWalk", locVel.x + radtogo);
            anim.SetFloat("Speed", rdb.velocity.magnitude + Mathf.Abs(radtogo));

            yield return new WaitForFixedUpdate();

        }
        ChangeState();
    }

    IEnumerator Board()
    {
        print("entrando");
        GetComponent<WeaponControl>().ChangeState(WeaponControl.State.NoWeapon);
        rdb.isKinematic = true;
        Collider[] cols = GetComponentsInChildren<Collider>();
        foreach(Collider col in cols)
        {
            col.enabled = false;
        }
        anim.SetBool("Board", true);

        while (state == State.Board)
        {
            cameraAim.transform.forward = gamecamera.transform.forward;

            yield return new WaitForFixedUpdate();
           
        }
        print("saindo");
        GetComponent<WeaponControl>().ChangeState(WeaponControl.State.Rifle);
        rdb.isKinematic = false;
        anim.SetBool("Board", false);
        foreach (Collider col in cols)
        {
            col.enabled = true;
        }
        ChangeState();
    }

    IEnumerator Died()
    {
        while (state == State.Died)
        {
            yield return new WaitForFixedUpdate();
        }
        ChangeState();
    }

    void ChangeState()
    {
       // StopAllCoroutines();
        StartCoroutine(state.ToString());
    }
    void ChangeState(State mystate)
    {
        state = mystate;
        //StopAllCoroutines();
        StartCoroutine(mystate.ToString());
    }



    // Update is called once per frame
    void Update()
    {
        mov = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (Input.GetButtonDown("Fire2"))
        {
            if (state == State.Board)
            {
                if (wantToOffBoard(out Transform mysteringWheel))
                { 
                    sterringWeell = null;
                    ChangeState(State.Combat);
                }
            }
            else
            {
                if (wantToBoard(out Transform mysteringWheel))
                {
                    sterringWeell = mysteringWheel;
                    ChangeState(State.Board);
                }
            }
        }

    }

    private void OnAnimatorIK(int layerIndex)
    {
       anim.SetBoneLocalRotation(HumanBodyBones.Spine,cameraAim.transform.localRotation);

        if (sterringWeell)
        {
            anim.SetIKPosition(AvatarIKGoal.RightHand, sterringWeell.position+ sterringWeell.right*0.2f- sterringWeell.forward*0.1f);
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
            anim.SetIKPosition(AvatarIKGoal.LeftHand, sterringWeell.position - sterringWeell.right * 0.2f - sterringWeell.forward * 0.1f);
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);

            anim.SetIKRotation(AvatarIKGoal.RightHand, sterringWeell.rotation*Quaternion.Euler(0,0,-90));
            anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
            anim.SetIKRotation(AvatarIKGoal.LeftHand, sterringWeell.rotation * Quaternion.Euler(0, 0, 90));
            anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
        }
    }
}
