using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : Menu
{	
	public override void Select()
	{
		StartCoroutine(Wait());
	}

	IEnumerator Wait()
	{
		Debug.Log("je lance le jeu");

		yield return new WaitForSeconds(.5f);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
