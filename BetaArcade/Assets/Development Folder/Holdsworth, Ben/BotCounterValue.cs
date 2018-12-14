using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Text))]
public class BotCounterValue : MonoBehaviour {

    Text text;
    private Coroutine co;

    void Start() {

        text = GetComponent<Text>();

    }

    public void UpdateValue(float value) {

        if (co != null) {

            //StopCoroutine(co);

        }

        co = StartCoroutine(IncrementCounter(value));

    }

    IEnumerator IncrementCounter(float value) {

        float currentValue = 0;

        for (int i = 0; i < text.text.Length; i++) {

            char letter = text.text[i];
            currentValue = 10 * currentValue + (letter - 48);

        }

        while (currentValue <= value) {

            text.text = currentValue.ToString();
            currentValue++;

            yield return new WaitForSeconds(0.045f);

        }
        
    }
	
}
