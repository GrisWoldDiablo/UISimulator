using UnityEngine;

public class CameraController : MonoBehaviour
{
	[SerializeField]
	private float _mouseSensitivity = 250.0f;

	[SerializeField]
	private float _moveSpeed = 15.0f;

	float _xRotation;

	void Update()
	{
		var uiMain = UIMain.Get();
		if (!uiMain || uiMain.IsMainMenuOn)
		{
			return;
		}

		float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
		float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;
		_xRotation -= mouseY;
		_xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
		transform.localRotation = Quaternion.Euler(_xRotation, transform.rotation.eulerAngles.y + mouseX, 0f);

		float moveHorizontal = Input.GetAxis("Horizontal") * _moveSpeed * Time.deltaTime;
		float moveVertical = Input.GetAxis("Vertical") * _moveSpeed * Time.deltaTime;

		Vector3 moveUpDown = Vector3.zero;
		if (Input.GetKey(KeyCode.E))
		{
			moveUpDown = transform.up * (_moveSpeed * Time.deltaTime);
		}
		else if (Input.GetKey(KeyCode.Q))
		{
			moveUpDown = transform.up * (-_moveSpeed * Time.deltaTime);
		}

		Vector3 moveDirection = transform.forward * moveVertical + transform.right * moveHorizontal + moveUpDown;
		transform.position += moveDirection;
	}
}