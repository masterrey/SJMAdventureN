using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardControl : MonoBehaviour
{
    public GameObject door;
    PlayerMove playerMove;
    public CarControl carControl;
    public Transform carSeat;
    public Transform sterringWheel;
    public AudioSource doorsource;
    public AudioClip open, close;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool TryingToBoard(out Transform mySterringWheel)
    {
        playerMove.transform.parent = carSeat;
        playerMove.transform.localPosition = Vector3.zero;
        playerMove.transform.localRotation = Quaternion.identity;
        // playerMove.gameObject.SetActive(false);
        StartCoroutine("CloseDoor");
        carControl.onBoard = true;
        playerMove.wantToBoard = null;
        mySterringWheel = sterringWheel;
        return true;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            StartCoroutine("OpenDoor");
            playerMove = other.gameObject.GetComponent<PlayerMove>();
            playerMove.wantToBoard = TryingToBoard;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (playerMove)
        {
            StartCoroutine("CloseDoor");
            playerMove.wantToBoard = null;
            playerMove = null;
        }
    }

    IEnumerator OpenDoor()
    {
        float time = 0;
        doorsource.PlayOneShot(open);
        while (time <= 1)
        {
            yield return new WaitForFixedUpdate();

            time += Time.fixedDeltaTime*2;
            door.transform.localRotation = Quaternion.Lerp(Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 60, 0),time);
        }
    }

    IEnumerator CloseDoor()
    {
        float ang = 60;
        
        while (ang >= 0)
        {
            yield return new WaitForFixedUpdate();

            ang -= Time.fixedDeltaTime*200;
            door.transform.localRotation = Quaternion.Euler(0, ang, 0);
        }
        doorsource.PlayOneShot(close);
        door.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
}
