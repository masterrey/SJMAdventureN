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

    // Start is called before the first frame update
    void Start()
    {
        gamecamera = Camera.main.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        mov = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        cameraAim.transform.forward = gamecamera.transform.forward;

        
    }

    private void FixedUpdate()
    {
        Vector3 cameraRelativeMov = gamecamera.transform.TransformDirection(mov); //movimento relativo a camera
        rdb.velocity = new Vector3(cameraRelativeMov.x*4, rdb.velocity.y, cameraRelativeMov.z*4); //mantenho o y como velocidade
        float radtogo = Vector3.Dot(transform.forward, -gamecamera.transform.right)*5;
        transform.Rotate(0, radtogo, 0);
        Vector3 locVel = transform.InverseTransformDirection(rdb.velocity).normalized;



        anim.SetFloat("Walk", locVel.z);
        anim.SetFloat("SideWalk", locVel.x+ radtogo);
        anim.SetFloat("Speed", rdb.velocity.magnitude + Mathf.Abs(radtogo));
    }
    
    

    private void OnAnimatorIK(int layerIndex)
    {
       anim.SetBoneLocalRotation(HumanBodyBones.Spine,cameraAim.transform.localRotation);
    }
}
