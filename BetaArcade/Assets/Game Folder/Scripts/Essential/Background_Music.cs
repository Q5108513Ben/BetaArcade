using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_Music : MonoBehaviour {
    
    private AudioSource aSource;

    [SerializeField]
    private List<AudioClip> BGMList = new List<AudioClip>();

    private float timeToChange = 300f;
    private float ttc = 0f;

    // Use this for initialization
    void Start()
    {
        aSource = GetComponent<AudioSource>();

        if (BGMList.Count > 0)
        {
            aSource.clip = BGMList[Random.Range(0, BGMList.Count)];
            if (!aSource.isPlaying)
                aSource.Play();
        }
        else
            Debug.Log("No BGM listed!");
    }

    // Update is called once per frame
    void Update()
    {
        if (ttc > timeToChange)
        {
            changeMusic(BGMList.IndexOf(aSource.clip));
            ttc = 0f;
        }
        else
            ttc += Time.deltaTime;
    }

    void changeMusic(int trackNumber)
    {
        int newTrack = Random.Range(0, BGMList.Count);

        if (newTrack == trackNumber)
            changeMusic(newTrack);
        else
        {
            aSource.Pause();
            aSource.clip = BGMList[newTrack];
            aSource.Play();
        }
    }
}
