using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public Vector2 Direction;
	float _speed = 10;

	Rigidbody2D _rb;
	private void Start()
	{
		_rb = GetComponent<Rigidbody2D>();
		Destroy(gameObject, 3);
	}

	private void Update()
	{
		_rb.velocity = Direction * _speed;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Bounds")
			Destroy(gameObject);
	}
}
