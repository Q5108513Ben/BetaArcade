using UnityEngine;
using System.Collections;

public class ObjectCreation : MonoBehaviour {

    public GameObject objectToCreate;
    public string objectToCollide;
    public uint noOfObjectsToCreate;

    public uint objectsPerRow;

    public Vector2 constantOffset = new Vector2(-0.5f, 1.0f);

    public Vector2[] objectSpawnOffset = new Vector2[100];

    public void Start() {

        objectToCreate.transform.position = transform.position;

        float x = 0f;
        float y = 0f;

        float xOffset = 0.25f;
        float yOffset = 0.5f;

        for (int i = 1; i < objectSpawnOffset.Length; i++) {

            objectSpawnOffset[i - 1].Set(x, y);

            x += xOffset;

            if (i % objectsPerRow == 0) {

                y += yOffset;
                x = 0f;

            }

        }

    }


    private void OnTriggerEnter(Collider collider) {
        
        if (collider.CompareTag(objectToCollide)) {

            float timeToWait = 0.1f;

            for (int i = 0; i < noOfObjectsToCreate; i++) {

                GameObject newBot = Instantiate(objectToCreate);
                newBot.transform.Translate(objectSpawnOffset[i].x + constantOffset.x, objectSpawnOffset[i].y + constantOffset.y, 0);

                StartCoroutine(WaitOnSpawn(newBot, timeToWait));

                timeToWait += 0.05f;

            }

        }

    }

    IEnumerator WaitOnSpawn(GameObject obj, float timeToWait) {

        bool isTimerDone = false;

        while (!isTimerDone) {

            yield return new WaitForSeconds(timeToWait);

            obj.tag = "Bot";
            obj.GetComponent<Rigidbody>().useGravity = true;
            isTimerDone = true;

        }
     
    }

}

// This is pretty much done now, the only thing I would like to add is delaying the spawn time in a similar method to the WaitOnSpawn function.
// Aside from that there is the refill station to do next which should be fairly similar. The Refill Station will have an additional variable
// that tracks the max amount of bots 
