using Cinemachine;
using UnityEngine;
public class CameraZoom3D : MonoBehaviour
{
    // Movement based Scroll Wheel Zoom.
    [Header("Components")]
    private CinemachineVirtualCamera _cam;
    private GameStateController _gameStateController;

    [Header("Variables")]
    public float zoom;
    public float zoomMultiplier = 4f;
    public float velocity = 0f;
    public float minZoom = 10f;
    public float maxZoom = 30f;
    public float smoothTime = 0.25f;
    public bool active;

    private void Start()
    {
        _gameStateController = FindObjectOfType<GameStateController>();
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
                (((_cam.m_Lens.OrthographicSize - maxZoom) < 0.3 && (_cam.m_Lens.OrthographicSize - maxZoom) > 0) 
                || ((_cam.m_Lens.OrthographicSize - maxZoom) > -0.3 && (_cam.m_Lens.OrthographicSize - maxZoom) < 0))
                && scroll < 0
                )
            { 
                _gameStateController.ToggleScreenState(EScreenState.Map);
            }
            else
            {
                zoom -= scroll * zoomMultiplier;
                zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
                _cam.m_Lens.OrthographicSize = Mathf.SmoothDamp(_cam.m_Lens.OrthographicSize, zoom, ref velocity, smoothTime); 
            }
        }
    }

    public void SetActive()
    {
        active = _gameStateController.screenState == EScreenState.ThreeD;
    }
}