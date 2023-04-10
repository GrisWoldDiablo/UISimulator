using UnityEngine;

public class UIScreenSpaceLabel : MonoBehaviour
{
	[SerializeField]
	private bool _isVisible = false;

	[SerializeField]
	private string _label;

	private UITextLabel _textLabel;

	// Update is called once per frame
	void Update()
	{
		if (_isVisible)
		{
			if (!_textLabel)
			{
				_textLabel = UIMain.Get().GetTextLabel(_label);
			}

			_textLabel.transform.position = Camera.main.WorldToScreenPoint(transform.position);
		}
		else if (!_isVisible && _textLabel)
		{
			UIMain.Get().ReturnTextLabel(_textLabel);
			_textLabel = null;
		}
	}
}