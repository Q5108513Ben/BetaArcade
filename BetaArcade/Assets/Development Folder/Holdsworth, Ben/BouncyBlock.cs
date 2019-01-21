using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyBlock : MonoBehaviour {

    public Rigidbody player;
    public float launchHeight = 12;

    private void OnCollisionEnter(Collision collision) {
        
        if (collision.gameObject.CompareTag("Player")) {

            player.velocity = Vector3.up * launchHeight;

        }

    }

}
