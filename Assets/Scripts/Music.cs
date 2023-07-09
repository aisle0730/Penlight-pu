using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField]
    private AudioSource audio;

    public static Music instance;

    private Recorder recorder;

    private float time;


    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayMusic()
    {
        audio.time = 0f;
        audio.Play();
    }

    public void StopMusic()
    {
        audio.Stop();
    }

    public void PlayMusicMiddle()
    {
        audio.time = Recorder.instance.startTime;
        audio.Play();
    }

    public float GetTime()
    {
        time = audio.time;
        Debug.Log("ŽžŠÔ:" + time);
        return time;
    }
}
