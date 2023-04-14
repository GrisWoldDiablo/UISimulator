using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
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
	private Button _buttonScene;

	[SerializeField]
	private Button _buttonTest;

	[SerializeField]
	private Button _buttonPrevious;

	[SerializeField]
	private Button _buttonNext;

	[SerializeField]
	private Button _buttonQuit;

	[SerializeField]
	private UIStatsHeader _statsHeader;

	[SerializeField]
	private Toggle _labelToggle;

	private int _currentCanvasIndex = 0;
	private int _currentSceneIndex = 0;

	private Stack<UIStatsHeader> _statsHeaders = new();

	private List<UIScreenSpaceLabel> _screenSpaceLabels = new();

	public bool IsMainMenuOn { get; private set; } = true;

	private void Awake()
	{
		if (Get() == this)
		{
			DontDestroyOnLoad(this);
		}
		else
		{
			DestroyImmediate(this);
			return;
		}

		_statsHeader.gameObject.SetActive(false);
		_labelToggle.onValueChanged.AddListener(OnToggleLabel);

		for (int i = _currentSceneIndex; i < SceneManager.sceneCount; i++)
		{
			if (SceneManager.GetSceneAt(i) == SceneManager.GetActiveScene())
			{
				_currentSceneIndex = i;
				break;
			}
		}
	}

	void Start()
	{
		_buttonScene.onClick.AddListener(OnSceneClicked);
		_buttonTest.onClick.AddListener(OnTestClicked);
		_buttonPrevious.onClick.AddListener(OnClickedPrevious);
		_buttonNext.onClick.AddListener(OnClickedNext);
		_buttonQuit.onClick.AddListener(OnClickedQuit);

		foreach (var canvas in _canvas)
		{
			canvas.SetActive(false);
		}

		ChangeIndex(0);
	}

	private void OnSceneClicked()
	{
		_currentSceneIndex++;
		if (_currentSceneIndex == SceneManager.sceneCountInBuildSettings)
		{
			_currentSceneIndex = 0;
		}

		SceneManager.LoadScene(_currentSceneIndex);
	}

	public void ToggleMainMenu()
	{
		StartCoroutine(Fading());
	}

	private void ChangeIndex(int value)
	{
		_canvas[_currentCanvasIndex].SetActive(false);

		_currentCanvasIndex += value;

		if (_currentCanvasIndex >= _canvas.Length)
		{
			_currentCanvasIndex = 0;
		}
		else if (_currentCanvasIndex < 0)
		{
			_currentCanvasIndex = _canvas.Length - 1;
		}

		_canvas[_currentCanvasIndex].SetActive(true);
	}

	private void OnTestClicked()
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
		_mainMenu.interactable = false;
		bool shouldFade = _mainMenu.alpha > 0.0f;
		if (!shouldFade)
		{
			Cursor.lockState = CursorLockMode.None;
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
			Cursor.lockState = CursorLockMode.Locked;
			_mainMenu.gameObject.SetActive(false);
			IsMainMenuOn = false;
		}
		else
		{
			IsMainMenuOn = true;
			_mainMenu.interactable = true;
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

	private void OnClickedQuit()
	{
		Application.Quit();
#if UNITY_EDITOR
		EditorApplication.isPlaying = false;
#endif
	}

	public UIStatsHeader GetStatsHeader(StatsScriptable stats)
	{
		if (!_statsHeaders.TryPop(out var statsHeader))
		{
			statsHeader = Instantiate(_statsHeader, _hud.transform);
		}

		statsHeader.gameObject.SetActive(true);
		statsHeader.SetStats(stats);
		return statsHeader;
	}

	public void ReturnStatsHeader(UIStatsHeader statsHeader)
	{
		statsHeader.gameObject.SetActive(false);
		_statsHeaders.Push(statsHeader);
	}

	public void RegisterScreenSpaceLabel(UIScreenSpaceLabel screenSpaceLabel)
	{
		if (screenSpaceLabel)
		{
			_screenSpaceLabels.Add(screenSpaceLabel);
		}
	}

	public void UnregisterScreenSpaceLabel(UIScreenSpaceLabel screenSpaceLabel)
	{
		if (screenSpaceLabel)
		{
			_screenSpaceLabels.Remove(screenSpaceLabel);
		}
	}

	private void OnToggleLabel(bool checkValue)
	{
		foreach (var screenSpaceLabel in _screenSpaceLabels)
		{
			if (screenSpaceLabel)
			{
				screenSpaceLabel.IsVisible = checkValue;
			}
		}
	}
}