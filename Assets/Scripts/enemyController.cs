using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody2D _rb;
    public float enemySpeed = 3.0f;
    public float horizontalMovement = 1.0f;
    public Collider2D cliffCheck;
    public Collider2D wallCheck;
    public LayerMask groundMask;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFlip();
    }

    private void FixedUpdate()
    {
        if (IsNextCliff() || IsNextWall())
        {
            horizontalMovement *= -1;
        }

        _rb.velocity = new Vector2(horizontalMovement * enemySpeed, _rb.velocity.y);
    }

    void UpdateFlip()
    {
        if (horizontalMovement < -0.005 && transform.localScale.x < 0 || horizontalMovement > 0.005 && transform.localScale.x > 0)
        {
            //flip the horizontal scale
            var scale = transform.localScale;
            scale.x = scale.x * -1;
            transform.localScale = scale;
        }
    }

    bool IsNextCliff()
    { 
      bool check = Physics2D.OverlapBox(cliffCheck.bounds.center, cliffCheck.bounds.size, 0f, groundMask);
      
      return !check;
    }

    bool IsNextWall()
    {
        bool check = Physics2D.OverlapBox(wallCheck.bounds.center, wallCheck.bounds.size, 0f, groundMask);

        return check;
    }
}
