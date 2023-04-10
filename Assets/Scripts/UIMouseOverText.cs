using System;
using UnityEngine;

public class UIMouseOverText : MonoBehaviour
{
	[SerializeField]
	private string _label;

	private UITextLabel _uiTextLabel;

	private void OnMouseEnter()
	{
		if (!_uiTextLabel)
		{
			_uiTextLabel = UIMain.Get().GetTextLabel(_label);
		}
	}

	private void OnMouseExit()
	{
		if (_uiTextLabel)
		{
			UIMain.Get().ReturnTextLabel(_uiTextLabel);
			_uiTextLabel = null;
		}
	}

	private void OnMouseOver()
	{
		if (!_uiTextLabel)
		{
			return;
		}
		
		_uiTextLabel.transform.position = Input.mousePosition;
	}
}