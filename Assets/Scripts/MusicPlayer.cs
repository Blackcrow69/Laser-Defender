using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {
	static MusicPlayer instance = null;
	
	public AudioClip startClip;
	public AudioClip gameClip;
	public AudioClip endClip;
	
	private AudioSource music;
	
	
	void Start () {
		if (instance != null && instance != this) {
			Destroy (gameObject);
			Debug.Log ("Duplicate music player self-destructing!");
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
			music = GetComponent<AudioSource>();
			music.clip = startClip;
			music.loop = true;
			music.volume = 1f;
			music.Play();
		}
		
	}
	
	void OnLevelWasLoaded (int level) {
		Debug.Log ("Music player loaded level " +level);
		music.Stop();
		if (level == 0) {
			music.clip = startClip;
			music.volume = 1f;
		}
		if (level == 1) {
			music.clip = gameClip;
			music.volume = 0.03f;
		}
		if (level == 2) {
			music.clip = endClip;
			music.volume = 0.1f;
		}
		music.loop = true;
		music.Play();
	}
}
