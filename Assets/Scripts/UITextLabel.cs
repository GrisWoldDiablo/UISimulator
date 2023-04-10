using System;
using TMPro;
using UnityEngine;

public class UITextLabel : MonoBehaviour
{
	[SerializeField]
	private TMP_Text _text;

	private Camera _camera;
	private RectTransform _rectTransform;

	private void Awake()
	{
		_camera = Camera.main;
		_rectTransform = (RectTransform)transform;
	}

	public void SetText(string text) => _text.SetText(text);

	private void Update()
	{
		Vector3 screenPos = _rectTransform.position;
		float xMin = _rectTransform.rect.width / 2;
		float xMax = Screen.width - xMin;
		float yMin = 0.0f;
		float yMax = Screen.height - _rectTransform.rect.height;

		if (screenPos.x < xMin)
			screenPos.x = xMin;

		if (screenPos.x > xMax)
			screenPos.x = xMax;

		if (screenPos.y < yMin)
			screenPos.y = yMin;

		if (screenPos.y > yMax)
			screenPos.y = yMax;

		_rectTransform.position = screenPos;
	}
}