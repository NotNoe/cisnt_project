using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private GameObject PauseCanvas;

    // Start is called before the first frame update
    void Awake()
    {
        if(Instance == null){
            Instance = this;
        }else{
            Destroy(gameObject);
        }
    }
    void Start(){
        PauseCanvas = GameObject.Find("PauseCanvas");
        PauseCanvas.SetActive(false);
    }

    public void Pause(){
        if(Time.timeScale != 0){
            Time.timeScale = 0;
            PauseCanvas.SetActive(true);
        }else{
            Time.timeScale = 1;
            PauseCanvas.SetActive(false);
        }
    }
}
