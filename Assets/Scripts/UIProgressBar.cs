using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UIProgressBar : MonoBehaviour
{
	private Image _image;

	public void SetProgress(float value) => _image.fillAmount = value;
	public void SetColor(Color value) => _image.color = value;

	private void Awake()
	{
		_image = GetComponent<Image>();
	}
}