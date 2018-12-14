using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreAttraction : MonoBehaviour {

    #region VARIABLES

    [Header("Game Objects")]

    [Tooltip("The player model - used for movement.")]
    [SerializeField]
    private GameObject player;

    [Tooltip("The list of bots used by the player.")]
    public List<GameObject> usedBots;

    [Tooltip("The list of bots unused by the player, outside of the players sphere of influence.")]
    public  List<GameObject> unusuedBots;
    
    [Space(10)]

    [Header("Bot attraction values")]

    [Tooltip("The minimum radius from the core the bots can attract from.")]
    [SerializeField]
    private float minRad = 0.1f;

    [Tooltip("The maximum radius from the core the bots can attract from.")]
    [SerializeField]
    private float maxRad = 5;

    [Tooltip("The absolute maximum radius from the core the bots can attract from.")]
    [SerializeField]
    private float absoluteMaxRad = 5;

    [Tooltip("The amount of force used to attract the bot to the core.")]
    [SerializeField]
    private float forceMultiplier = 100;

    [Tooltip("The amount of reduction of attraction per fixed update while reducing attraction.")]
    [SerializeField]
    private float attractionDeductionRate = 0.1f;

    [Space(10)]

    [Header("Solid form values")]

    [Tooltip("Whether the player is in their solid form or not.")]
    [SerializeField]
    private bool isSolid = false;

    [Tooltip("The outer range of the solid state.")]
    [SerializeField]
    private float outerSolidRange = 1.25f;

    [Space(10)]

    [Header("Core to Player attraction values")]
    [Tooltip("The amount of force from the core towards the player controller.")]
    [SerializeField]
    private float forceCore = 10000f;

    [Tooltip("The maximum distance allowed between the core and the player controller when not attracting the core.")]
    [SerializeField]
    private float maxPlayerDist = 0.1f;

    [Tooltip("The current distance between the core and the player controller.")]
    [SerializeField]
    private float currPlayerDist;

    [Tooltip("The rate at which the core moves towards the player's position.")]
    [SerializeField]
    private float coreMoveRate = 0.1f;


    #endregion

    // Use this for initialization
    void Start () {
        usedBots = new List<GameObject>();
        unusuedBots = new List<GameObject>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        #region BOT_CHECK_&_ATTRACTION

        if(usedBots.Count > 0)
            usedBots.Clear();

        if(unusuedBots.Count > 0)
            unusuedBots.Clear();

        foreach(GameObject bot in GameObject.FindGameObjectsWithTag("Bot"))
        {
            if (!isSolid)
            {
                var currentBotPosition = bot.transform.position;
                var corePosition = this.transform.position;
                //var corePositionUnder = this.transform.position - new Vector3(0, 0.25f, 0);
                float dist = Vector3.Distance(currentBotPosition, corePosition);

                if (dist > maxRad)
                {
                    //If beyond core attraction, move bot to "unowned"
                    //Similar check should be done with those that are beyond the area of attraction
                    unusuedBots.Add(bot);
                    continue;
                }



                usedBots.Add(bot);

                if (dist <= maxRad && dist > minRad)
                {
                    Vector3 dir = (corePosition - currentBotPosition).normalized;
                    //float force = forceMultiplier * smoothStep(minRad, maxRad, dist);
                    float force = forceMultiplier;
                    float distLinear = (dist / maxRad);

                    bot.GetComponent<Rigidbody>().AddForce(dir * force * Time.fixedDeltaTime);
                    // Multiply by distLinear in order to add distance attenuation on the force strength.
                }

            }
            else
            {

                var currentBotPosition = bot.transform.position;
                var corePosition = this.transform.position;
                //var corePositionUnder = this.transform.position - new Vector3(0, 0.25f, 0);
                float dist = Vector3.Distance(currentBotPosition, corePosition);

                if(dist > maxRad)
                {
                    unusuedBots.Add(bot);
                    continue;
                }


                usedBots.Add(bot);

                if (dist > outerSolidRange)
                {
                    
                    Vector3 dir = (corePosition - currentBotPosition).normalized;
                    //float force = forceMultiplier * smoothStep(minRad, maxRad, dist);
                    float force = forceMultiplier;

                    if (bot.GetComponent<Rigidbody>().velocity.magnitude < 10)
                        bot.GetComponent<Rigidbody>().AddForce(dir * force * Time.fixedDeltaTime);
                    else
                        bot.GetComponent<Rigidbody>().velocity /= 2f;
                }
                //else if (dist < innerSolid)
                //{
                //    Vector3 dir = -(corePosition - currentBotPosition).normalized;
                //    //float force = forceMultiplier * smoothStep(minRad, maxRad, dist);
                //    float force = forceMultiplier;

                //    //if(currBot.GetComponent<Rigidbody>().velocity.magnitude > )
                //    if(currBot.GetComponent<Rigidbody>().velocity.magnitude < 10)
                //        currBot.GetComponent<Rigidbody>().AddForce(dir * force * Time.fixedDeltaTime);
                //    else
                //        currBot.GetComponent<Rigidbody>().velocity /= 2f;
                //}
                //else
                //{
                //    //Do nothing
                //    //currBot.transform.position = currBot.transform.position;
                //}
            }

        }

        #endregion


        #region KEY_PRESS_CHECKS

        if (Input.GetKey(KeyCode.LeftShift))
        {
            //isAttracting = !isAttracting;
            if (maxRad > attractionDeductionRate)
                maxRad -= attractionDeductionRate;
            else
                maxRad = 0;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
            maxRad = absoluteMaxRad;



        if (Input.GetKeyUp(KeyCode.E))
        {
            isSolid = !isSolid;

            Debug.Log("Player Radius before: " + player.GetComponent<SphereCollider>().radius);
            Debug.Log("Core Radius before: " + this.GetComponent<SphereCollider>().radius);

            Debug.Log("Number of bots: " + usedBots.Count);

            foreach (GameObject currBot in usedBots)
            {
                currBot.GetComponent<Rigidbody>().useGravity = !currBot.GetComponent<Rigidbody>().useGravity;
            }

            //Sphere radius should be in relation to number of bots
            //BALL SANDWICH
            if (isSolid)
            {
                float tempPRad = 0.5f;
                float tempCRad = 0.5f;

                int numBots = usedBots.Count;

                if(numBots >= 100)
                {
                    tempPRad = 3f;
                    tempCRad = 2.8f;
                }
                else if(numBots == 0)
                {
                    tempPRad = 0.5f;
                    tempCRad = 0.5f;
                }
                else
                {
                    Debug.Log("This should be hit!");
                    tempPRad = 2.5f * (numBots / 100) + 0.7f;
                    tempCRad = 2.5f * (numBots / 100) + 0.5f;
                    Debug.Log("TempPRad: " + tempPRad);
                    Debug.Log("TempCRad: " + tempCRad);
                }




                player.GetComponent<SphereCollider>().radius = tempPRad;
                this.GetComponent<SphereCollider>().radius = tempCRad;
            }
            else
            {
                player.GetComponent<SphereCollider>().radius = 0.5f;
                this.GetComponent<SphereCollider>().radius = 0.5f;
            }

            Debug.Log("Player Radius after: " + player.GetComponent<SphereCollider>().radius);
            Debug.Log("Core Radius after: " + this.GetComponent<SphereCollider>().radius);

        }


        #endregion


        #region CORE_ATTRACTION_TO_PLAYER

        var corePos = this.transform.position;
        var playerPosition = player.transform.position;

        currPlayerDist = Vector3.Distance(playerPosition, corePos);

        if(currPlayerDist > maxPlayerDist)
        {
            //Vector3 direction = (playerPosition - corePos).normalized;


            //float currForce = forceCore;// * (smoothStep(playerDist, 10, distance)) + (forceCore * 0.1f);
            //if (this.GetComponent<Rigidbody>().velocity.magnitude < 15)
            //    this.GetComponent<Rigidbody>().AddForce(direction * currForce * Time.fixedDeltaTime);
            //else
            //    this.GetComponent<Rigidbody>().velocity /= 2f;


            //this.transform.position = Vector3.MoveTowards(corePos, playerPosition, coreMoveRate);
            this.transform.position = Vector3.Lerp(corePos, playerPosition, Time.time);
        }
        else
        {
            //Vector3 direction = (playerPosition - corePos).normalized;
            //float currForce = forceCore;// * (smoothStep(playerDist, 100, distance));
            //this.GetComponent<Rigidbody>().velocity = this.GetComponent<Rigidbody>().velocity * smoothStep(0, maxPlayerDist, currPlayerDist);
            this.transform.position = playerPosition;
        }

        #endregion

    }

    #region PHYSICS_FUNCTIONS

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

    #endregion

}
