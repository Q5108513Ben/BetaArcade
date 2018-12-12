using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class BotCounterWidget : MonoBehaviour {

    Image image;

	void Start () {

        image = GetComponent<Image>();

        image.type = Image.Type.Filled;
        image.fillMethod = Image.FillMethod.Radial360;
        image.fillClockwise = true;
        
        image.fillAmount = 0;

	}

	void Update () {

        image.fillAmount += 0.1f * Time.deltaTime;

	}
}
