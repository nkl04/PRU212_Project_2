using UnityEngine;

public class InfiniteBackground : MonoBehaviour
{
    public Transform player;
    public float scrollFactor = 0.02f;

    private Material backgroundMaterial;
    private Vector3 lastPosition;
    private Vector2 uvOffset = Vector2.zero;

    void Start()
    {
        lastPosition = player.position;
        backgroundMaterial = GetComponent<Renderer>().material;

        backgroundMaterial.SetVector("_ScrollOffset", Vector2.zero);
    }

    void Update()
    {
        Vector3 movement = player.position - lastPosition;
        lastPosition = player.position;

        if (movement.sqrMagnitude > 0.0001f)
        {
            uvOffset += new Vector2(movement.x, movement.y) * scrollFactor;
            backgroundMaterial.SetVector("_ScrollOffset", uvOffset);
        }
    }
}
