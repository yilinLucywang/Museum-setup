using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;

public class DarkAceSaveLoad : ScriptableWizard {

	public GameObject go;
	public string fileName; 

	[MenuItem ("My Tools/DarkACE Save and Load...")]
	static void CreateWizard() {
		ScriptableWizard.DisplayWizard<DarkAceSaveLoad> ("DarkACE Save and Load", "Save Data", "Load Data");
	}

	void OnWizardCreate() {
		// Save Event Data
		AudioEvents events = go.GetComponent<AudioEvents> ();
		/*for (int i = 0; i < events.triggers.Count; i += 1) {
			Debug.Log ("***");
			Debug.Log (events.triggers [i].audioPos);
			Debug.Log (events.triggers [i].methodName);
		}*/
		// setup the path for data files
		string path = Application.dataPath + "/../dataFiles/";
		Debug.Log("the path for data files: " + path);
		// open the supplied file for writing
		StreamWriter sw = File.CreateText (path + fileName + ".csv");
		// begine writing 
		for (int i = 0; i < events.triggers.Count; i += 1) {
			string line = events.triggers [i].audioPos.ToString () + "," + events.triggers [i].methodName;
			sw.WriteLine(line);
		}
		// close the file
		sw.Close ();

	}

	void OnWizardOtherButton() {
		// Load Event Data
		AudioEvents events = go.GetComponent<AudioEvents> ();
		// remove existing events
		events.triggers = new List<AudioPosTrigger>();
		// open the supplied file for reading
		string path = Application.dataPath + "/../dataFiles/";
		StreamReader sr = File.OpenText(path + fileName + ".csv");
		// read in the data and store the info
		string line = sr.ReadLine();
		while (line != null) {
			string[] temp = line.Split(","[0]);  // seperate the string into elements based on ","s => time,message
			events.triggers.Add(new AudioPosTrigger(float.Parse(temp[0]),temp[1]));
			line = sr.ReadLine();
		}
		// close the file
		sr.Close ();
	}

	void OnWizardUpdate()
	{
		helpString = "Save or Load Event Data for the provided ACE game object";
	}
		
}
