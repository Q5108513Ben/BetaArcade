using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot_Detection : MonoBehaviour {

    private List<GameObject> botCounter = new List<GameObject>();

    public int numBots = 0;

	// Use this for initialization
	void Start () {
        botCounter.Clear();
	}
	
	// Update is called once per frame
	void Update () {
        numBots = botCounter.Count;
	}

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Bot"))
        {
            botCounter.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Bot"))
        {
            botCounter.Remove(collision.gameObject);
        }
    }

}
