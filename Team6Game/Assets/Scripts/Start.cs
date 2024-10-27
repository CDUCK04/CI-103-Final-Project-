using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Colosseum");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
