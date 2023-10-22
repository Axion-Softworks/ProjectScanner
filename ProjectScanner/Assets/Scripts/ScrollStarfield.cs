using UnityEngine;

public class ScrollStarfield : MonoBehaviour
{
    [Header("Components")]
    MeshRenderer meshRenderer;
    Material starfieldMaterial;

    [Header("Variables")]
    [Range(0.01f, 0.1f)]
    public float scrollFactor = 10f;

    public float parallaxFactor = 2f;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();        
        starfieldMaterial = meshRenderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 offset = starfieldMaterial.mainTextureOffset;
        offset.x = transform.position.x / (transform.localScale.x * scrollFactor) / parallaxFactor;
        offset.y = transform.position.z / (transform.localScale.z * scrollFactor) / parallaxFactor;
        starfieldMaterial.mainTextureOffset = offset;
    }
}
