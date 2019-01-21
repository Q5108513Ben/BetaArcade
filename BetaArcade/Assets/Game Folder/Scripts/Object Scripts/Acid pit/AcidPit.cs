using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AcidPit : MonoBehaviour {

    public Canvas deathMenuPopUp; //the deth menu will be placed into this slot so when the player dies it will appear.
    public AudioSource deathSound;//this is the sound that the player makes when they die.
    void Start()
    {
        deathMenuPopUp.enabled = false;
    }

    private void Update()
    {
        
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Core")
        {

            Destroy(col.gameObject);//destroys the player object.
            deathSound.Play();

           

            deathMenuPopUp.enabled = true;


             //if the player is useing health the damage would be put here.          
        }

    }
}
