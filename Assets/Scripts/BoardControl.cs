using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardControl : MonoBehaviour
{
    public GameObject door;
    PlayerMove playerMove;
    public CarControl carControl;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TryingToBoard()
    {
        playerMove.transform.parent = transform;
        playerMove.transform.localPosition = Vector3.zero;
        playerMove.gameObject.SetActive(false);
        door.transform.rotation = Quaternion.Euler(0, 0, 0);
        carControl.onBoard = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            door.transform.rotation = Quaternion.Euler(0, 60, 0);
            playerMove = other.gameObject.GetComponent<PlayerMove>();
            playerMove.wantToBoard = TryingToBoard;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (playerMove)
        {
            door.transform.rotation = Quaternion.Euler(0, 0, 0);
            playerMove.wantToBoard = null;
            playerMove = null;
        }
    }
}
