using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyShoot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Shoot(new Vector2(-5, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot(Vector2 direction)
    {
        var rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction.normalized * 5f;
    }
}
