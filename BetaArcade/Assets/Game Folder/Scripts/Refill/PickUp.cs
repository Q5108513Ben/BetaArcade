using UnityEngine;

public class PickUp : MonoBehaviour {

    public string objectToCollide;

    private bool hasBeenConsumed = false;

	// Update is called once per frame
	void Update () {
		


	}

    private void OnTriggerEnter(Collider collider) {

        if (!hasBeenConsumed) {
            
            if (collider.CompareTag(objectToCollide)) {
                
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                gameObject.GetComponent<SphereCollider>().enabled = false;
                hasBeenConsumed = true;

            }

        }

    }

}
