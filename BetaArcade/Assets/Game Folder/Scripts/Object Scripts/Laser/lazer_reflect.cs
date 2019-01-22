using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lazer_reflect : MonoBehaviour
{

    public LineRenderer line;
    public LineRenderer line2;

    Vector3 lasersLastHitPoint;

    public int maxReflecionCount = 1;
    public float maxDistance = 100;
    public bool isUseingAButton = false;
    private bool isON = false;
    private bool LaserIsReflecting;
    private bool isWaiting = false;
    private bool isWaiting2 = false;
    private float WaitingTime = 0.3f;
    private float WaitingTime2 = 0.3f;

    void Start()
    {
        if (maxReflecionCount > 1)
            {
            maxReflecionCount = 1;
            }

    }
    

    



    void OnDrawGizmos()
    {
        DrawLazerReflection(this.transform.position + this.transform.forward, this.transform.forward, maxReflecionCount);

    }

    private void DrawLazerReflection(Vector3 lazerPosition, Vector3 direction, int reflectionsRemaining)
    {
        if (isUseingAButton == false)
        {
            isON = true;
        }

        if (isUseingAButton == true)
        {
            if (!isON && GetComponentInParent<Active_Receiver>().isActive)
            {
                isON = true;
            }

            if ((isON && !GetComponentInParent<Active_Receiver>().isActive))
            {
                isON = false;

            }
        }

        if (isON == true)
        {
            if (reflectionsRemaining == 0)
            {
                return;
            }

            Vector3 startingPosition = lazerPosition;


            //raycast to detect reflection
            Ray ray = new Ray(lazerPosition, direction);
            RaycastHit hit;



            if (Physics.Raycast(ray, out hit, maxDistance))
            {


                if (hit.transform.gameObject.tag == "Bot")
                {
                    if (isWaiting == false)
                    {
                        Destroy(hit.transform.gameObject);
                        StartCoroutine(killBot());
                    }
                }
                if (hit.transform.gameObject.tag == "Core")
                {
                    GameObject.Find("Core").SetActive(false);
                }

                if (hit.transform.gameObject.tag == "reflect")
                {

                    
                        LaserIsReflecting = true;
                        direction = Vector3.Reflect(direction, hit.normal);
                        lazerPosition = hit.point; // updates the hit point for mutiple reflections   
                        lasersLastHitPoint = lazerPosition;
                        line2.enabled = true;

                    
                }
                else if (hit.transform.gameObject.tag != "reflect")
                {
                  
                    
                        LaserIsReflecting = true;
                        lazerPosition = hit.point;
                        lasersLastHitPoint = lazerPosition;
                        line2.enabled = false;
 
                    
                }
                else
                {
                    LaserIsReflecting = false;

                }
            }
            else
            {
                if (LaserIsReflecting == false)
                {
                    lazerPosition += direction * maxDistance;
                    lasersLastHitPoint = lazerPosition;
                    line2.enabled = false;
                }
            }


            Gizmos.color = Color.red;
            Gizmos.DrawLine(startingPosition, lazerPosition);
            DrawLazerReflection(lazerPosition, direction, reflectionsRemaining - 1);

            //for drawing the lines in the game display
            line.SetPosition(0, startingPosition);
            line.SetPosition(1, lasersLastHitPoint);

            line2.SetPosition(0, lasersLastHitPoint);
            line2.SetPosition(1, lazerPosition + (direction * maxDistance) );

          

        }

    }
    IEnumerator killBot()
    {
         yield return new WaitForSeconds(WaitingTime);
         isWaiting = false;
        
    }

}

    

