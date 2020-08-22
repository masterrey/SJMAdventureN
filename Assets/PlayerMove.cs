using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody rdb;
    public Animator anim;
    Vector3 mov;
    GameObject gamecamera;
    // Start is called before the first frame update
    void Start()
    {
        gamecamera = Camera.main.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        mov = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }

    private void FixedUpdate()
    {
        rdb.velocity = gamecamera.transform.TransformDirection(mov*10);

        transform.forward = gamecamera.transform.forward;

        anim.SetFloat("Walk", rdb.velocity.magnitude);
    }
}
