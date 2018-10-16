using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreAttraction : MonoBehaviour {

    [Tooltip("The empty game object storing all the current bots the player has.")]
    public GameObject collection;

    [Tooltip("The minimum radius from the core the bots can attract from.")]
    public float minRad = 0.01f;

    [Tooltip("The maximum radius from the core the bots can attract from.")]
    public float maxRad = 5;

    [Tooltip("The absolute maximum radius from the core the bots can attract from.")]
    public float absoluteMaxRad = 5;

    [Tooltip("The amount of force used to attract the bot to the core.")]
    public float forceMultiplier = 1000;

    [Tooltip("The amount of reduction of attraction per fixed update while reducing attraction.")]
    public float attractionDeductionRate = 0.01f;

    [Tooltip("Whether the player is in their solid form or not.")]
    public bool isSolid = false;

    [Tooltip("Whether the player is attracting the bots or not.")]
    public bool isAttracting = true;

    [Tooltip("The array of bots used by the player.")]
    public GameObject[] bots;

    private bool canJump = true;

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        //Temp until can add/delete bots from bot collection, then findchildren
        bots = GameObject.FindGameObjectsWithTag("Bot");

        if (Input.GetKey(KeyCode.Space))
        {
            //isAttracting = !isAttracting;
            if (maxRad > attractionDeductionRate)
                maxRad -= attractionDeductionRate;
            else
                maxRad = 0;
        }

        if (Input.GetKeyUp(KeyCode.Space))
            maxRad = absoluteMaxRad;



        if (Input.GetKeyUp(KeyCode.E))
        {
            isSolid = !isSolid;

            foreach (GameObject currBot in bots)
            {
                //currBot.GetComponent<SphereCollider>().material.
            }

            //Sphere radius should be in relation to number of bots
            //BALL SANDWICH
            if(isSolid)
                this.GetComponent<SphereCollider>().radius = 1;
            else
                this.GetComponent<SphereCollider>().radius = 0.5f;
        }

        
        
        if (!isSolid)
        {
            foreach (GameObject currBot in bots)
            {
                var currentBotPosition = currBot.transform.position;
                var corePosition = this.transform.position;
                //var corePositionUnder = this.transform.position - new Vector3(0, 0.25f, 0);
                float dist = Vector3.Distance(currentBotPosition, corePosition);

                if(dist > maxRad)
                {
                    //If beyond core attraction, move bot to "unowned"
                    //Similar check should be done with those that are beyond the area of attraction 
                }

                if (dist > minRad && dist < maxRad)
                {
                    Vector3 dir = (corePosition - currentBotPosition).normalized;
                    //float force = forceMultiplier * smoothStep(minRad, maxRad, dist);
                    float force = forceMultiplier;
                    currBot.GetComponent<Rigidbody>().AddForce(dir * force * Time.fixedDeltaTime);

                }
                //else if(dist < minRad)
                //{
                //    Vector3 dir = (corePositionUnder - currentBotPosition).normalized;
                //    float force = forceMultiplier;
                //    this.GetComponent<Rigidbody>().AddForce(-dir * force * Time.fixedDeltaTime);
                //}
            }
        }
        else
        {

            foreach (GameObject currBot in bots)
            {
                var currentBotPosition = currBot.transform.position;
                var corePosition = this.transform.position;
                //var corePositionUnder = this.transform.position - new Vector3(0, 0.25f, 0);
                float dist = Vector3.Distance(currentBotPosition, corePosition);

                if (dist > minRad && dist < maxRad)
                {
                    Vector3 dir = (corePosition - currentBotPosition).normalized;
                    //float force = forceMultiplier * smoothStep(minRad, maxRad, dist);
                    float force = forceMultiplier;
                    currBot.GetComponent<Rigidbody>().AddForce(dir * force * Time.fixedDeltaTime);
                }
                //else if (dist < minRad)
                //{
                //    Vector3 dir = (corePositionUnder - currentBotPosition).normalized;
                //    float force = forceMultiplier;
                //    this.GetComponent<Rigidbody>().AddForce(-dir * force * Time.fixedDeltaTime);
                //}
            }
        }
    }

    float clamp(float value, float start_value, float end_value)
    {
        if (value < start_value)
        {
            return start_value;
        }

        if (value > end_value)
        {
            return end_value;
        }

        return value;
    }

    float linearStep(float min, float max, float x)
    {
        return clamp((x - min) / (max - min), 0, 1);
    }

    float smoothStep(float min, float max, float x)
    {
        x = linearStep(min, max, x);
        return ((3 * x * x) - (2 * x * x * x));
    }

    bool getCanJump()
    {
        return canJump;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            canJump = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            canJump = true;
        }
    }

}
