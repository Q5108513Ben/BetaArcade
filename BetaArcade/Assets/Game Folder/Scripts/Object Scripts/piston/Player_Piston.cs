﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Piston : MonoBehaviour
{

    public Canvas deathMenuPopUp; //the deth menu will be placed into this slot so when the player dies it will appear.
    public AudioSource deathSound;//this is the sound that the player makes when they die.
    private List<string> collisions = new List<string>();

    void Start()
    {

        deathMenuPopUp.enabled = false;
    }




    void OnTriggerEnter(Collider col)
    {

        collisions.Add(col.gameObject.name);

        if (collisions.Contains("Crush_Detector") && collisions.Contains("Floor"))
        {
            //  Destroy(this.gameObject);//destroys the player object.
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
            deathSound.Play();


            deathMenuPopUp.enabled = true;

            //if the player is useing health the damage would be put here.      
        }
    }

    public void OnTriggerExit(Collider col)
    {
        collisions.Remove(col.gameObject.name);
    }
}
