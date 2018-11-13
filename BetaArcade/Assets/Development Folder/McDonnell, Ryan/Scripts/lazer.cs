using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lazer : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public GameObject source;
    public GameObject target;
    public float maxDistance = 100;
    

    // Use this for initialization
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
       // lineRenderer.useWorldSpace = true;
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 raysource = (source.transform.position - transform.position); 
        Vector3 raydirection = (target.transform.position - transform.position);
        Vector3 endPoint;


        // if (Input.GetKey(KeyCode.R))
        // {


        Ray ray = new Ray(raysource, raydirection);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, maxDistance))

            endPoint = hit.point;

            Debug.DrawRay(source.transform.position, raydirection - raysource, Color.cyan);
             lineRenderer.SetPosition(0, raysource);
            lineRenderer.SetPosition(1, raydirection);

       // }
    }
}
