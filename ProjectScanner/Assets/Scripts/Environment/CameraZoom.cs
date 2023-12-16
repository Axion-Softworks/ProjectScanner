using UnityEngine;
public class CameraZoom : MonoBehaviour
{
    // Movement based Scroll Wheel Zoom.
    private Camera cam;
    public float zoom;
    public float zoomMultiplier = 4f;
    public float velocity = 0f;
    public float minZoom = 10f;
    public float maxZoom = 30f;
    public float smoothTime = 0.25f;

    private void Start()
    {
        cam = this.GetComponent<Camera>();
        zoom = cam.orthographicSize;
    }

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        zoom -= scroll * zoomMultiplier;
        zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, zoom, ref velocity, smoothTime);
    }
}