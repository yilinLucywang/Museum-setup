using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class AudioPosTriggerData : AudioPoint {
	public string methodName;
	public string data;	//RCC added

	public AudioPosTriggerData(float audioPos, string methodName, string data) : base(audioPos){	//RCC added data
		this.methodName = methodName;
		this.data = data;	//RCC added
	}
}
