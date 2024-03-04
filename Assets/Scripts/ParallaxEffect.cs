using UnityEngine;
using UnityEngine.Tilemaps;

public class ParallaxEffect : MonoBehaviour
{
    private Vector2 startPosition;
    public GameObject cam;
    public float parallaxFactor;

    void Start()
    {
        startPosition = transform.position;
    }

    void FixedUpdate()
    {
        Vector2 distance = cam.transform.position * parallaxFactor;
        Vector3 newPosition = startPosition + distance;
        transform.position = newPosition;
    }
}
