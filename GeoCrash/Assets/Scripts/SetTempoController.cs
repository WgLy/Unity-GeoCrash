using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SetTempoController : MonoBehaviour
{
    public AudioController audioController;
    public float gameTime;
    float BPM;
    public int tapTimes;
    public float averange;

    // Start is called before the first frame update
    void Start()
    {
        gameTime = 0;
        BPM = 103;
        audioController.audioSource.Play();
        tapTimes = 0;
        averange = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        gameTime += Time.fixedDeltaTime;
        
        if(Input.GetKeyDown(KeyCode.Space)){
            Debug.Log( ( Math.Min( gameTime%(60.0f/BPM) , 60.0f/BPM- (gameTime%(60.0f/BPM)) ) ).ToString() );
            averange = (averange*tapTimes+Math.Min( gameTime%(60.0f/BPM) , 60.0f/BPM- (gameTime%(60.0f/BPM)) ))/(tapTimes+1);
            tapTimes ++;
        }
    }
}
