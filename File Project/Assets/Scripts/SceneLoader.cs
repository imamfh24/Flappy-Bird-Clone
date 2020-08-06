using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    [SerializeField] float waitTransition = 1f;
    [SerializeField] GameObject transisionAnimation;

    /*private void Start()
    {
        Screen.SetResolution(1080, 1920, true);
    }*/
    public void LoadScene(string name)
    {
        // Melakukan pengecekan jika name tidak null atau empty
        if (!string.IsNullOrEmpty(name))
        {
            // Membuka scene dengan nama sesuai dengan variabel name
            StartCoroutine(LoadTheScene(name));
        }
    }

    IEnumerator LoadTheScene(string name)
    {
        transisionAnimation.GetComponent<Animator>().SetTrigger("TransisiToBlack");
        yield return new WaitForSeconds(waitTransition);
        SceneManager.LoadScene(name);
    }

    IEnumerator LoadTheRestartScene()
    {
        transisionAnimation.GetComponent<Animator>().SetTrigger("TransisiToBlack");
        yield return new WaitForSeconds(waitTransition);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RestartGame()
    {
        StartCoroutine(LoadTheRestartScene());
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
