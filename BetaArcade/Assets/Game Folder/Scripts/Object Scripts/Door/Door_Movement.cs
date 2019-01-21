using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Movement : MonoBehaviour {

    public bool isActive;
    private bool isClosed = true;

    private float speed = 8.5f;

    private bool hasStarted = true;
    private Vector3 velocity;

    private GameObject doorObject;
    private GameObject lightObject;
    
    private Rigidbody rb;
    private Active_Receiver ar;
    private Player_Detection pd;

    private Vector3 closedPos;
    private Vector3 openPos;

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

        doorObject = transform.Find("Door").gameObject;
        lightObject = transform.Find("Light").gameObject;

        rb = doorObject.GetComponent<Rigidbody>();
        ar = GetComponent<Active_Receiver>();
        pd = GetComponent<Player_Detection>();

        closedPos = doorObject.transform.position;
        openPos = doorObject.transform.position + new Vector3(0f, 0f, 1.5f);

        
    }

    // Update is called once per frame
    void FixedUpdate () {

        if (!isActive && ar.isActive && doorType == DoorType.RequiresActivation)
        {
            isActive = true;
        }
        else if (!isActive && pd.playerDetected && doorType == DoorType.RequiresPlayer)
        {
            isActive = true;
        }
        else if (!isActive && pd.playerDetected && ar.isActive && doorType == DoorType.RequiresActivationAndPlayer)
        {
            isActive = true;
        }

        if (isActive && !ar.isActive && doorType == DoorType.RequiresActivation)
        {
            isActive = false;
        }
        else if (isActive && !pd.playerDetected && doorType == DoorType.RequiresPlayer)
        {
            isActive = false;
        }
        else if (isActive && doorType == DoorType.RequiresActivationAndPlayer && (!pd.playerDetected || !ar.isActive))
        {
            isActive = false;
        }

        float distanceToGo = speed * Time.deltaTime;
        //If it's not open and should, do so.
        if(isActive && isClosed)
        {
            while (distanceToGo > 0)
            {
                Vector3 direction = openPos - doorObject.transform.position;

                float dist = distanceToGo;

                if (direction.sqrMagnitude < dist * dist)
                {
                    isClosed = false;
                    break;
                }

                velocity = direction.normalized * dist;

                doorObject.transform.position += direction.normalized * dist;
                //rb.MovePosition(rb.position + velocity);

                distanceToGo -= dist;
            }
        }

        //If it's open and shouldn't be, close.
        if(!isActive && !isClosed)
        {
            while (distanceToGo > 0)
            {
                Vector3 direction = closedPos - doorObject.transform.position;

                float dist = distanceToGo;

                if (direction.sqrMagnitude < dist * dist)
                {
                    isClosed = true;
                    break;
                }

                velocity = direction.normalized * dist;

                doorObject.transform.position += direction.normalized * dist;
                //rb.MovePosition(rb.position + velocity);

                distanceToGo -= dist;
            }
        }

        if(isActive && !isClosed)
        {
            lightObject.GetComponent<Light_Switch>().isOn = true;
        }
        else
        {
            lightObject.GetComponent<Light_Switch>().isOn = false;
        }

    }
}
