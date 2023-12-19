using Cinemachine;
using UnityEngine;
public class CameraZoom : MonoBehaviour
{
    // Movement based Scroll Wheel Zoom.
    private CinemachineVirtualCamera cam;
    public float zoom;
    public float zoomMultiplier = 4f;
    public float velocity = 0f;
    public float minZoom = 10f;
    public float maxZoom = 30f;
    public float smoothTime = 0.25f;

    private void Start()
    {
        cam = this.GetComponent<CinemachineVirtualCamera>();
        zoom = cam.m_Lens.OrthographicSize;
    }

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        zoom -= scroll * zoomMultiplier;
        zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
        cam.m_Lens.OrthographicSize = Mathf.SmoothDamp(cam.m_Lens.OrthographicSize, zoom, ref velocity, smoothTime);
    }
}