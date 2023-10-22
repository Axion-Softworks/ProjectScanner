using UnityEditor;
using UnityEngine;

public class BasicShipControl : MonoBehaviour
{
    [Header("Variables")]
    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(
            Input.GetAxis("Horizontal") * speed,
            0,
            Input.GetAxis("Vertical") * speed
        ) * Time.deltaTime);
    }
}
