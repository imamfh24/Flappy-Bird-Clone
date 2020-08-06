using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Bird : MonoBehaviour
{
    // Global Variabels Config
    [Header("Projectile")]
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject gun;
    [SerializeField] float delayFire = 5f;
    [SerializeField] bool isShoot = true;

    [Header("Score")]
    [SerializeField] private int score = 0;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text finalScoreText;

    [Header("Body")]
    [SerializeField] private float upForce = 5f;
    [SerializeField] private float gravityBird = 4f;
    [SerializeField] private bool isPlaying;
    [SerializeField] private bool isDead;

    [Header("Other")]
    [SerializeField] private GameObject tapPanel;
    [SerializeField] private GameObject thePipes;

    [Header("Event")]
    [SerializeField] private UnityEvent OnJump, OnDead;
    [SerializeField] private UnityEvent OnAddPoint;

    private float timer = 0;
    private Rigidbody2D rigidBody2d;
    private Animator animator;
    private Vector2 rotationPosition;

    private void Start()
    {
        // Mendapatkan komponen ketika game baru berjalan
        rigidBody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        scoreText.text = score.ToString();
        finalScoreText.text = score.ToString();
        isPlaying = false;
        thePipes.SetActive(false);
        rigidBody2d.gravityScale = 0f;
    }

    private void Update()
    {
        
        if (!isPlaying)
        {
            TapFirstPlay();
            return;
        }
        ClickForJump();
        if (isShoot && !isDead)
        {
            timer += Time.deltaTime;
            if (timer > delayFire)
            {
                FireProjectile();
                timer = 0;
            }
        }
        
    }

    private void ClickForJump()
    {
        // Melakukan pengecekan jika belum mati dan klik kiri pada mouse
        if (!isDead && Input.GetMouseButtonDown(0))
        {
            //Burung Meloncat
            Jump();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Menghentikan animasi burung ketika bersentuhan dengan objek lain
        animator.enabled = false;
    }
    private void Jump()
    {
        // Mengecek rigidbody null atau tidak
        if (rigidBody2d)
        {
            //Menghentikan kecepatan burung ketika jatuh
            rigidBody2d.velocity = Vector2.zero;

            //Menambahkan gaya ke arah sumbu y
            rigidBody2d.velocity = new Vector2(0f, upForce);
        }

        // Pengecekan Null Variabel
        if(OnJump != null)
        {
            // Menjalankan semua event OnJump event
            OnJump.Invoke();
        }
    }

    public void Dead() // Membuat burung mati
    {
        // Pengecekan jika belum mati dan value OnDead tidak sama dengan Null
        if(!isDead && OnDead != null)
        {
            // Memanggil semua event pada OnDead
            OnDead.Invoke();
        }

        // Set variabel dead menjadi true
        isDead = true;
    }

    public bool IsDead()
    {
        return isDead;
    }

    public void AddScore(int value)
    {
        // Menambahkan Score Value
        score += value;
        scoreText.text = score.ToString();
        finalScoreText.text = score.ToString();

        // Pengecekan Null Value
        if (OnAddPoint != null)
        {
            // Memanggil semua event pada OnAddPoint
            OnAddPoint.Invoke();
        }
    }

    public void TapFirstPlay()
    {
        if (!isDead && Input.GetMouseButtonDown(0))
        {
            if (rigidBody2d)
            {
                tapPanel.GetComponent<Animator>().SetTrigger("IsPlay");
                thePipes.SetActive(true);
                isPlaying = true;
                rigidBody2d.gravityScale = gravityBird;

                //Menambahkan gaya ke arah sumbu y
                rigidBody2d.velocity = new Vector2(0f, upForce);
            }
            // Pengecekan Null Variabel
            if (OnJump != null)
            {
                // Menjalankan semua event OnJump event
                OnJump.Invoke();
            }
        }
    }

    public void FireProjectile()
    {
        GameObject newProjectile = Instantiate(
            projectile,
            gun.transform.position,
            gun.transform.rotation);
    }
}
