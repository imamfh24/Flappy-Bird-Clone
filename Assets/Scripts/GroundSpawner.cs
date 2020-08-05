using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class GroundSpawner : MonoBehaviour
{
    [SerializeField] private Ground groundRef; // Menampung referensi ground yang ingin di buat
    private Ground prevGround; // Menampung ground sebelumnya

    GameObject groundParent;
    const string GROUNDS_PARENT_NAME = "Grounds";

    private void Start()
    {
        CreateParentGrounds();
    }

    private void CreateParentGrounds()
    {
        groundParent = GameObject.Find(GROUNDS_PARENT_NAME);
        if (!groundParent)
        {
            groundParent = new GameObject(GROUNDS_PARENT_NAME);
        }
    }

    private void SpawnGround()
    {
        //Pengecekan Null Variabel
        if(prevGround != null)
        {
            CreateGround();
        }
    }

    private void CreateGround()
    {
        // Menduplikasi Groundref
        Ground newGround = Instantiate(groundRef);

        // Jadikan game object menjadi child dari parent ground agar hierarchy tidak ramai
        newGround.transform.parent = groundParent.transform;

        // Mengaktifkan game object
        newGround.gameObject.SetActive(true);

        // Menempatkan new ground dengan posisi nextground dari prevground agar posisinya sejajar dengan ground sebelumnya
        prevGround.SetNextGround(newGround.gameObject);
    }

    //Method ini akan dipanggil ketika terdapat game object lain yang memiliki komponen collider keluar dari area collider
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Mencari komponent ground dari object yang keluar dari area trigger
        Ground ground = collision.GetComponent<Ground>();

        // Pengecekan null variabel
        if (ground)
        {
            // Mengisi variabel prevground
            prevGround = ground;

            // Membuat ground baru
            SpawnGround();
        }
    }
}
