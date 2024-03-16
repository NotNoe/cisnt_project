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
    [SerializeField]
    private GameObject MainCanvas;
    private TMP_Text Clock;
    private int time = 540; //9am

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
        UpdateTime();
    }

    public void Pause(){
        if(Time.timeScale != 0){
            Time.timeScale = 0;
            PauseCanvas.SetActive(true);
            MainCanvas.SetActive(false);
        }else{
            Time.timeScale = 1;
            PauseCanvas.SetActive(false);
            MainCanvas.SetActive(true);
        }
    }

    public void UpdateTime(){
        int h = time / 60;
        int m = time % 60;
        Clock.text = h.ToString() + ":" + m.ToString();
        
    }
}
