using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
	[HideInInspector]
	public bool _triggered;
	bool _playerInside;
	bool _antispam;
	bool _cleared;

	public GameObject Ennemy;
	public GameObject Player;
	public GameObject Bounds;
	[HideInInspector] public Player Script;
	Rect _area;
	public Vector2 SizeArea;

	TriggerSpawner _triggerSpawner;

	[Header("Spawner Stats")]
	public int NbrOfEnnemies;
	public float MinDistanceFromPlayer;
	public float StartTimeSpawn;
	public float MinTimeSpawn;
	public float SpacingPerModulo;
	public int Modulo;
	float _timeSpawn;


	[HideInInspector]
	public int _ennemiesKilled;

	IEnumerator _spawns;

	List<GameObject> _ennemies;

	public bool IsFinalZone;

	public Text FinalScore;

	public SoundManager.Sound AfterFightLoop;


	private void Awake()
	{
		if(FinalScore != null)
		FinalScore.text = " ";

		_spawns = Spawn();
		_ennemies = new List<GameObject>();
		Script = Player.GetComponent<Player>();

		_area = new Rect(transform.position.x-SizeArea.x/2,transform.position.y - SizeArea.y/2,SizeArea.x,SizeArea.y);
		//_triggerSpawner = transform.GetChild(0).gameObject.GetComponent<TriggerSpawner>();

		ResetSpawns();

		//SpawningEnnemy();
	}

	private void Update()
	{
		if (_triggered && _playerInside && !_antispam && !_cleared)
		{
			_antispam = true;
			StartCoroutine(_spawns);
		}

		if (CheckWinConditions() && !_cleared)
		{
			_cleared = true;
			ResetSpawns();

			SoundManager.PlaySound(SoundManager.Sound.Pickup);

			Destroy(SoundAssets.i.AudioLoops[0]);

			SoundAssets.i.AudioLoops = new List<GameObject>();
			SoundManager.PlayLoop("music", AfterFightLoop);

			Script.LoopToPlay = AfterFightLoop;
		}

		if (IsFinalZone && _triggered)
		{
			FinalScore.text = _ennemiesKilled.ToString();
		}

		if(Script.Hurt && !IsFinalZone)
		{
			ResetSpawns();
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			_playerInside = true;
			if (IsFinalZone)
				Script.IsInFinalZone = true;
		}

	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			_playerInside = false;
			ResetSpawns();
		}
	}

	IEnumerator Spawn()
	{
		yield return new WaitForSeconds(.5f);
		if (!CheckSpawnConditions())
			ResetSpawns();
		else
		{
			if(Bounds != null)
			Bounds.SetActive(true);

			if (!IsFinalZone)
				Destroy(SoundAssets.i.AudioLoops[0]);

			SoundAssets.i.AudioLoops = new List<GameObject>();
			SoundManager.PlayLoop("Music", SoundManager.Sound.MusicFight);


			for (int i =0;i<NbrOfEnnemies;i++)
			{
				yield return new WaitForSeconds(_timeSpawn);

				if (i % Modulo == 0 && _timeSpawn> MinTimeSpawn)
					_timeSpawn -= SpacingPerModulo;

				if (_timeSpawn < MinTimeSpawn)
					_timeSpawn = MinTimeSpawn;

				if (!CheckSpawnConditions())
					ResetSpawns();
				else
				{
					SpawningEnnemy();
				}
			}
		}
	}

	void SpawningEnnemy()
	{
		Vector2 instancePos = new Vector2(Random.Range(_area.xMin, _area.xMax), Random.Range(_area.yMin, _area.yMax));
		Debug.Log(instancePos);
		Debug.LogWarning("x :" + _area.xMin + " " + _area.xMax + " | y :" + _area.yMin + " " + _area.yMax);

		if (Vector2.Distance(instancePos, (Vector2)Player.transform.position) > MinDistanceFromPlayer)
		{
			GameObject ennemy = Instantiate(Ennemy, instancePos, Quaternion.identity);
			Ennemy script = ennemy.GetComponent<Ennemy>();
			script.SpawnerEnnemy = this;
			script.Target = Player.transform;

			_ennemies.Add(ennemy);

			if (!IsFinalZone)
			SoundManager.PlaySound(SoundManager.Sound.Spawn);
		}
		else
			SpawningEnnemy();
	}

	bool CheckSpawnConditions()
	{
		if (_playerInside)
			return true;
		else
			return false;
	}

	bool CheckWinConditions()
	{
		if (_ennemiesKilled == NbrOfEnnemies)
			return true;
		else
			return false;
	}

	void ResetSpawns()
	{
		_antispam = false;
		_ennemiesKilled = 0;
		_timeSpawn = StartTimeSpawn;

		StopCoroutine(_spawns);
		_spawns = Spawn();

		if (Bounds != null)
			Bounds.SetActive(false);

		foreach (GameObject ennemy in _ennemies)
		{
			ennemy.SetActive(false);
			Object.Destroy(ennemy);
		}

		_ennemies = new List<GameObject>();
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube(transform.position, SizeArea);
	}
}
