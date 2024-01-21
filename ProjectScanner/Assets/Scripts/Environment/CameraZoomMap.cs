using Cinemachine;
using UnityEngine;
public class CameraZoomMap : MonoBehaviour
{
    // Movement based Scroll Wheel Zoom.
    [Header("Components")]
    private CinemachineVirtualCamera _cam;
    private GameStateManager _gameStateManager;

    [Header("Variables")]
    public float zoom;
    public float zoomMultiplier = 4f;
    public float velocity = 0f;
    public float minZoom = 30f;
    public float maxZoom = 100f;
    public float smoothTime = 0.25f;
    public bool active;

    private void Start()
    {
        _gameStateManager = FindObjectOfType<GameStateManager>();
        SetActive();
        
        _cam = this.GetComponent<CinemachineVirtualCamera>();
        zoom = _cam.m_Lens.OrthographicSize;
    }

    void Update()
    {
        SetActive();

        if (active)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");

            if (
                (((_cam.m_Lens.OrthographicSize - minZoom) < 0.3 && (_cam.m_Lens.OrthographicSize - minZoom) > 0) 
                || ((_cam.m_Lens.OrthographicSize - minZoom) > -0.3 && (_cam.m_Lens.OrthographicSize - minZoom) < 0))
                && scroll > 0
                )
            {
                _gameStateManager.ToggleScreenState(EScreenState.ThreeD);
            }
            else if (_cam.m_Lens.OrthographicSize >= minZoom) 
            {
                zoom -= scroll * zoomMultiplier;
                zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
                _cam.m_Lens.OrthographicSize = Mathf.SmoothDamp(_cam.m_Lens.OrthographicSize, zoom, ref velocity, smoothTime); 
            }
        }
    }

    public void SetActive()
    {
        active = _gameStateManager.screenState == EScreenState.Map;
    }
}