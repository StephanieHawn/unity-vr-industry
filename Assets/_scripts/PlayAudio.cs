using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour {

	public GvrAudioSource background;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlayAudioSource(GvrAudioSource stationAudio){
		//gvrAudio.Play ();
		if (stationAudio.isPlaying) {
			stationAudio.Pause ();
			background.Play ();
		} else {
			stationAudio.Play ();
			background.Pause ();
		}
	}
}
