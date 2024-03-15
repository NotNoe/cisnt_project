using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void QuitButton(){
        Application.Quit();
    }
    public void MainMenuButton(){
        SceneManager.LoadScene("MainMenu");
    }
    public void ResumeButton(){
        GameManager.Instance.Pause();
    }
}
