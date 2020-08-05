using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 5f;

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var pipe = collision.gameObject.GetComponent<Pipe>();
        if (pipe != null)
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
