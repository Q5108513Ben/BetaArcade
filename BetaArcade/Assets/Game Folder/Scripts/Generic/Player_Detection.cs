using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Detection : MonoBehaviour {

    public bool playerDetected = false;

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Core"))
        {
            playerDetected = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Core"))
        {
            playerDetected = false;
        }
    }


}
