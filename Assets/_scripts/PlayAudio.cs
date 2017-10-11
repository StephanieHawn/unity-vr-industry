using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour {

	public GvrAudioSource gvrAudio;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlayAudioSource(GvrAudioSource gvrAudio){
		//gvrAudio.Play ();
		if (gvrAudio.isPlaying) {
			gvrAudio.Pause ();
		} else {
			gvrAudio.Play ();
		}
	}
}
