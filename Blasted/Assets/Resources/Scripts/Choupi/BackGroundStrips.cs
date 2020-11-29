using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundStrips : MonoBehaviour
{
	private void Start()
	{
		for(int i =0;i< transform.childCount;i++)
		{
			SpriteRenderer renderer = transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>();
			renderer.color = Color.HSVToRGB(0, 0, Random.Range(0, 1f));
			renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, .25f);
		}
	}
}
