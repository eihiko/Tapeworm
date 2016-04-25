using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Splat : MonoBehaviour {

    AudioSource splatSound;

	// Use this for initialization
	void Start () {
        splatSound = GetComponent<AudioSource>();
        splatSound.Play();
        Destroy(gameObject, splatSound.clip.length);
	}

    void OnDestroy()
    {
        Pitpex.GameOver();
    }
}
