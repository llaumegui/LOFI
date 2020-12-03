using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	int _selected;
	[SerializeField] bool _inGame;

	[System.Serializable]
	public class MenuSelected
	{
		public Menu menu;
		public GameObject Selection;
		public GameObject Deselection;
	}

	[SerializeField] MenuSelected[] menus;

	bool _antispamUP;
	bool _antispamDown;

	private void Start()
	{
		_selected = 0;

		UpdateSelection();

		PlayMusic();
	}

	void PlayMusic()
	{
		if(!_inGame)
		SoundManager.PlayLoop("Music", SoundManager.Sound.MusicMenu);
	}

	void UpdateSelection()
	{
		foreach (MenuSelected menu in menus)
		{
			if(menu == menus[_selected])
			{
				menu.Selection.SetActive(true);
				menu.Deselection.SetActive(false);
			}
			else
			{
				menu.Selection.SetActive(false);
				menu.Deselection.SetActive(true);
			}
		}
	}

	private void Update()
	{
		Navigation();
	}

	void Navigation()
	{
		if(Input.GetAxisRaw("Vertical") > .75f && !_antispamUP)
		{
			_antispamUP = true;
			_antispamDown = false;
			CheckOutOfRange(-1);
			SoundManager.PlaySound(SoundManager.Sound.MenuMove, 1);
		}
		if (Input.GetAxisRaw("Vertical") < -.75f && !_antispamDown)
		{
			_antispamUP = false;
			_antispamDown = true;
			CheckOutOfRange(+1);
			SoundManager.PlaySound(SoundManager.Sound.MenuMove, 1);
		}

		if(Input.GetAxisRaw("Vertical") == 0)
		{
			_antispamDown = false;
			_antispamUP = false;
		}

		if(Input.GetButtonDown("Action"))
		{
			menus[_selected].menu.Select();
			SoundManager.PlaySound(SoundManager.Sound.MenuOk, 1);
		}
	}

	void CheckOutOfRange(int plusOrMinus)
	{
		_selected = _selected + plusOrMinus == menus.Length || _selected + plusOrMinus < 0 ? _selected : _selected + plusOrMinus;
		UpdateSelection();
	}
}
