using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piston : MonoBehaviour
{

    // activation variables
    public float maxExtentionRange = 0;
    public bool pistonIsOn = false;
    public bool isUseingAButton = false;
    private bool pistonIsExtending = true;
    private bool pistonIsRetracting = false;
    //  private bool pistonHasReachedMaxRange = false;
    // private bool pistonHasFullyRetracted = false;

    //rigidbody's
    public GameObject pistonHead;
    public GameObject pistonBody;

    // waiting time variables
    public float isFullyExtendedWait = 0;
    public float isFullyRetractedWait = 0;

    // speed variables
    public float pistonExtendingSpeed = 0;
    public float pistonRetractingSpeed = 0;



    //test
    private float maxRange;
    private float minRange;

    // Use this for initialization
    void Start()
    {
        maxRange = pistonHead.transform.position.x + maxExtentionRange;
        minRange = pistonHead.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {


        if (pistonIsOn)
        {
            if (pistonIsExtending == true)
            {
                //extends the piston head
                pistonHead.transform.Translate(Vector3.right * pistonExtendingSpeed * Time.deltaTime);
                // pistonBody.transform.localScale = new Vector3(transform.localScale.x  * Time.deltaTime, 0);
                pistonBody.transform.localScale += new Vector3(0, maxRange / maxRange / 1.4f, 0) * pistonExtendingSpeed * Time.deltaTime;
                pistonBody.transform.Translate(Vector3.up * pistonExtendingSpeed / 2 * Time.deltaTime);
            }
            else if (pistonIsRetracting == true)
            {
                //retracts the piston head
                pistonHead.transform.Translate(Vector3.left * pistonRetractingSpeed * Time.deltaTime);
                //pistonBody.transform.localScale = new Vector3(-transform.localScale.x * pistonRetractingSpeed * Time.deltaTime, 0);
                pistonBody.transform.localScale += new Vector3(0, -maxRange / maxRange / 1.4f, 0) * pistonRetractingSpeed * Time.deltaTime;
                pistonBody.transform.Translate(Vector3.down * pistonRetractingSpeed / 2 * Time.deltaTime);
            }

            // for checking if the poston has reached full range and if so start wait coroutine
            if (pistonHead.transform.position.x >= maxRange)
            {
                StartCoroutine(waitFullyExtended());
            }

            //for checking if the piston has fully retracted and if so start wait coroutine
            if (pistonHead.transform.position.x <= minRange)
            {
                StartCoroutine(waitFullyRetracted());
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Floor")
        {
            Destroy(col.gameObject);
            StopAllCoroutines();

            StartCoroutine(waitFullyExtended());
        }
    }
  
    IEnumerator waitFullyExtended()
    {
        pistonIsOn = false;
        yield return new WaitForSeconds(isFullyExtendedWait);
        pistonIsExtending = false;
        pistonIsRetracting = true;
        pistonIsOn = true;

    }

    IEnumerator waitFullyRetracted()
    {
        pistonIsOn = false;
        yield return new WaitForSeconds(isFullyRetractedWait);
        pistonIsRetracting = false;
        pistonIsExtending = true;
        pistonIsOn = true;
    }
}


