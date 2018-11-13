using UnityEngine;

public class ObjectCreation : MonoBehaviour {

    public GameObject objectToCreate;
    public string objectToCollide;
    public uint noOfObjectsToCreate = 1;

    public void Start()
    {
        objectToCreate.transform.position = transform.position;
    }


    private void OnTriggerEnter(Collider collider) {
        
        if (collider.CompareTag(objectToCollide)) {

            for (int i = 0; i < noOfObjectsToCreate; i++) {

                Instantiate(objectToCreate);

            }

        }

    }

}
