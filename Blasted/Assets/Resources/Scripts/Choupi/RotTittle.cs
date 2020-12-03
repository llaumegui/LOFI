using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotTittle : MonoBehaviour
{
	float _interpolator;
	bool _switch;
	int _multiplier = 20;
	bool _first = true;

	private void Update()
	{
		if(_switch)
		{
			transform.localScale = Vector3.Lerp(Vector3.zero, (Vector3.one * 10), _interpolator);
			transform.Rotate(Vector3.forward * -Time.deltaTime * _multiplier *2);
		}
		else
		{
			if(_first)
			{
				transform.localScale = Vector3.Lerp((Vector3.one * 10), Vector3.zero, _interpolator);
				transform.Rotate(Vector3.forward * Time.deltaTime * _multiplier);
			}
			else
			{
				transform.localScale = Vector3.Lerp((Vector3.one * 10), Vector3.zero, _interpolator);
				transform.Rotate(Vector3.forward * Time.deltaTime * _multiplier *2);
			}
		}

		_interpolator += Time.deltaTime / 5;

		if(_interpolator >= 1)
		{
			_first = false;
			_switch = !_switch;
			_interpolator = 0;
		}
	}
}
