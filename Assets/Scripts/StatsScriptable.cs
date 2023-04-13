using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "Custom/StatsScriptable")]
public class StatsScriptable : ScriptableObject
{
	public string Name = "Unnamed";
	[Min(0.0f)]
	public float MaxHealth;
	public Color Color;

	private float _currentHealth;
	
	public float HealthPercentage => _currentHealth / MaxHealth;

	public event Action<StatsScriptable> OnHealthChanged;
	
	private void OnEnable()
	{
		_currentHealth = MaxHealth;
	}

	public void ChangeHealth(int value)
	{
		_currentHealth += value;
		_currentHealth = Math.Clamp(_currentHealth, 0.0f, MaxHealth);
		OnHealthChanged?.Invoke(this);
	}
}
