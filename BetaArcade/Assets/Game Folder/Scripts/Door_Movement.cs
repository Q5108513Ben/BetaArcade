using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Movement : MonoBehaviour {

    public bool isToggle;
    public bool isActive;

    private bool hasLerped = false;

    public GameObject startPoint;
    public GameObject endPoint;

    private Vector3 startPosition;
    private Vector3 endPosition;
    private Vector3 currentPosition;

    private float startTime;
    private float journeyLength;

    public float speed = 1.0f;

    private bool hasTimeStarted = false;

    private Animator anim;
    
    private enum DoorType
    {
        RequiresActivation,
        RequiresPlayer,
        RequiresActivationAndPlayer
    }

    [SerializeField]
    private DoorType doorType = DoorType.RequiresPlayer;

    // Use this for initialization
    void Start () {
        startPosition = startPoint.transform.position;
        endPosition = endPoint.transform.position;
        journeyLength = Vector3.Distance(startPosition, endPosition);
        anim = this.GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update () {
		if(!isActive && 
            GetComponentInParent<Active_Receiver>().isActive && 
            doorType == DoorType.RequiresActivation)
        {
            isActive = true;
        }
        else if(!isActive && 
            GetComponentInParent<Player_Detection>().playerDetected && 
            doorType == DoorType.RequiresPlayer)
        {
            isActive = true;
        }
        else if(!isActive && 
            GetComponentInParent<Player_Detection>().playerDetected && 
            GetComponentInParent<Active_Receiver>().isActive && 
            doorType == DoorType.RequiresActivationAndPlayer)
        {
            isActive = true;
        }

        if((isActive && 
            isToggle && 
            !GetComponentInParent<Active_Receiver>().isActive) && 
            doorType == DoorType.RequiresActivation)
        {
            isActive = false;
        }
        else if(isActive && 
            !GetComponentInParent<Player_Detection>().playerDetected && 
            doorType == DoorType.RequiresPlayer)
        {
            isActive = false;
        }
        else if(isActive && 
            doorType == DoorType.RequiresActivationAndPlayer && 
            (!GetComponentInParent<Player_Detection>().playerDetected || !GetComponentInParent<Active_Receiver>().isActive))
        {
            isActive = false;
        }

        anim.SetBool("DoorOpen", isActive);

        /*
        //Lerping once active
        if (isActive && !hasLerped)
        {
            if (!hasTimeStarted)
            {
                startTime = Time.time;
                hasTimeStarted = true;
                //Debug.Log(startTime);
            }

            float distCovered = (Time.time - startTime) * speed;

            //float distCovered = Vector3.Distance(transform.position, startPosition);

            float fracJourney = distCovered / journeyLength;

            transform.position = Vector3.Lerp(startPosition, endPosition, fracJourney);

            //Debug.Log(transform.position);

            if (transform.position == endPosition)
            {
                hasLerped = true;
                hasTimeStarted = false;
            }
            else
            {
                currentPosition = transform.position;
            }
        }

        //if(!isActive && !hasLerped)
        //{
        //    float distCovered = Vector3.Distance(currentPosition, endPosition);
        //    float fracJourney = distCovered / journeyLength;
        //    transform.position = Vector3.Lerp(startPosition, endPosition, fracJourney);
        //    Debug.Log(transform.position);
        //    if (transform.position == endPosition)
        //    {
        //        hasLerped = true;
        //        hasTimeStarted = false;
        //    }
        //}


        if (!isActive && hasLerped && isToggle)
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
        */

    }
}
