using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [Header("Components")]
    public ScreenStateController screenStateController;
    
    [Header("Variables")]
    public EScreenState screenState = EScreenState.ThreeD;

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
        screenState = state;

        screenStateController.ToggleScreenState(state);
    }
}
