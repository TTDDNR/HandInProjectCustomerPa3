using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string sceneToSwitchTo;
    public void Switching()
    {
        SceneManager.LoadScene(sceneToSwitchTo);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
