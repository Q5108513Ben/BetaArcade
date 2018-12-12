using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class BotCounterValue : MonoBehaviour {

    Text text;

    void Start() {

        text = GetComponent<Text>();

    }

    public void UpdateValue(float value) {

        text.text = value.ToString();

    }
	
}
