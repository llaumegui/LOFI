using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : MonoBehaviour
{
	List<Color> colors;

	public float SpawnTransition;

	public Transform Target;
	public float Speed;

	bool _canMove;

	private void Start()
	{

		gameObject.GetComponent<Collider2D>().enabled = false;

		GetColors();

		StartCoroutine(Spawning());
	}

	void GetColors()
	{
		colors = new List<Color>();

		for (int i = 0; i < transform.childCount; i++)
		{
			colors.Add(transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().color);
		}
	}

	IEnumerator Spawning()
	{
		List<float> floats = new List<float>();

		for(int i=0;i<colors.Count;i++)
		{
			floats.Add(colors[i].a);
			colors[i] = new Color(colors[i].r, colors[i].g, colors[i].b, 0);
			ApplyColor();
		}

		yield return new WaitForSeconds(SpawnTransition);

		for (int i = 0; i < colors.Count; i++)
		{
			floats.Add(colors[i].a);
			colors[i] = new Color(colors[i].r, colors[i].g, colors[i].b, floats[i]/3);
			ApplyColor();
		}

		yield return new WaitForSeconds(SpawnTransition);

		for (int i = 0; i < colors.Count; i++)
		{
			floats.Add(colors[i].a);
			colors[i] = new Color(colors[i].r, colors[i].g, colors[i].b, floats[i] /2);
			ApplyColor();
		}

		yield return new WaitForSeconds(SpawnTransition);

		for (int i = 0; i < colors.Count; i++)
		{
			floats.Add(colors[i].a);
			colors[i] = new Color(colors[i].r, colors[i].g, colors[i].b, floats[i]);
			ApplyColor();
		}

		yield return new WaitForSeconds(SpawnTransition);

		_canMove = true;
		gameObject.GetComponent<Collider2D>().enabled = true;

	}

	void ApplyColor()
	{
		for(int i=0;i<transform.childCount;i++)
		{
			transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().color = colors[i];
		}
	}

	private void Update()
	{
		if(_canMove)
		{
			Movement();
		}
	}

	void Movement()
	{
		Vector2 targetPos = Target.position;
		float step = Speed * Time.deltaTime;

		transform.position = Vector2.MoveTowards(transform.position, targetPos, step);
	}
}
