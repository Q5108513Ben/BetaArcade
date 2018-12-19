using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Text))]
public class BotCounterValue : MonoBehaviour {

    Text text;
    public int currentValue = 0;

    void Start() {

        text = GetComponent<Text>();

    }

    public void UpdateValue(float value) {

        int valueToIncrementBy = (int)value - currentValue;

        if (valueToIncrementBy > 0)
        {
            StartCoroutine(IncrementCounter(valueToIncrementBy));
        }
        else
        {
            StartCoroutine(DecrementCounter(valueToIncrementBy));
        }

        

    }

    IEnumerator IncrementCounter(int value) {

        int index = 0;

        while (index < value) {

            currentValue++;
            text.text = currentValue.ToString();
            index++;

            yield return new WaitForSeconds(0.045f);

        }
        
    }

    IEnumerator DecrementCounter(int value)
    {

        int tempValue = value * -1;

        int index = 0;

        while (index < tempValue)
        {

            currentValue--;
            text.text = currentValue.ToString();
            index++;

            yield return new WaitForSeconds(0.02f);

        }

    }

}
