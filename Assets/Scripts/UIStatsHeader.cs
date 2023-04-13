using System;
using Unity.VisualScripting;
using UnityEngine;

public class UIStatsHeader : MonoBehaviour
{
	[SerializeField]
	private UIProgressBar _uiProgressBar;
	[SerializeField]
	private UITextLabel _uiTextLabel;

	private RectTransform _rectTransform;
	private StatsScriptable _stats;

	private void Awake()
	{
		_rectTransform = (RectTransform)transform;
	}

	private void Update()
	{
		Vector3 screenPos = _rectTransform.position;
		var rect = _rectTransform.rect;
		float xMin = rect.width / 2;
		float xMax = Screen.width - xMin;
		float yMin = 0.0f;
		float yMax = Screen.height - rect.height;

		if (screenPos.x < xMin)
		{
			screenPos.x = xMin;
		}
		else if (screenPos.x > xMax)
		{
			screenPos.x = xMax;
		}

		if (screenPos.y < yMin)
		{
			screenPos.y = yMin;
		}
		else if (screenPos.y > yMax)
		{
			screenPos.y = yMax;
		}

		_rectTransform.position = screenPos;
	}

	public void SetStats(StatsScriptable stats)
	{
		_stats = stats;
		UpdateProgressBar(_stats);
		_uiTextLabel.SetText(_stats.Name);
		_stats.OnHealthChanged += OnHealthChanged;
	}

	private void OnHealthChanged(StatsScriptable stats)
	{
		UpdateProgressBar(stats);
	}

	private void UpdateProgressBar(StatsScriptable stats)
	{
		_uiProgressBar.SetProgress(stats.HealthPercentage);
		_uiProgressBar.SetColor(stats.Color);
	}

	private void OnDisable()
	{
		if (_stats)
		{
			_stats.OnHealthChanged -= OnHealthChanged;
			_stats = null;
		}
	}
}