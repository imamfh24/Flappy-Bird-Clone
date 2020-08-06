using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    //Global variables
    [SerializeField] private Bird bird;
    [SerializeField] private Pipe pipeUp, pipeDown;
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private float defaultHoleSize = 1f;
    [SerializeField] private float minOffset = -3f;
    [SerializeField] private float maxOffset = 6f;
    [SerializeField] private Point point;

    //Variabel menampung coroutine yang sedang berjalan
    private Coroutine CR_Spawn;

    GameObject pipeParent;
    const string PIPE_PARENT_NAME = "Pipes";

    // Start is called before the first frame update
    void Start()
    {
        CreateParentPipe();
        StartSpawn();
    }

    private void CreateParentPipe()
    {
        pipeParent = GameObject.Find(PIPE_PARENT_NAME);
        if (!pipeParent)
        {
            pipeParent = new GameObject(PIPE_PARENT_NAME);
        }
    }

    private void StartSpawn()
    {
        if(CR_Spawn == null)
        {
            CR_Spawn = StartCoroutine(IeSpawn());
        }
    }

    IEnumerator IeSpawn()
    {
        while (true)
        {
            // Jika burung mati maka hentikan pembuatan pipa baru
            if (bird.IsDead())
            {
                StopSpawn();
            }

            // Membuat Pipa Baru
            SpawnPipe();

            // Menunggu beberapa detik sesuai dengan spawn interval
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnPipe()
    {
        CreatePipe();
    }

    private void CreatePipe()
    {

        Pipe newPipeUp = CreatePipeUp(); // Buat pipa atas
        Pipe newPipeDown = CreatePipeDown(); // Buat pipa bawah

        CreateHolePipe(newPipeUp, newPipeDown); // Buat jarak antara 2 pipa

        float y = CreatePositionPipe(newPipeUp, newPipeDown); // Buat posisi antar pipa dan dapatkan jarak antara batas atas pipa dan bawah pipa

        CreatePointObject(y); // Buat objek point antara pipa atas dan pipa bawah
    }

    private Pipe CreatePipeDown()
    {
        // Menduplikasi game object pipeDown dan menempatkan posisinya sama dengan game object
        Pipe newPipeDown = Instantiate(pipeDown, transform.position, Quaternion.identity);
        newPipeDown.transform.parent = pipeParent.transform;
        newPipeDown.gameObject.SetActive(true);
        return newPipeDown;
    }

    private Pipe CreatePipeUp()
    {
        // Menduplikasi game object pipeUp dan menempatkan posisinya sama dengan game object ini tetapi dirotasi 180 derajat
        Pipe newPipeUp = Instantiate(pipeUp, transform.position, Quaternion.Euler(0, 0, 180));
        newPipeUp.transform.parent = pipeParent.transform;
        //Mengaktifkan game object newPipeUp
        newPipeUp.gameObject.SetActive(true);
        return newPipeUp;
    }

    private float CreatePositionPipe(Pipe newPipeUp, Pipe newPipeDown)
    {
        // Menempatkan posisi pipa yang telah terbentuk agar posisinya menyesuaikan dengan fungsi sin

        float y = UnityEngine.Random.Range(minOffset, maxOffset); // Value untuk Random Posisi Pipa
        newPipeUp.transform.position += Vector3.up * y;
        newPipeDown.transform.position += Vector3.up * y;
        return y;
    }

    private void CreateHolePipe(Pipe newPipeUp, Pipe newPipeDown)
    {
        var randomHoleSize = UnityEngine.Random.Range(defaultHoleSize, defaultHoleSize + 2f); // Random Hole Size
        // Menempatkan posisi dari pipa yang sudah terbentuk agar memiliki lubang ditengahnya
        newPipeUp.transform.position += Vector3.up * (randomHoleSize / 2);
        newPipeDown.transform.position += Vector3.down * (randomHoleSize / 2);
    }

    private void CreatePointObject(float y)
    {
        // Buat Objek Point
        Point newPoint = Instantiate(point, transform.position, Quaternion.identity);
        newPoint.transform.parent = pipeParent.transform;
        newPoint.gameObject.SetActive(true);
        newPoint.SetSize(defaultHoleSize * defaultHoleSize + maxOffset);
        newPoint.transform.position += Vector3.up * y;
    }

    private void StopSpawn()
    {
        //Menghentikan coroutine IeSpawn jika sebelumnya sudah dijalankan
        if(CR_Spawn != null)
        {
            StopCoroutine(CR_Spawn);
        }
    }
}
