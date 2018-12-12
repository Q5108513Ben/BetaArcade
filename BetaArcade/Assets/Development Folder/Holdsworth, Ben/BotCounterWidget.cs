using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class BotCounterWidget : MonoBehaviour {

    Image image;
    public BotCounterValue text;

	void Start () {

        image = GetComponent<Image>();

        image.type = Image.Type.Filled;
        image.fillMethod = Image.FillMethod.Radial360;
        image.fillOrigin = 135;
        image.fillClockwise = true;

        
        image.fillAmount = 0;

	}

	public void SetCounter(float value) {

        image.fillAmount = value;
        text.UpdateValue(value * 100);

    }

    public void AddCounter(float value) {

        text.UpdateValue((image.fillAmount + value) * 100);
        image.fillAmount += value;

    }

}
