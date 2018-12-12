using UnityEngine;
using System.Collections;

public class RefillStation : MonoBehaviour {

    public CoreAttraction player;
    public GameObject objectToCreate;
    public int refillMax;
    public int maxCreatedBots;

    public int objectsPerRow;
    public Vector2 constantOffset = new Vector2(-0.5f, 1.0f);
    public Vector2[] objectSpawnOffset = new Vector2[100];

    private int noOfObjectsToCreate;
    private bool hasRefilled = false;

    public float cooldownTimer;

    public BotCounterWidget counter;

    // I am planning on adding a UI number above all refill stations that represents the maximum amount of 
    // bots the player can go up to through the repeated use of the refill station. 

    // To do this we could create a canvas that covers the entire world. Each refill station will have a reference
    // to the canvas, and upon creation will add a text element to the UI. Its position will be the position of the 
    // refill station plus an offset. The text added to the screen will be the int 'maxCreatedBots', we can just 
    // convert this to a string and add that as the Text element's text property. 

    // If I get this working I will talk to Ryan about implementing the same feature with the pressure plates so
    // that the user can clearly see how many bots are required to activate the plate.

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

        if (hasRefilled) { return;  }

        if (collider.CompareTag("Core")) {

            int currentBots = player.usedBots.Count + player.unusuedBots.Count;

            if (currentBots > refillMax) { return; }

            else {

                noOfObjectsToCreate = refillMax - currentBots;

                if (noOfObjectsToCreate > maxCreatedBots) {
                    noOfObjectsToCreate = maxCreatedBots;
                }

            }

            float timeToWait = 0.1f;

            for (int i = 0; i < noOfObjectsToCreate; i++)
            {

                GameObject newBot = Instantiate(objectToCreate);
                newBot.transform.Translate(objectSpawnOffset[i].x + constantOffset.x, objectSpawnOffset[i].y + constantOffset.y, 0);

                StartCoroutine(WaitOnSpawn(newBot, timeToWait));

                timeToWait += 0.05f;

            }

            hasRefilled = true;
            StartCoroutine(Cooldown(cooldownTimer));

            if (counter != null) {

                counter.SetCounter((currentBots + noOfObjectsToCreate) / 100f);

            }

        }

    }

    IEnumerator WaitOnSpawn(GameObject obj, float timeToWait)
    {

        bool isTimerDone = false;

        while (!isTimerDone)
        {

            yield return new WaitForSeconds(timeToWait);

            obj.tag = "Bot";
            obj.GetComponent<Rigidbody>().useGravity = true;
            isTimerDone = true;

        }

    }

    IEnumerator Cooldown(float timeToWait) {

        yield return new WaitForSeconds(timeToWait);

        hasRefilled = false;

    }

}
