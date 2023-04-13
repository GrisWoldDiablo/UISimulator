using UnityEngine;

public class UIMouseOverText : MonoBehaviour
{
	[SerializeField]
	private StatsScriptable _stats;

	private UIStatsHeader _statsHeader;
	private bool _pressed = false;
	
	private void OnMouseEnter()
	{
		if (!_statsHeader)
		{
			_statsHeader = UIMain.Get().GetStatsHeader(_stats);
		}
	}

	private void OnMouseExit()
	{
		if (_statsHeader)
		{
			UIMain.Get().ReturnStatsHeader(_statsHeader);
			_statsHeader = null;
		}
	}

	private void OnMouseOver()
	{
		if (!_statsHeader)
		{
			return;
		}

		_statsHeader.transform.position = Input.mousePosition;

		Debug.Log($"Fire1 {Input.GetAxis("Fire1")}");
		if (Input.GetAxis("Fire1") > 0.0f )
		{
			if (!_pressed)
			{
				_pressed = true;
				_stats.ChangeHealth(-10);
			}
		}
		else
		{
			_pressed = false;
		}
	}
}