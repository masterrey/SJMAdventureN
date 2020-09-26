using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliBoardControl : BoardControl
{
    public HeliControl heliControl;
    protected override void CallToBoard()
    {
        heliControl.onBoard = true;
    }
    IEnumerator OpenDoor()
    {
        float time = 0;
        doorsource.PlayOneShot(open);
        while (time <= 1)
        {
            yield return new WaitForFixedUpdate();

            time += Time.fixedDeltaTime * 2;
            door.transform.localRotation =
                Quaternion.Lerp(Quaternion.Euler(-90, 0, 0)
                , Quaternion.Euler(-90, 0, 120), time);
        }
    }

    IEnumerator CloseDoor()
    {
        float ang = 120;

        while (ang >= 0)
        {
            yield return new WaitForFixedUpdate();

            ang -= Time.fixedDeltaTime * 200;
            door.transform.localRotation = Quaternion.Euler(-90, 0, ang);
        }
        doorsource.PlayOneShot(close);
        door.transform.localRotation = Quaternion.Euler(-90, 0, 0);
    }
}
