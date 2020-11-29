using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
	public static bool EnableWASD;
	public bool WASD;

	public static KeyCode[] KeyCodes = new KeyCode[4];

	private void Awake()
	{
		if (WASD)
			EnableWASD = true;
		else
			EnableWASD = false;

		DontDestroyOnLoad(this);
		SetControls();
	}

	void SetControls()
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


}
