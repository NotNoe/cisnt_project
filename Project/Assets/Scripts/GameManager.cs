using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    private System.Random rnd;
    private const int H_INI = 540; //9 a.m.
    private const int H_FIN = 1020; //5 p.m.
    [SerializeField]
    private string[] niveles;
    private Paciente[] pacientes;
    private Paciente paciente_actual;
    private int nivel_actual = 0;
    private int n_paciente = 0;
    public int nivel_jugador = 0;
    public static GameManager Instance { get; private set; }
    [SerializeField]
    private GameObject PauseCanvas;
    private GameObject MainCanvas;
    private ConversationGenerator conv;
    private TMP_Text Clock;
    private int time = 540; //9am
    [SerializeField]
    private Sprite[] sprites;
    private GameObject paciente;
    private SpriteRenderer paciente_rer;

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
        MainCanvas = GameObject.Find("GameCanvas");

        rnd = new System.Random();
        conv = MainCanvas.GetComponentInChildren<ConversationGenerator>();
        Clock = GameObject.Find("GameCanvas/Clock/Time").GetComponent<TMP_Text>();
        paciente = GameObject.Find("Patient");
        paciente.SetActive(false);
        paciente_rer = paciente.GetComponent<SpriteRenderer>();
        UpdateTime();
        CargaNivel();
        Debug.Log(niveles[0]);
    }

    void StartLvl(){
        n_paciente = 0;
        time = H_INI;
        UpdateTime();
        paciente.SetActive(true);
        SiguientePaciente();
    }
    private void SiguientePaciente(){ //Llama al siguiente y lo inicia

        if(n_paciente == pacientes.Length || time >= H_FIN){
            EndLvl();
            return;
        }

        paciente_actual = pacientes[n_paciente];
        n_paciente++;
        paciente_rer.sprite = sprites[rnd.Next(sprites.Length)];
        conv.ChangeDialog(paciente_actual.getDialog());
        conv.GenerateOptions(paciente_actual.getAnswers(), paciente_actual.getTimes(), paciente_actual.getLimitations());
    }

    public void SelectOption(int n){
        this.time += paciente_actual.getTimes()[n];
        UpdateTime();
        paciente_actual.selectAnswer(n);
        if(paciente_actual.hasEnded()){
            SiguientePaciente();
            return;
        }
        conv.ChangeDialog(paciente_actual.getDialog());
        conv.GenerateOptions(paciente_actual.getAnswers(), paciente_actual.getTimes(), paciente_actual.getLimitations());
    }
    void EndLvl(){
        paciente.SetActive(false);
        Debug.Log("Nivel terminado\n");
        nivel_actual++;
        CargaNivel();
    }

    void CargaNivel(){
        if(this.nivel_actual >= this.niveles.Length){
            TerminaJuego();
            return;
        }
        StreamReader reader = new StreamReader(niveles[nivel_actual]);
        int p = int.Parse(reader.ReadLine());
        pacientes = new Paciente[p];
        for(int i = 0; i < p; i++){
            pacientes[i] = new Paciente(reader.ReadLine());
        }
        reader.Close();
        StartLvl();
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
        Clock.text = h.ToString("D2") + ":" + m.ToString("D2");
    }

    public void TerminaJuego(){
        Debug.Log("Termino el juego\n");
        SceneManager.LoadScene("MainMenu");
        
    }
}
