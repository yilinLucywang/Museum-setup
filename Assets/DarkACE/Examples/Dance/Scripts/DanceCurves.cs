using UnityEngine;
using System.Collections;

public class DanceCurves : MonoBehaviour {
	AudioEvents events;
	Animator animator;

	void Start () {
		events = GetComponent<AudioEvents>();
		animator = GetComponent<Animator>();
	}

	void FixedUpdate () {
		animator.SetFloat("Side", events.GetCurrentValue(0));
		animator.SetFloat("Intensity", events.GetCurrentValue(1));
	}

	void Macarena () {
        animator.SetTrigger("Macarena");
	}
	
	void Idle () {
		animator.SetTrigger("Idle");
	}
	
	void CrossLegs () {
		animator.SetTrigger("CrossLegs");
	}
	
	void Star () {
		animator.SetTrigger("Star");
	}
	
	void March () {
		animator.SetTrigger("March");
	}
	
	void RestartMusic () {
		GetComponent<AudioSource>().time = 0f;
		GetComponent<AudioSource>().Play();
	}
}
