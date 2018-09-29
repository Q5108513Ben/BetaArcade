using UnityEngine;

public class PlayerController : MonoBehaviour {
    
	void Update() {

        if (Input.GetButton("Move - Right")) {

            var xVelocity = 10.0f * Time.deltaTime;

            transform.Translate(xVelocity, 0, 0);

        }

        if (Input.GetButton("Move - Left")) {

            var xVelocity = -10.0f * Time.deltaTime;

            transform.Translate(xVelocity, 0, 0);

        }

    }

}