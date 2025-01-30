using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown ("return")) 
		{
			SceneManager.LoadScene ("Main_11");
			Debug.Log ("Play Game");
		}

		if (Input.GetKeyDown ("space")) 
		{
			SceneManager.LoadScene ("Main_Instructions");
			Debug.Log ("Instructions");
		}

		if (Input.GetKeyDown ("escape")) 
		{
			Application.Quit();
			Debug.Log ("Quit");
		}
	}
}
