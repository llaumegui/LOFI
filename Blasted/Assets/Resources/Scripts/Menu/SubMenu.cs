using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubMenu : Menu
{
	public GameObject[] ToHide;
	public GameObject[] ToShow;

	public override void Select()
	{
		MainMenu.CallChange?.Invoke() ;
		StartCoroutine(Wait());
	}


	IEnumerator Wait()
	{

		yield return new WaitForSeconds(.5f);

		foreach (GameObject ui in ToHide)
		{
			ui.SetActive(false);
		}

		foreach (GameObject ui in ToShow)
		{
			ui.SetActive(true);
		}

	}
}
