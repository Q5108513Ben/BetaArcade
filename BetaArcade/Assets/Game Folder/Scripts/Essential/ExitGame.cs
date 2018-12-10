using UnityEngine;

public class ExitGame : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {

        Application.Quit();

    }

}
