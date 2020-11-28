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

	[Header("PauseMenu")]
	public bool TogglePauseMenu;
	[SerializeField] KeyCode _togglePause;
	[SerializeField] GameObject _pauseBackGround;
	[SerializeField] GameObject[] _menusToHide;
	[HideInInspector] public bool _paused;

	bool _antispamUP;
	bool _antispamDown;

	private void Start()
	{
		_selected = 0;

		if (!TogglePauseMenu)
			UpdateSelection();
		else
			TogglePause();

		PlayMusic();
	}

	void PlayMusic()
	{
		if(!_inGame)
		SoundManager.PlayLoop("Music", SoundManager.Sound.MusicMenu);
	}

	public void TogglePause()
	{
		if(!_paused)
		{
			foreach(GameObject menu in _menusToHide)
			{
				menu.SetActive(false);
			}
			_pauseBackGround.SetActive(false);
			Time.timeScale = 1;

			Debug.Log("pause désactivée");
		}
		else //toggle pause
		{
			foreach (GameObject menu in _menusToHide)
			{
				menu.SetActive(true);
			}
			_pauseBackGround.SetActive(true);
			Time.timeScale = 0;

			Debug.Log("pause activée");

			UpdateSelection();
		}
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
		if(Input.GetKeyDown(_togglePause)&& TogglePauseMenu)
		{
			_paused = !_paused;
			TogglePause();
		}

		if(_paused || !TogglePauseMenu)
		Navigation();
	}

	void Navigation()
	{
		if(Input.GetAxisRaw("Vertical") > .75f && !_antispamUP)
		{
			_antispamUP = true;
			_antispamDown = false;
			CheckOutOfRange(-1);
			SoundManager.PlaySound(SoundManager.Sound.MenuMove, 10);
		}
		if (Input.GetAxisRaw("Vertical") < -.75f && !_antispamDown)
		{
			_antispamUP = false;
			_antispamDown = true;
			CheckOutOfRange(+1);
			SoundManager.PlaySound(SoundManager.Sound.MenuMove, 10);
		}

		if(Input.GetAxisRaw("Vertical") == 0)
		{
			_antispamDown = false;
			_antispamUP = false;
		}

		if(Input.GetButtonDown("Action"))
		{
			menus[_selected].menu.Select();
			SoundManager.PlaySound(SoundManager.Sound.MenuOk, 10);
		}
	}

	void CheckOutOfRange(int plusOrMinus)
	{
		_selected = _selected + plusOrMinus == menus.Length || _selected + plusOrMinus < 0 ? _selected : _selected + plusOrMinus;
		UpdateSelection();
	}
}
