using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    //Global Variabel
    [SerializeField] private Bird bird;
    [SerializeField] private float speed = 1;

    private void Update()
    {
        //Melakukan pengecekan jika burung belum mati
        if (!bird.IsDead())
        {
            // Membuat pipa bergerak kesebalh kiri dengan kecepatan tertentu
            transform.Translate(Vector2.left * speed * Time.deltaTime, Space.World);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Bird bird = collision.gameObject.GetComponent<Bird>();

        if (bird)
        {
            // Mendapatkan komponent collider pada game object
            Collider2D collider = GetComponent<Collider2D>();

            // Melakukan pengecekan null variabel atau tidak
            if (collider)
            {
                // Menonaktifkan collider
                collider.enabled = false;
            }
            bird.Dead();
        }
    }
}
