using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class UITextLabel : MonoBehaviour
{
	private TMP_Text _text;

	private void Awake()
	{
		_text = GetComponent<TMP_Text>();
	}

	public void SetText(string value) => _text.SetText(value);
}