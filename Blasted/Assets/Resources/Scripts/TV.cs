using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TV : MonoBehaviour
{
	bool _playing;
	bool _antispam;
	bool _firstPlay = true;

	public int CharacterId;

	public List<GameObject> ConstantsObjects;
	public GameObject CharacterList;
	GameObject _character;
	public string TextToType;
	public int SizeText;
	public Text[] ShowText;

	public float WaitTime;



	private void Start()
	{
		_character = CharacterList.GetComponent<CharacterList>().Characters[CharacterId];
		Hide();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			PlayTV(collision);
		}
		
	}

	public void PlayTV(Collider2D collision = null)
	{
		_playing = true;
		StartCoroutine(Show());

		if(collision != null)
		collision.gameObject.GetComponent<Player>().CanMoveCrosshair = false;
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if(collision.tag == "Player")
		{
			_playing = false;
			_antispam = false;
			_firstPlay = false;
			Hide();

			collision.gameObject.GetComponent<Player>().CanMoveCrosshair = true;
		}
	}

	private void Update()
	{
		if (_playing && !_antispam)
		{
			StartCoroutine(Playing());
			_antispam = true;
		}
	}

	IEnumerator Playing()
	{
		if (_firstPlay)
		{
			bool typing = false;
			for (int i = 0; i < TextToType.Length; i++)
			{
				if (!_playing)
					break;

				yield return new WaitForSeconds(WaitTime);
				ShowText[0].text += TextToType[i];
				ShowText[1].text += TextToType[i];

				if(!typing)
				SoundManager.PlaySound(SoundManager.Sound.WordTyping);

				typing = !typing;
			}
		}
		else
		{
			ShowText[0].text = TextToType;
			ShowText[1].text = TextToType;
			SoundManager.PlaySound(SoundManager.Sound.WordTyping);
		}
			
	}

	IEnumerator Show()
	{
		foreach(GameObject obj in ConstantsObjects)
		{
			obj.SetActive(true);
		}
		yield return new WaitForSeconds(.5f);
		_character.SetActive(true);

		foreach(Text text in ShowText)
		{
			text.enabled = true;

			if (SizeText != 0)
				text.fontSize = SizeText;
			else
				text.fontSize = 16;
		}

		if (!_playing)
			Hide();
	}

	void Hide()
	{

		foreach (GameObject obj in ConstantsObjects)
		{
			obj.SetActive(false);
		}
		_character.SetActive(false);

		foreach (Text text in ShowText)
		{
			text.text = "";
			text.enabled = false;
		}

	}




}
