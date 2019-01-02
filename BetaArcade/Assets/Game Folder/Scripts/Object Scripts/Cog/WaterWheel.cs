using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterWheel : MonoBehaviour {



    public bool TurnLeft = false;
    public bool TurnRight = false;
    public bool isTurning = true;
    public float runingTime = 0;
    public float WaitingTime = 0;

    private bool isWaiting = false;
    private bool isRunning = false;

    public bool isUseingAButton = false;
    [Tooltip("Dont use negative numbers for the speed, the turn right and left will take care of it for you, you can use decimal numbers")]
    public float speed = 0;

    // Use this for initialization
    void Start()
    {

        StartCoroutine(StopStart());
    }

    // Update is called once per frame
    void Update()
    {

        if (isTurning == true)
        {
            isRunning = true;
        }


        StartCoroutine(StopStart());


        if (isUseingAButton == true)
        {
            if (!isTurning && GetComponentInParent<Active_Receiver>().isActive)
            {
                isTurning = true;
            }

            if ((isTurning && !GetComponentInParent<Active_Receiver>().isActive))
            {
                isTurning = false;
            }
        }

        if (TurnLeft == true)
        {
            if (isTurning == true)
            {
                transform.Rotate(new Vector3(0, 0, speed), Space.Self);
            }

        }

        if (TurnRight == true)
        {
            if (isTurning == true)
            {
                transform.Rotate(new Vector3(0, 0, -speed), Space.Self);
            }

        }

        if (TurnLeft == true && TurnRight == true)
        {
            TurnLeft = false;
            TurnRight = false;



        }
    }

    IEnumerator StopStart()
    {
        //for the stoping and sarting time intivals
        if (isWaiting == true)
        {
            isTurning = false;

            yield return new WaitForSeconds(WaitingTime);

            isRunning = true;
            isWaiting = false;
    

        }
        else if (isRunning == true)
        {
            isTurning = true;

            yield return new WaitForSeconds(runingTime);

            isWaiting = true;
            isRunning = false;

        }

    }
}


