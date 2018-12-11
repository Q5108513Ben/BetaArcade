using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_Platform : MonoBehaviour {

    public float speed = 1.0f;

    private bool m_Started = false;
    private Vector3 m_Velocity;

    public enum MovementType
    {
        Back_and_Forth,
        Loop,
        Once
    }

    public MovementType movementType;

    public Vector3[] wayPoints = new Vector3[1];

    private Vector3[] worldWayPoints;

    public float[] waitTimes = new float[1];

    private int m_Current = 0;
    private int m_Next = 0;
    private int m_Dir = 1;

    private float m_WaitTime = -1.0f;

    private Rigidbody rb;

    // Use this for initialization
    void Start () {
        
        rb = GetComponent<Rigidbody>();
        m_Current = 0;
        m_Dir = 1;
        m_Next = wayPoints.Length > 1 ? 1 : 0;

        m_WaitTime = waitTimes[0];
        
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

        

        if (!m_Started)
            return;

        //no need to update we have a single node in the path
        if (m_Current == m_Next)
            return;

        if (m_WaitTime > 0)
        {
            m_WaitTime -= Time.deltaTime;
            return;
        }

        float distanceToGo = speed * Time.deltaTime;

        while (distanceToGo > 0)
        {

            Vector3 direction = wayPoints[m_Next] - transform.position;

            float dist = distanceToGo;
            if (direction.sqrMagnitude < dist * dist)
            {  
                dist = direction.magnitude;

                m_Current = m_Next;

                m_WaitTime = waitTimes[m_Current];

                if (m_Dir > 0)
                {
                    m_Next += 1;
                    if (m_Next >= wayPoints.Length)
                    { 

                        switch (movementType)
                        {
                            case MovementType.Back_and_Forth:
                                m_Next = wayPoints.Length - 2;
                                m_Dir = -1;
                                break;
                            case MovementType.Loop:
                                m_Next = 0;
                                break;
                            case MovementType.Once:
                                m_Next -= 1;
                                m_Started = false;
                                break;
                        }
                    }
                }
                else
                {
                    m_Next -= 1;
                    if (m_Next < 0)
                    { //reached the beginning again

                        switch (movementType)
                        {
                            case MovementType.Back_and_Forth:
                                m_Next = 1;
                                m_Dir = 1;
                                break;
                            case MovementType.Loop:
                                m_Next = wayPoints.Length - 1;
                                break;
                            case MovementType.Once:
                                m_Next += 1;
                                m_Started = false;
                                break;
                        }
                    }
                }
            }

            m_Velocity = direction.normalized * dist;

            //transform.position +=  direction.normalized * dist;
            rb.MovePosition(rb.position + m_Velocity);
            //We remove the distance we moved. That way if we didn't had enough distance to the next goal, we will do a new loop to finish
            //the remaining distance we have to cover this frame toward the new goal
            distanceToGo -= dist;

            // we have some wait time set, that mean we reach a point where we have to wait. So no need to continue to move the platform, early exit.
            if (m_WaitTime > 0.001f)
                break;
        }
    }
}
