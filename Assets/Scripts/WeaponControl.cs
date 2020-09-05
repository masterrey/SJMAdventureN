using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class WeaponControl : MonoBehaviour
{
    public Animator anim;
    public VisualEffect vfxmuzzle;
    public VisualEffect vfxricochete;
    public AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (!anim.GetBool("Shoot") && Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(ShootSingle());
        }


    }

    IEnumerator ShootSingle()
    {
        anim.SetBool("Shoot", true);
        
        yield return new WaitForSeconds(0.1f);
        vfxmuzzle.Play();
        source.pitch = Random.Range(0.9f, 1.1f);
        source.Play();
        Rigidbody rdb=null;
        Vector3 point=Vector3.zero;
        Explode exp = null;
        if (Physics.Raycast(vfxmuzzle.transform.position, vfxmuzzle.transform.forward, out RaycastHit hit, 100))
        {
            vfxricochete.transform.position = hit.point;
            rdb = hit.collider.GetComponent<Rigidbody>();
            point= hit.point;
            exp = hit.collider.GetComponent<Explode>();
        }

        yield return new WaitForSeconds(0.1f);
        vfxricochete.Play();
        if (rdb)
        {

            if (point.magnitude > 0)
            {
                rdb.AddForceAtPosition(vfxmuzzle.transform.forward * 10, point, ForceMode.Impulse);
            }
            else
            {
                rdb.AddForce(vfxmuzzle.transform.forward * 10, ForceMode.Impulse);
            }
        }
        if (exp)
        {
            exp.Damage();
        }
        yield return new WaitForSeconds(0.1f);
        anim.SetBool("Shoot", false);
    }
}
