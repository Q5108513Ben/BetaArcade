using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate_press : MonoBehaviour {

    public bool isToggle;
    public bool isActive;

    private bool hasLerped = false;

    private Vector3 startPosition;
    private Vector3 endPosition;

    private float startTime;
    private float journeyLength;
    private float speed = 1.0f;

    // Use this for initialization
    void Start () {
        startPosition = transform.position;
        endPosition = transform.position - new Vector3(0, 0.2f, 0);

        startTime = Time.time;

        journeyLength = Vector3.Distance(startPosition, endPosition);
    }
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKeyUp(KeyCode.I))
        {
            isActive = true;
        }

        

		//if player is on top of plate
        //press down and make "isActive" true
        //boolean of if it's press once or hold


        //Lerping once active
        if(isActive && !hasLerped)
        {

            float distCovered = (Time.time - startTime) * speed;

            float fracJourney = distCovered / journeyLength;

            transform.position = Vector3.Lerp(startPosition, endPosition, fracJourney);

            if(transform.position == endPosition)
                hasLerped = true;
        }


        

	}
}
