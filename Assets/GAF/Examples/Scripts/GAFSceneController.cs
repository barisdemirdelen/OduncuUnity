
// File:			GAFSceneController.cs
// Version:			5.2
// Last changed:	2017/3/31 09:57
// Author:			Nikitin Nikolay, Nikitin Alexey
// Copyright:		© 2017 GAFMedia
// Project:			GAF Unity plugin


using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GAFSceneController : MonoBehaviour 
{
	public void RunDemoScene(string _SceneName)
	{
		SceneManager.LoadScene(_SceneName);
	}

	private void Update()
	{
		if (Input.GetMouseButtonUp(0))
		{
			if (SceneManager.GetActiveScene().name != "Main")
				SceneManager.LoadScene("Main");
		}
	}
}
