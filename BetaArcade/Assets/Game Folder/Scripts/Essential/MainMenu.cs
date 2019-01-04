using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour {

    public List<Image> menuImages;
    public List<Text> menuText;

    private void Update() {
        
        if (Input.GetKeyDown(KeyCode.F5)) {

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }

    }


    public void EnableUI() {

        foreach (Image image in menuImages) {

            image.enabled = true;

        }

        foreach (Text text in menuText) {

            text.enabled = true;

        }

    }

    public void DisableUI() {

        foreach (Image image in menuImages) {

            image.enabled = false;

        }

        foreach (Text text in menuText) {

            text.enabled = false;

        }

    }

}
