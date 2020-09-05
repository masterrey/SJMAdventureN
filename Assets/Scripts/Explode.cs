using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public GameObject explosion;
    public int lives = 0;
    public void Damage()
    {
        lives--;
        if (lives < 1)
        {
            StartCoroutine(Explosion());
        }
    }
    public void DamageHard()
    {

        StartCoroutine(Explosion());

    }


    IEnumerator Explosion()
    {
       Instantiate(explosion, transform.position, Quaternion.identity);
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, 20, Vector3.up, 30);

        yield return new WaitForSeconds(0.1f);

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.gameObject == gameObject)
            {
                break;
            }
            yield return new WaitForSeconds(0.01f);
            Explode exp = hit.collider.GetComponent<Explode>();
            
            Rigidbody rdb = hit.collider.GetComponent<Rigidbody>();
            if (rdb)
            {
                rdb.AddExplosionForce(2000, transform.position, 30);
            }

            if (exp)
            {
                exp.DamageHard();
            }
        }
        
        Destroy(gameObject, 0.2f);
    }


}
