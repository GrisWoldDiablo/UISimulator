using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private static GameManager _sInstance;

	public static GameManager Get()
	{
		return _sInstance;
	}

	private void Awake()
	{
		if (!_sInstance)
		{
			_sInstance = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			DestroyImmediate(this);
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			UIMain.Get().ToggleMainMenu();
		}
	}
}