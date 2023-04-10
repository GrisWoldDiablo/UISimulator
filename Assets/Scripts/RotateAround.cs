using UnityEngine;

public class RotateAround : MonoBehaviour
{
	[SerializeField]
	private bool _shouldLookAtPivot = false;
	
	[SerializeField]
	private Transform _pivot;

	[SerializeField]
	private float _speed = 1.0f;

	void Update()
	{
		transform.RotateAround(_pivot.position, _pivot.up, _speed * Time.deltaTime);
		if (_shouldLookAtPivot)
		{
			transform.LookAt(_pivot);
		}
	}
}