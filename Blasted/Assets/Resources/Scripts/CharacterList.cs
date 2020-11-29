using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterList : MonoBehaviour
{
	public List<GameObject> Characters;

	private void Awake()
	{
		for(int i =0;i<transform.childCount;i++)
		{
			Characters.Add(transform.GetChild(i).gameObject);
		}
	}
}
