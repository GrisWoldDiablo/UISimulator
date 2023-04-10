using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIMain : MonoBehaviour
{
	private static UIMain _sInstance;

	public static UIMain Get()
	{
		if (!_sInstance)
		{
			_sInstance = FindObjectOfType<UIMain>();
		}

		return _sInstance;
	}

	[SerializeField]
	private Canvas _hud;

	[SerializeField]
	private CanvasGroup _mainMenu;

	[FormerlySerializedAs("_milliseconds")]
	[SerializeField]
	private float _seconds = 2.0f;

	[SerializeField]
	private GameObject[] _canvas;

	[SerializeField]
	private Button _buttonTest;

	[SerializeField]
	private Button _buttonPrevious;

	[SerializeField]
	private Button _buttonNext;

	[SerializeField]
	private UITextLabel _mouseLabel;

	private int _currentIndex = 0;

	private Stack<UITextLabel> _labels = new();

	private void Awake()
	{
		_mouseLabel.gameObject.SetActive(false);
	}

	void Start()
	{
		_buttonTest.onClick.AddListener(OnClicked);
		_buttonPrevious.onClick.AddListener(OnClickedPrevious);
		_buttonNext.onClick.AddListener(OnClickedNext);

		foreach (var canvas in _canvas)
		{
			canvas.SetActive(false);
		}

		ChangeIndex(0);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			// _mainMenu.gameObject.SetActive(!_mainMenu.gameObject.activeSelf);
			StartCoroutine(Fading());
		}
	}

	private void ChangeIndex(int value)
	{
		_canvas[_currentIndex].SetActive(false);


		_currentIndex += value;

		if (_currentIndex >= _canvas.Length)
		{
			_currentIndex = 0;
		}
		else if (_currentIndex < 0)
		{
			_currentIndex = _canvas.Length - 1;
		}

		_canvas[_currentIndex].SetActive(true);
	}

	private void OnClicked()
	{
		Debug.Log($"Clicked time is {DateTime.Now.ToLongDateString()} ");
		Fade();
		StartCoroutine(Fading());
	}

	async void Fade()
	{
		Debug.Log("Fade Before");
		await Task.Delay((int)_seconds);
		Debug.Log("Fade After");
	}

	IEnumerator Fading()
	{
		bool shouldFade = _mainMenu.alpha > 0.0f;
		if (!shouldFade)
		{
			_mainMenu.gameObject.SetActive(true);
		}

		Debug.Log("Fading Before");
		do
		{
			float fadingSpeed = Time.deltaTime / _seconds;
			_mainMenu.alpha += shouldFade ? -1 * fadingSpeed : fadingSpeed;
			yield return new WaitForEndOfFrame();
		} while (shouldFade ? _mainMenu.alpha > 0.0f : _mainMenu.alpha < 1.0f);

		if (shouldFade)
		{
			_mainMenu.gameObject.SetActive(false);
		}

		Debug.Log("Fading After");
	}

	private void OnClickedPrevious()
	{
		ChangeIndex(-1);
	}

	private void OnClickedNext()
	{
		ChangeIndex(1);
	}

	public UITextLabel GetTextLabel(string labelText)
	{
		if (!_labels.TryPop(out var label))
		{
			label = Instantiate(_mouseLabel, _hud.transform);
		}

		label.gameObject.SetActive(true);
		label.SetText(labelText);
		return label;
	}

	public void ReturnTextLabel(UITextLabel label)
	{
		label.gameObject.SetActive(false);
		_labels.Push(label);
	}
}