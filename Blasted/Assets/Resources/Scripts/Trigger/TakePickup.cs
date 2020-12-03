using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakePickup : TriggerSpawner
{
	public GameObject Pickup;
	public bool SwordOrGun;
	bool _pickedUp;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag == "Player" && !_pickedUp)
		{
			_pickedUp = true;
			Player script = collision.gameObject.GetComponent<Player>();
			if (SwordOrGun)
				script._sword = true;
			else
				script._gun = true;

			Pickup.SetActive(false);

			_ennemySpawner._triggered = true;

			StartCoroutine(_ennemySpawner.Script.Waiting());

			SoundManager.PlaySound(SoundManager.Sound.Pickup,.75f);
		}
	}

}
