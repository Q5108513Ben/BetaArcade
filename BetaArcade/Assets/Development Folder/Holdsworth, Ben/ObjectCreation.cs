using UnityEngine;
using System.Collections;

public class ObjectCreation : MonoBehaviour {

    public GameObject objectToCreate;
    public string objectToCollide;
    public uint noOfObjectsToCreate = 1;
    public CoreAttraction player;

    public Vector2[] objectSpawnOffset = new Vector2[20];

    public void Start() {

        objectToCreate.transform.position = transform.position;

        float x = -2.5f;
        float y = 2f;

        for (int i = 0; i < objectSpawnOffset.Length; i++) {

            objectSpawnOffset[i].Set(x, y);

        }

    }


    private void OnTriggerEnter(Collider collider) {
        
        if (collider.CompareTag(objectToCollide)) {

            float timeToWait = 0.2f;

            for (int i = 0; i < noOfObjectsToCreate; i++) {

                GameObject newBot = Instantiate(objectToCreate);
                newBot.transform.position.x += objectSpawnOffset[i].x;

                StartCoroutine(WaitOnSpawn(newBot, timeToWait));

                timeToWait += 0.2f;

            }

        }

    }

    IEnumerator WaitOnSpawn(GameObject obj, float timeToWait) {

        bool isTimerDone = false;

        while (!isTimerDone) {

            yield return new WaitForSeconds(timeToWait);

            obj.tag = "Bot";
            isTimerDone = true;

        }
     
    }

}
