using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        StartCoroutine(LoadMainMenu());
    }

    IEnumerator LoadMainMenu()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(1);

    }
}
