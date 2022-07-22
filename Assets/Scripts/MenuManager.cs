using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private AudioClip buttonClick;

    public void PlayButton() {
        // not playing scene loading too fast
        AudioSource.PlayClipAtPoint(buttonClick, Camera.main.gameObject.transform.position, .5f);
        SceneManager.LoadScene(1);
    }

    public void QuitButton() {
        AudioSource.PlayClipAtPoint(buttonClick, Camera.main.gameObject.transform.position, .5f);
        SceneManager.LoadScene(0);
    }

    public void ApplicationQuitButton() {
        AudioSource.PlayClipAtPoint(buttonClick, Camera.main.gameObject.transform.position, .5f);
        Application.Quit();
    }
}
