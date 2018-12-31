using UnityEngine;
using System.Collections;

public class ObjectCreation : MonoBehaviour {

    public GameObject objectToCreate;
    public string objectToCollide;
    public uint noOfObjectsToCreate;

    public uint objectsPerRow;

    public Vector2 constantOffset = new Vector2(-0.5f, 1.0f);

    public Vector2[] objectSpawnOffset = new Vector2[100];

    public BotCounterWidget counter;

    public void Start() {

        float x = 0f;
        float y = 0f;

        float xOffset = 0.25f;
        float yOffset = 0.5f;

        for (int i = 1; i < objectSpawnOffset.Length; i++) {

            objectSpawnOffset[i - 1].Set(x + transform.position.x, y + transform.position.y);

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

            if (counter != null) {

                counter.AddCounter(noOfObjectsToCreate / 100f);

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