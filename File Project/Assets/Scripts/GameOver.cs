using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject gameOver;
    [SerializeField] float delayGameOver = 1f;
    [SerializeField] AudioClip audioClip;

    public void GameOverActive()
    {
        StartCoroutine(SetGameOverActive());
    }

    IEnumerator SetGameOverActive()
    {
        yield return new WaitForSeconds(delayGameOver);
        gameOver.GetComponent<AudioSource>().PlayOneShot(audioClip);
        gameOver.GetComponent<Animator>().SetTrigger("ShowGameOver");
    }
}