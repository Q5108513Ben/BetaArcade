using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate_press : MonoBehaviour {

    public bool isToggle;
    public bool isActive;

    private bool hasLerped = false;

    public GameObject startPoint;
    public GameObject endPoint;

    private Vector3 startPosition;
    private Vector3 endPosition;

    private float startTime;
    private float journeyLength;


    private float speed = 1.0f;

    private bool hasTimeStarted = false;

    // Use this for initialization
    void Start () {
        startPosition = startPoint.transform.position;
        endPosition = endPoint.transform.position;
        journeyLength = Vector3.Distance(startPosition, endPosition);
    }
	
	// Update is called once per frame
	void Update () {

        //if player is on top of plate
        if(GetComponentInChildren<Player_Detection>().playerDetected && !isActive)
        {
            isActive = true;
        }

        if(!GetComponentInChildren<Player_Detection>().playerDetected && isActive && isToggle)
        {
            isActive = false;
        }
        
        //Lerping once active
        if(isActive && !hasLerped)
        {
            if(!hasTimeStarted)
            {
                startTime = Time.time;
                hasTimeStarted = true;
            }

            float distCovered = (Time.time - startTime) * speed;

            float fracJourney = distCovered / journeyLength;

            transform.position = Vector3.Lerp(startPosition, endPosition, fracJourney);

            if (transform.position == endPosition)
            {
                hasLerped = true;
                hasTimeStarted = false;
            }
        }

        if(!isActive && hasLerped && isToggle)
        {
            if (!hasTimeStarted)
            {
                startTime = Time.time;
                hasTimeStarted = true;
            }

            float distCovered = (Time.time - startTime) * speed;

            float fracJourney = distCovered / journeyLength;

            transform.position = Vector3.Lerp(endPosition, startPosition, fracJourney);

            if (transform.position == startPosition)
            {
                hasLerped = false;
                hasTimeStarted = false;
            }
        }

        if (isActive && hasLerped)
        {
            GetComponent<Active_Sender>().isActive = true;
            this.transform.parent.Find("Light").gameObject.GetComponent<Light_Switch>().isOn = true;
        }
        else
        {
            GetComponent<Active_Sender>().isActive = false;
            this.transform.parent.Find("Light").gameObject.GetComponent<Light_Switch>().isOn = false;
        }
	}
}
