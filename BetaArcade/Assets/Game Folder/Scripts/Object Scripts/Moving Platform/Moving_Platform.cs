using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_Platform : MonoBehaviour {

    public float speed = 1.0f;

    private bool hasStarted = true;
    private Vector3 velocity;

    public enum MovementType
    {
        Back_and_Forth,
        Loop,
        Once
    }

    public MovementType movementType;

    public Vector3[] wayPoints = new Vector3[1];

    public float[] waitTimes = new float[1];

    private int currentPoint = 0;
    private int nextPoint = 0;
    private int direction = 1;

    private float waitTime = -1.0f;

    private Rigidbody rb;

    // Use this for initialization
    void Start () {
        
        rb = GetComponent<Rigidbody>();
        currentPoint = 0;
        direction = 1;
        nextPoint = wayPoints.Length > 1 ? 1 : 0;

        waitTime = waitTimes[0];
        
    }

    // Update is called once per frame
    void FixedUpdate () {

        /*
        if (!isActive &&
            GetComponentInParent<Active_Receiver>().isActive &&
            doorType == DoorType.RequiresActivation)
        {
            isActive = true;
        }
        else if (!isActive &&
            GetComponentInParent<Player_Detection>().playerDetected &&
            doorType == DoorType.RequiresPlayer)
        {
            isActive = true;
        }
        else if (!isActive &&
            GetComponentInParent<Player_Detection>().playerDetected &&
            GetComponentInParent<Active_Receiver>().isActive &&
            doorType == DoorType.RequiresActivationAndPlayer)
        {
            isActive = true;
        }

        if ((isActive &&
            isToggle &&
            !GetComponentInParent<Active_Receiver>().isActive) &&
            doorType == DoorType.RequiresActivation)
        {
            isActive = false;
        }
        else if (isActive &&
            !GetComponentInParent<Player_Detection>().playerDetected &&
            doorType == DoorType.RequiresPlayer)
        {
            isActive = false;
        }
        else if (isActive &&
            doorType == DoorType.RequiresActivationAndPlayer &&
            (!GetComponentInParent<Player_Detection>().playerDetected || !GetComponentInParent<Active_Receiver>().isActive))
        {
            isActive = false;
        }
        */

        

        if (!hasStarted)
            return;

        //no need to update we have a single node in the path
        if (currentPoint == nextPoint)
            return;

        if (waitTime > 0)
        {
            waitTime -= Time.deltaTime;
            return;
        }

        float distanceToGo = speed * Time.deltaTime;

        while (distanceToGo > 0)
        {

            Vector3 direction = wayPoints[nextPoint] - transform.position;

            float dist = distanceToGo;
            if (direction.sqrMagnitude < dist * dist)
            {  
                dist = direction.magnitude;

                currentPoint = nextPoint;

                waitTime = waitTimes[currentPoint];

                if (this.direction > 0)
                {
                    nextPoint += 1;
                    if (nextPoint >= wayPoints.Length)
                    { 

                        switch (movementType)
                        {
                            case MovementType.Back_and_Forth:
                                nextPoint = wayPoints.Length - 2;
                                this.direction = -1;
                                break;
                            case MovementType.Loop:
                                nextPoint = 0;
                                break;
                            case MovementType.Once:
                                nextPoint -= 1;
                                hasStarted = false;
                                break;
                        }
                    }
                }
                else
                {
                    nextPoint -= 1;
                    if (nextPoint < 0)
                    { //reached the beginning again

                        switch (movementType)
                        {
                            case MovementType.Back_and_Forth:
                                nextPoint = 1;
                                this.direction = 1;
                                break;
                            case MovementType.Loop:
                                nextPoint = wayPoints.Length - 1;
                                break;
                            case MovementType.Once:
                                nextPoint += 1;
                                hasStarted = false;
                                break;
                        }
                    }
                }
            }

            velocity = direction.normalized * dist;

            //transform.position +=  direction.normalized * dist;
            rb.MovePosition(rb.position + velocity);
            //We remove the distance we moved. That way if we didn't had enough distance to the next goal, we will do a new loop to finish
            //the remaining distance we have to cover this frame toward the new goal
            distanceToGo -= dist;

            // we have some wait time set, that mean we reach a point where we have to wait. So no need to continue to move the platform, early exit.
            if (waitTime > 0.001f)
                break;
        }
    }
}
