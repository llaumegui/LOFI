using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsBackground : MonoBehaviour
{
	MeshRenderer _meshRenderer;
	Material _material;

	bool _canSwitch = true;

	private void Start()
	{
		_meshRenderer = GetComponent<MeshRenderer>();
	}

	private void Update()
	{
		if(_canSwitch)
		{
			_canSwitch = false;
			StartCoroutine(Cooldown());
			_meshRenderer.sharedMaterial.SetColor("_EmissionColor", Color.HSVToRGB(.515f, 1, Random.Range(.5f, 1)));
		}
	}

	IEnumerator Cooldown()
	{
		yield return new WaitForSeconds(Random.Range(.5f, 2));
		_canSwitch = true;
	}
}
