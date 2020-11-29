using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsTV : MonoBehaviour
{
	SpriteRenderer _spriteRenderer;

	int value;

	private void Start()
	{
		_spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void Update()
	{
		if(value ==0)
		_spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, Random.Range(.25f, .75f));

		value++;
		if (value >= 30)
			value = 0;
	}
}
