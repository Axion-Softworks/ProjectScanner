using Cinemachine;
using UnityEngine;

public class ScreenStateController : MonoBehaviour
{
    [Header("Components")]
    public CinemachineVirtualCamera camMap;
    public CinemachineVirtualCamera cam3D;
    public Camera mainCamera;
    
    [Header("Variables")]
    private int _layer3D = 9;
    private int _layerMap = 10;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleScreenState(EScreenState state)
    {
        if (state == EScreenState.ThreeD)
        {
            cam3D.enabled = true;
            camMap.enabled = false;

            // Switch off layer 10, leave others as-is
            mainCamera.cullingMask = ~(1 << _layerMap);
            // Switch on layer 9, leave others as-is
            mainCamera.cullingMask |= (1 << _layer3D);

            mainCamera.GetComponent<CameraScanlineEffect>().EffectPlay(0f);
        }
        else 
        {
            cam3D.enabled = false;
            camMap.enabled = true;

            // Switch off layer 9, leave others as-is
            mainCamera.cullingMask = ~(1 << _layer3D);
            // Switch on layer 10, leave others as-is
            mainCamera.cullingMask |= (1 << _layerMap);

            mainCamera.GetComponent<CameraScanlineEffect>().EffectPlay(0.05f);
        }
    }
}
