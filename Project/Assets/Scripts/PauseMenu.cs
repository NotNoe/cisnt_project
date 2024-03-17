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
        GameManager.Instance.Quit();
    }
    public void ResumeButton(){
        GameManager.Instance.Pause();
    }
}
