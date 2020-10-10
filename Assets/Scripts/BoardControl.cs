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
    bool waitingdoor = false;
  
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
        transform.parent = null;
        DontDestroyOnLoad(gameObject);
        playerMove.transform.parent = carSeat;
        playerMove.transform.localPosition = Vector3.zero;
        playerMove.transform.localRotation = Quaternion.identity;
        // playerMove.gameObject.SetActive(false);
        StartCoroutine("CloseDoor");
        CallToBoard(true);
        playerMove.wantToBoard = null;
        mySterringWheel = sterringWheel;
        playerMove.wantToOffBoard = TryingToOffBoard;
        playerMove.wantToBoard = null;
        return true;
    }

    public bool TryingToOffBoard(out Transform mySterringWheel)
    {
        playerMove.transform.parent = null;
        playerMove.transform.position = transform.position+transform.right*-2;
        playerMove.transform.localRotation = Quaternion.identity;
        StartCoroutine("OpenDoor");
        CallToBoard(false);
        playerMove.wantToBoard = null;
        mySterringWheel = null;
        return true;
    }

    protected virtual void CallToBoard(bool onboard)
    {
        carControl.onBoard = onboard;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            if (waitingdoor) return;
            StartCoroutine("OpenDoor");
            playerMove = other.gameObject.GetComponent<PlayerMove>();
            playerMove.wantToBoard = TryingToBoard;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && playerMove)
        {
            if (waitingdoor) return;
            StartCoroutine("CloseDoor");
            playerMove = null;
        }
    }

    IEnumerator OpenDoor()
    {

        waitingdoor = true;

        float ang = door.transform.localRotation.eulerAngles.y;

        while (ang <= 60)
        {
            yield return new WaitForFixedUpdate();

            ang += Time.fixedDeltaTime * 200;
            door.transform.localRotation = Quaternion.Euler(door.transform.localRotation.eulerAngles.x, ang, door.transform.localRotation.eulerAngles.z);
        }
        doorsource.PlayOneShot(open);
       
        waitingdoor = false;

    }

    IEnumerator CloseDoor()
    {
       
        waitingdoor = true;
       
        float ang = door.transform.localRotation.eulerAngles.y;
       
        while (ang >= 0)
        {
            yield return new WaitForFixedUpdate();

            ang -= Time.fixedDeltaTime*200;
            door.transform.localRotation = Quaternion.Euler(door.transform.localRotation.eulerAngles.x, ang, door.transform.localRotation.eulerAngles.z);
        }
        doorsource.PlayOneShot(close);
        door.transform.localRotation = Quaternion.Euler(door.transform.localRotation.eulerAngles.x, 0, door.transform.localRotation.eulerAngles.z);
        waitingdoor = false;
       
    }
}
