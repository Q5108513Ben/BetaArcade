using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Active_Receiver : MonoBehaviour {

    public GameObject[] requiredObjects;

    public bool isActive = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        foreach(GameObject obj in requiredObjects)
        {
            if(obj.GetComponent<Active_Sender>().isActive == false)
            {
                isActive = false;
                break;
            }
            else
            {
                isActive = true;
            }
        }
	}
}
