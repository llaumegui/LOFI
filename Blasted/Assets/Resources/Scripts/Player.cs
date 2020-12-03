using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
	Transform _swordCollider;

	[Header("GameObjects")]
	public GameObject SwordBack;
	public GameObject GunBack;
	public GameObject Bullet;
	public GameObject BlackScreen;

	TV _endScreen;

	[Header("")]
	public float Speed;
	Vector2 _direction;
	Vector2 _crosshairDirection;
	Rigidbody2D _rb;

	float _vertical;
	float _horizontal;

	public float MaxCrosshairDistance;

	public float SlashCooldown;
	public float ShootCooldown;

	public bool _sword;
	bool _usingSword;
	public bool _gun;
	bool _usingGun;

	public bool Hurt;

	public Vector2 _respawnPosition;

	[HideInInspector]
	public bool CanMoveCrosshair = true;

	[HideInInspector]
	public bool CanMove = true;

	bool _canUseSword = true;
	bool _canShoot = true;

	IEnumerator _sheathe;

	int _leg;

	public SoundManager.Sound LoopToPlay;

	[HideInInspector]
	public bool IsInFinalZone;

	private void Awake()
	{
		LoopToPlay = SoundManager.Sound.MusicMenu;
		_endScreen = GetComponent<TV>();

		Sword.gameObject.SetActive(false);
		Gun.gameObject.SetActive(false);
		SwordUI.gameObject.SetActive(false);
		Legs.gameObject.SetActive(false);
		_swordCollider = Sword.GetChild(0);

		_sheathe = Sheathe();

		ShowWeapons();

		_thisTransform = GetComponent<Transform>();

		_rb = GetComponent<Rigidbody2D>();

		StartCoroutine(LegsMoving());


		StartCoroutine(FadeIn(1));
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0) && _sword && CanMove && _canUseSword)
			Slash();
		if (Input.GetMouseButton(1) && _gun && CanMove && _canShoot)
			Shoot();

		if (_sword || _gun)
			ShowWeapons();

		if(_usingSword)
			_swordCollider.localPosition = Vector3.zero;
	}

	private void FixedUpdate()
	{
		if(CanMove)
		Movement();
	}

	void ShowWeapons()
	{
		if (_sword)
		{
			SwordUI.gameObject.SetActive(true);
			if (_canUseSword)
				SwordUI.GetChild(0).gameObject.SetActive(true);
			else
				SwordUI.GetChild(0).gameObject.SetActive(false);
		}

		if(_gun && !_usingGun)
		{
			Gun.gameObject.SetActive(false);
			GunBack.SetActive(true);
		}
		if (_sword && !_usingSword)
		{
			Sword.gameObject.SetActive(false);
			SwordBack.SetActive(true);
		}
		if (_gun && _usingGun)
		{
			Gun.gameObject.SetActive(true);
			GunBack.SetActive(false);
		}
		if (_sword && _usingSword)
		{
			Sword.gameObject.SetActive(true);
			SwordBack.SetActive(false);
		}

		if(!_gun)
		{
			Gun.gameObject.SetActive(false);
			GunBack.SetActive(false);
		}
		if (!_sword)
		{
			Sword.gameObject.SetActive(false);
			SwordBack.SetActive(false);
		}

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
		_usingSword = true;
		_canUseSword = false;

		StartCoroutine(SwordCoolDown());

		SoundManager.PlaySound(SoundManager.Sound.Woosh);
	}

	IEnumerator SwordCoolDown()
	{
		yield return new WaitForSeconds(.25f);
		_usingSword = false;
		yield return new WaitForSeconds(SlashCooldown);
		_canUseSword = true;

		SoundManager.PlaySound(SoundManager.Sound.MenuOk,.75f);
	}

	void Shoot()
	{
		_usingGun = true;

		StartCoroutine(ShootingCooldown());

		StopCoroutine(_sheathe);
		_sheathe = Sheathe();
		StartCoroutine(_sheathe);

		Vector2 direction = _head.up;
		GameObject bullet = Instantiate(Bullet, (Vector2)transform.position + direction, Quaternion.identity);
		bullet.GetComponent<Bullet>().Direction = direction.normalized;

		SoundManager.PlaySound(SoundManager.Sound.Woosh);
	}

	IEnumerator ShootingCooldown()
	{
		_canShoot = false;
		yield return new WaitForSeconds(ShootCooldown);
		_canShoot = true;
	}

	IEnumerator Sheathe()
	{
		yield return new WaitForSeconds(1f);
		_usingGun = false;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "Ennemy")
		{
			if(!Hurt)
				SoundManager.PlaySound(SoundManager.Sound.Dead,.75f);
			Hurt = true;
			StartCoroutine(FadeOut());
		}
		if (collision.gameObject.tag == "Sword")
			Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
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

			SoundManager.PlaySound(SoundManager.Sound.Walk,.5f);

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

	public IEnumerator Waiting(float Wait = 1,bool final = false)
	{
		_rb.velocity = Vector3.zero;
		_direction = Vector2.zero;
		CanMove = false;
		yield return new WaitForSeconds(Wait);
		CanMove = true;

		if(final)
		{
			SceneManager.LoadScene(0);
		}
	}

	public IEnumerator FadeOut(float wait = .5f)
	{
		_rb.velocity = Vector3.zero;
		_direction = Vector2.zero;
		CanMove = false;
		Destroy(SoundAssets.i.AudioLoops[0]);
		SoundAssets.i.AudioLoops = new List<GameObject>();

		SpriteRenderer renderer = BlackScreen.GetComponent<SpriteRenderer>();
		renderer.color = new Color(0, 0, 0, 0);

		yield return new WaitForSeconds(wait);

		renderer.color = new Color(0, 0, 0, .25f);


		yield return new WaitForSeconds(wait);

		renderer.color = new Color(0, 0, 0, .5f);


		yield return new WaitForSeconds(wait);

		renderer.color = new Color(0, 0, 0, 1f);


		if(IsInFinalZone)
		{
			_endScreen.PlayTV();
			StartCoroutine(Waiting(10,true));
		}
		else
		{
			Hurt = false;
			StartCoroutine(FadeIn(wait));
			transform.position = _respawnPosition;
		}

	}

	IEnumerator FadeIn(float wait = .5f)
	{
		_rb.velocity = Vector3.zero;
		_direction = Vector2.zero;
		CanMove = false;

		SpriteRenderer renderer = BlackScreen.GetComponent<SpriteRenderer>();
		renderer.color = new Color(0, 0, 0, 1);

		yield return new WaitForSeconds(wait);

		renderer.color = new Color(0, 0, 0, .5f);


		yield return new WaitForSeconds(wait);

		renderer.color = new Color(0, 0, 0, .25f);


		yield return new WaitForSeconds(wait);

		renderer.color = new Color(0, 0, 0, 0f);

		CanMove = true;

		if (!IsInFinalZone)
		SoundManager.PlayLoop("music", LoopToPlay,1,true);
	}
}
