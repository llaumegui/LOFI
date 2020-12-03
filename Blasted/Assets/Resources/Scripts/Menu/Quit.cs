using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Quit : Menu
{
	public override void Select()
	{
		StartCoroutine(Wait());
	}

	IEnumerator Wait()
	{

		yield return new WaitForSeconds(.5f);
		Application.Quit();
	}
}
