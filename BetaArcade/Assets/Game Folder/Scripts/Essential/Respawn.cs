using UnityEngine;

public class Respawn : MonoBehaviour {

    private Vector3 respawnPosition = new Vector3(0, 0, 0);
    public Vector3 repsawnOffset = new Vector3(0, 0, 0);

    private void Start() {

        respawnPosition = gameObject.transform.position;

    }

    private void OnTriggerEnter(Collider other) {

        if (other.tag == "Player") {

            other.gameObject.GetComponent<PlayerMovement>().respawnLocation = respawnPosition;

        }

    }

}
