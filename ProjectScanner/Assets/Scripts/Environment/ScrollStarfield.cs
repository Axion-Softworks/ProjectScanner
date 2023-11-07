using UnityEngine;

public class ScrollStarfield : MonoBehaviour
{
    [Header("Components")]
    MeshRenderer MeshRenderer;
    Material StarfieldMaterial;
    public GameObject PlayerShip;

    [Header("Variables")]
    [Range(0.01f, 0.1f)]
    public float scrollFactor = 10f;

    public float parallaxFactor = 2f;

    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer = GetComponent<MeshRenderer>();        
        StarfieldMaterial = MeshRenderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 offset = StarfieldMaterial.mainTextureOffset;
        offset.x = transform.position.x / (transform.localScale.x * scrollFactor) / parallaxFactor;
        offset.y = transform.position.z / (transform.localScale.z * scrollFactor) / parallaxFactor;
        StarfieldMaterial.mainTextureOffset = offset;

        var positionOffset = new Vector3( 0, -9, 11);
        transform.position = PlayerShip.transform.position + positionOffset;
    }
}
