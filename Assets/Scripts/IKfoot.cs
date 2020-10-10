using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKfoot : MonoBehaviour
{
    public Animator anim;
    public Transform footl, footr,spine;
    public float ikforce = 0.7f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

  
    private void OnAnimatorIK(int layerIndex)
    {
        if (Physics.Raycast(spine.position, footl.transform.position - spine.position, out RaycastHit hit, 2))
        {
            Debug.DrawLine(spine.position, hit.point, Color.red);
            
            anim.SetIKPosition(AvatarIKGoal.LeftFoot, hit.point+hit.normal*0.1f);
            anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, ikforce);
            anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, ikforce);
            anim.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation);
        }

        if (Physics.Raycast(spine.position, footr.transform.position - spine.position, out RaycastHit hit2, 2))
        {
            Debug.DrawLine(spine.position, hit2.point, Color.red);

            anim.SetIKPosition(AvatarIKGoal.RightFoot, hit2.point + hit.normal * 0.1f);
            anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, ikforce);
            anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, ikforce);
            anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, ikforce);
            anim.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation);
        }

       
    }
}
