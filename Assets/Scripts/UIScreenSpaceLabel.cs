using UnityEngine;

public class UIScreenSpaceLabel : MonoBehaviour
{
	[SerializeField]
	private StatsScriptable _stats;

	[SerializeField]
	public bool IsVisible
	{
		get => _isVisible;
		set
		{
			if (_isVisible != value)
			{
				_isVisible = value;
				UpdateVisibility();
			}
		}
	}
	
	public bool ShouldRenderLabel
	{
		get => _shouldRenderLabel;
		set
		{
			if (_shouldRenderLabel != value)
			{
				_shouldRenderLabel = value;
				UpdateVisibility();
			}
		}
	}

	private bool _isVisible = false;
	private UIStatsHeader _statsHeader;
	private bool _shouldRenderLabel;
	private Renderer _renderer;
	private Camera _camera;

	private void Awake()
	{
		_camera = Camera.main;
		_renderer = gameObject.GetComponent<Renderer>();
	}

	private void OnEnable()
	{
		var uiMain = UIMain.Get();
		if (uiMain)
		{
			uiMain.RegisterScreenSpaceLabel(this);
		}
	}

	private void OnDisable()
	{
		var uiMain = UIMain.Get();
		if (uiMain)
		{
			uiMain.UnregisterScreenSpaceLabel(this);
		}
	}

// Update is called once per frame
	void Update()
	{
		if (_statsHeader)
		{
			_statsHeader.transform.position = _camera.WorldToScreenPoint(transform.position);
		}

		Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(_camera);
		ShouldRenderLabel = GeometryUtility.TestPlanesAABB(frustumPlanes, _renderer.bounds);
	}

	private void UpdateVisibility()
	{
		if (_isVisible && _shouldRenderLabel)
		{
			if (!_statsHeader)
			{
				_statsHeader = UIMain.Get().GetStatsHeader(_stats);
			}
		}
		else if ((!_isVisible || !_shouldRenderLabel) && _statsHeader)
		{
			UIMain.Get().ReturnStatsHeader(_statsHeader);
			_statsHeader = null;
		}
	}
}