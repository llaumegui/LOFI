using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Host : Menu
{
	public override void Select()
	{
		StartCoroutine(Wait());
	}

	IEnumerator Wait()
	{
		Debug.Log("je lance le jeu");

		yield return new WaitForSeconds(.5f);

		MainMenu.PlayCoop?.Invoke("00000");
	}
}
