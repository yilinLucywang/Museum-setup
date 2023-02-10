using UnityEngine;
using System.Collections;

public class CreateInstance : MonoBehaviour {

	public GameObject original;

	void DoInstance(){
		GameObject.Instantiate(original, new Vector3(Random.value, Random.value, Random.value), Quaternion.identity);
	}

	void DoInstanceWithData(string data){
		switch (data) {
		case "Hello":
			GameObject.Instantiate(original, new Vector3(Random.value, Random.value, Random.value), Quaternion.identity);
			break;
		case "right":
			GameObject.Instantiate(original, new Vector3(Random.value + 5f, Random.value, Random.value), Quaternion.identity);
			break;
		case "":
			Debug.Log ("Event Trigger with no Data");
			break;
		default:
			Debug.Log ("Event Trigger with Data not found: " + data);
			break;
		}
	}
}
