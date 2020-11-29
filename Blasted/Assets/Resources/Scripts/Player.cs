using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	Transform _thisTransform;

	[Header ("Transforms")]
	public Transform _head;
	public Transform _body;
	public Transform _crosshair;
	public Transform _camera;
	public Transform Gun;
	public Transform Sword;
	public Transform SwordUI;
	public Transform Legs;

	[Header("")]
	public float Speed;
	bool _dead;
	Vector2 _direction;
	Vector2 _crosshairDirection;
	Rigidbody2D _rb;

	float _vertical;
	float _horizontal;

	public float MaxCrosshairDistance;

	bool _sword;
	bool _gun;

	[HideInInspector]
	public bool CanMoveCrosshair = true;

	[HideInInspector]
	public bool CanMove = true;

	int _leg;

	private void Awake()
	{
		Sword.gameObject.SetActive(false);
		Gun.gameObject.SetActive(false);
		SwordUI.gameObject.SetActive(false);
		Legs.gameObject.SetActive(false);

		_thisTransform = GetComponent<Transform>();

		_rb = GetComponent<Rigidbody2D>();

		StartCoroutine(LegsMoving());
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0) && _sword && !_dead)
			Slash();
		if (Input.GetMouseButtonDown(1) && _gun && !_dead)
			Shoot();
	}

	private void FixedUpdate()
	{
		if(!_dead && CanMove)
		Movement();
	}

	void Movement()
	{
		_vertical = 0;
		_horizontal = 0;

		if(Input.GetKey(GameMaster.KeyCodes[0]))
		{
			_vertical += 1;
		}
		if (Input.GetKey(GameMaster.KeyCodes[1]))
		{
			_vertical -= 1;
		}
		if (Input.GetKey(GameMaster.KeyCodes[2]))
		{
			_horizontal -= 1;
		}
		if (Input.GetKey(GameMaster.KeyCodes[3]))
		{
			_horizontal += 1;
		}

		_direction = new Vector2(_horizontal, _vertical);

		_rb.velocity = _direction * Speed;

		if(CanMoveCrosshair)
		CrosshairMove();

		ApplyRotation();

	}

	void CrosshairMove()
	{
		_crosshair.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		_crosshair.position = new Vector3(_crosshair.position.x, _crosshair.position.y, 0);

		_crosshairDirection = _crosshair.position - _thisTransform.position;

		if (Vector2.Distance(_crosshair.position, _thisTransform.position) > MaxCrosshairDistance*4)
			_crosshair.localPosition = _crosshairDirection.normalized * MaxCrosshairDistance;

		_camera.position = _thisTransform.position + ((_crosshair.position - _thisTransform.position)/2);
		_camera.position = new Vector3(_camera.position.x, _camera.position.y, -2);
	}

	void ApplyRotation()
	{
		float difAngle = Vector3.SignedAngle(_head.up, _crosshairDirection, transform.forward);
		float target = _head.rotation.eulerAngles.z + difAngle;
		_head.rotation = Quaternion.Slerp(_head.rotation, Quaternion.Euler(0, 0, target), .5f);

		float Angle = Vector3.SignedAngle(_body.up, _direction, transform.forward);
		float targetAngle = _body.rotation.eulerAngles.z + Angle;
		if(_horizontal !=0 || _vertical !=0)
		_body.rotation = Quaternion.Slerp(_body.rotation, Quaternion.Euler(0, 0, targetAngle), .25f);
	}

	void Slash()
	{

	}

	void Shoot()
	{

	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "Ennemy")
		{
			Debug.Log("MORT");
		}
	}

	IEnumerator LegsMoving()
	{
		yield return new WaitForSeconds(.25f);

		if(_direction != Vector2.zero)
		{
			Legs.gameObject.SetActive(true);
			if(_leg == 0)
			{
				Legs.GetChild(0).gameObject.SetActive(true);
				Legs.GetChild(1).gameObject.SetActive(false);
			}
			else
			{
				Legs.GetChild(0).gameObject.SetActive(false);
				Legs.GetChild(1).gameObject.SetActive(true);
			}

		}
		else
		{
			Legs.gameObject.SetActive(false);
		}

		_leg++;
		if (_leg > 1)
			_leg = 0;

		StartCoroutine(LegsMoving());
	}

}
