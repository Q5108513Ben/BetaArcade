using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Detection : MonoBehaviour {

    public bool playerDetected = false;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Core"))
        {
            playerDetected = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Core"))
        {
            playerDetected = false;
        }
    }


}
