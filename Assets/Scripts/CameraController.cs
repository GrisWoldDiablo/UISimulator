using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public float MouseSensitivity = 100f;
	public float MoveSpeed = 15.0f;
	
	float _xRotation = 0f;

	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update()
	{
		float mouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
		float mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;
		_xRotation -= mouseY;
		_xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
		transform.localRotation = Quaternion.Euler(_xRotation, transform.rotation.eulerAngles.y + mouseX, 0f);

		float moveHorizontal = Input.GetAxis("Horizontal") * MoveSpeed * Time.deltaTime;
		float moveVertical = Input.GetAxis("Vertical") * MoveSpeed * Time.deltaTime;

		Vector3 moveDirection = transform.forward * moveVertical + transform.right * moveHorizontal;
		transform.position += moveDirection;
	}
}