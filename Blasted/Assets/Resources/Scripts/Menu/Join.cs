using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Join : Menu
{
	string code;

	public override void Select()
	{
		code = InputCode.texte;

		if (code.Length != 5) return;

		StartCoroutine(Wait());
	}

	IEnumerator Wait()
	{
		Debug.Log("je lance le jeu");

		yield return new WaitForSeconds(.5f);

		MainMenu.PlayCoop?.Invoke(code);
	}
}
