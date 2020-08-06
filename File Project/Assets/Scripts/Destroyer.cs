using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Menambahkan komponen Rigidbody2D dan BoxCollider2D
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Destroyer : MonoBehaviour
{
    //Memusnahkan object yang menyentuh ketika bersentuhan
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }
}
