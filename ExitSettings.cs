using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitSettings : MonoBehaviour
{
public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ReloadMain()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("MainMenu"));
    }
}
