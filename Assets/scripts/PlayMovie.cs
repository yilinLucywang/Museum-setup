using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlayMovie : MonoBehaviour {

    public bool useAce = false;
    public AudioSource aceAS;

	private VideoPlayer videoPlayer;

	// Use this for initialization
	void Start () {
		videoPlayer = GetComponent<VideoPlayer> ();
        this.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlayVideo() {
        // called by Triggers
		this.gameObject.SetActive(true);
		videoPlayer.frame = 0;
		videoPlayer.Play ();
        if (useAce)
        {
            aceAS.volume = 0;
            aceAS.Play();
        }
	}

    public float GetMovieLength() {
        return ((float)videoPlayer.clip.length);
    }

	public void StopVideo()
	{
		videoPlayer.Stop();
		if (useAce) aceAS.Stop();
	}
}
