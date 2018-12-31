using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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

        if (value > 1) { value = 1; }

        StartCoroutine(IncreaseFill(value));

        text.UpdateValue(value * 100);

    }

    public void AddCounter(float value) {

        float maxValue = image.fillAmount + value;
        if (maxValue > 1) { maxValue = 1; }

        StartCoroutine(IncreaseFill(maxValue));
        int newValue = (int)((image.fillAmount * 100) + (value * 100));

        text.UpdateValue(newValue);

    }

    public void EmptyCounter() {

        StartCoroutine(DecreaseFill(0));
        text.UpdateValue(0);

    }

    IEnumerator IncreaseFill(float maxValue) {

        while (image.fillAmount < maxValue) {

            image.fillAmount += 0.2f * Time.deltaTime;
            yield return new WaitForEndOfFrame();

        }

    }

    IEnumerator DecreaseFill(float value) {

        while (image.fillAmount > value) {

            image.fillAmount -= 0.4f * Time.deltaTime;
            yield return new WaitForEndOfFrame();

        }

    }

}
