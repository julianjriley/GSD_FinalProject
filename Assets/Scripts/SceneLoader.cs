using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadSceneByName()
    {
        SceneManager.LoadScene("JulianScene");
    }

    public void QuitGame()
    {
        Debug.Log("The Game will end in the actual build");
        Application.Quit();
    }

}
