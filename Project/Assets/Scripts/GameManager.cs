using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }
    [SerializeField]
    private GameObject PauseCanvas;
    private TMP_Text Clock;

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
        Clock = GameObject.Find("Canvas/Clock/Time").GetComponent<TMP_Text>();
        Clock.text = "00:00";
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

    public void ChangeTime(string time){
        Clock.text = time;
    }
}
