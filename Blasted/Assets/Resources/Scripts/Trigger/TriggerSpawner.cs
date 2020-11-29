using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TriggerSpawner : MonoBehaviour
{
	protected Spawner _ennemySpawner;

	public virtual void Awake()
	{
		_ennemySpawner = transform.parent.gameObject.GetComponent<Spawner>();
	}
}
