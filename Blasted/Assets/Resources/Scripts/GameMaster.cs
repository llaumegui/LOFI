using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
	public static bool EnableWASD;
	public bool WASD;

	bool _showMouse;

	public static KeyCode[] KeyCodes = new KeyCode[4];

	private void Awake()
	{
		Screen.SetResolution(1720, 1000, false);

		if (WASD)
			EnableWASD = true;
		else
			EnableWASD = false;

		DontDestroyOnLoad(this);
		SetControls();

		Cursor.lockState = CursorLockMode.Confined;
		Cursor.visible = false;
	}

	public static void SetControls()
	{
		if (EnableWASD)
		{
			KeyCodes[0] = KeyCode.W;
			KeyCodes[1] = KeyCode.S;
			KeyCodes[2] = KeyCode.A;
			KeyCodes[3] = KeyCode.D;
		}
		else
		{
			KeyCodes[0] = KeyCode.Z;
			KeyCodes[1] = KeyCode.S;
			KeyCodes[2] = KeyCode.Q;
			KeyCodes[3] = KeyCode.D;
		}
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape) && !_showMouse)
		{
			_showMouse = true;
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
		if(_showMouse && Input.GetMouseButtonDown(0))
		{
			_showMouse = false;
			Cursor.lockState = CursorLockMode.Confined;
			Cursor.visible = false;
		}
	}


}
