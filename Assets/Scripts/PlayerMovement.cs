using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float runSpeed = 3.0f;
    [SerializeField]
    float jumpForce = 3.0f;

    SpriteRenderer sprite;
    Rigidbody2D rigi;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rigi = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        if (Input.GetKey("d"))
        {
            transform.Translate(Vector2.right * runSpeed * Time.deltaTime);
            sprite.flipX = false;
        }

        if (Input.GetKey("a"))
        {
            transform.Translate(Vector2.left * runSpeed * Time.deltaTime);
            sprite.flipX = true;
        }

        if (Input.GetKey(KeyCode.Space) && Mathf.Abs(rigi.velocity.y) < 0.01f)
        {
            rigi.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}