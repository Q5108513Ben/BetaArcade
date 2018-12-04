using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_Switch : MonoBehaviour {

    Material m_Material;

    public bool isOn;

	// Use this for initialization
	void Start () {
        m_Material = GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
        if (isOn)
            m_Material.color = Color.green;
        else
            m_Material.color = Color.red;


	}
}
