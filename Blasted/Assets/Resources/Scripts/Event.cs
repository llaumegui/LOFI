using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event : MonoBehaviour
{
	GameObject _player;
	Player _script;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag == "Player")
		{
			Debug.Log("marche");
			_player = collision.gameObject;
			_script = collision.gameObject.GetComponent<Player>();

			StartCoroutine(GoingToFinalZone());
		}
	}

	IEnumerator GoingToFinalZone()
	{
		StartCoroutine(_script.Waiting(7));
		yield return new WaitForSeconds(6);
		_script._respawnPosition = new Vector2(0, 350);
		StartCoroutine(_script.FadeOut());

	}
}
