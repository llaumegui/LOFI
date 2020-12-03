using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZQSD : Menu
{
	public List<Text> Texts;

	private void Start()
	{
		RefreshText();
	}

	public override void Select()
	{
		GameMaster.EnableWASD = !GameMaster.EnableWASD;
		GameMaster.SetControls();
		RefreshText();
	}

	void RefreshText()
	{
		foreach(Text text in Texts)
		{
			if(!GameMaster.EnableWASD)
				text.text = "ZQSD";
			else
				text.text = "WASD";
		}
	}
}
