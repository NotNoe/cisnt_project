using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.GameCenter;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    private System.Random rnd;

    private const int MAXLVL = 3;
    private const int H_INI = 540; //9 a.m.
    private const int H_FIN = 13*60; //1 p.m.

    public const int RED_LIMIT = 2;
    public const int YELLOW_LIMIT = 4;
    [SerializeField]
    private string[] niveles;
    private Paciente[] pacientes;
    private Paciente paciente_actual;
    private int nivel_actual = 0;
    private int n_paciente = 0;
    public int nivel_jugador = 0;
    private string[] endings;
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
    private int pacientes_terminados;
    private TMP_Text lvl_cnt;
    private TMP_Text patients_cnt;
    private Image bck_cnt;
    [SerializeField]
    private GameObject canvas_rendimiento;

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

        lvl_cnt = GameObject.Find("GameCanvas/Contador/Nivel").GetComponent<TMP_Text>();
        patients_cnt = GameObject.Find("GameCanvas/Contador/Pacientes").GetComponentInChildren<TMP_Text>();
        bck_cnt = GameObject.Find("GameCanvas/Contador/Pacientes").GetComponentInChildren<Image>();
        rnd = new System.Random();
        conv = MainCanvas.GetComponentInChildren<ConversationGenerator>();
        Clock = GameObject.Find("GameCanvas/Clock/Time").GetComponent<TMP_Text>();
        paciente = GameObject.Find("Patient");
        paciente.SetActive(false);
        paciente_rer = paciente.GetComponent<SpriteRenderer>();
        changeLvlCount(nivel_jugador);
        UpdateTime();
        CargaNivel();
    }

    void StartLvl(){
        n_paciente = 0;
        pacientes_terminados = 0;
        changePatiensCount(pacientes_terminados);
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
            pacientes_terminados++;
            changePatiensCount(pacientes_terminados);
            this.endings[n_paciente-1] = this.paciente_actual.getEnding();
            SiguientePaciente();
            return;
        }
        conv.ChangeDialog(paciente_actual.getDialog());
        conv.GenerateOptions(paciente_actual.getAnswers(), paciente_actual.getTimes(), paciente_actual.getLimitations());
    }
    void EndLvl(){
        paciente.SetActive(false);
        Debug.Log("Nivel terminado\n");
        if(pacientes_terminados < RED_LIMIT){
            if(nivel_jugador == 0){
                TerminaJuego();
            }else{
                nivel_jugador--;
            }
        }else if(pacientes_terminados >= YELLOW_LIMIT){
            nivel_jugador = Math.Min(MAXLVL, nivel_jugador + 1);
        }
        Debug.Log(endings);
        changeLvlCount(nivel_jugador);
        nivel_actual++;
        canvas_rendimiento.SetActive(true);
    }

    public void CargaNivel(){
        if(this.nivel_actual >= this.niveles.Length){
            TerminaJuego();
            return;
        }
        StreamReader reader = new StreamReader(niveles[nivel_actual]);
        int p = int.Parse(reader.ReadLine());
        endings = new string[p];
        pacientes = new Paciente[p];
        for(int i = 0; i < p; i++){
            pacientes[i] = new Paciente(reader.ReadLine());
        }
        reader.Close();
        canvas_rendimiento.SetActive(false);
        StartLvl();
    }


    public void Pause(){
        if(canvas_rendimiento.activeSelf) return;
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
        Quit();
    }
    public void Quit(){
        MainCanvas.SetActive(true);
        paciente.SetActive(true);
        PauseCanvas.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }

    public void changePatiensCount(int n){
        patients_cnt.text = n.ToString();
        if(n < GameManager.RED_LIMIT){
            bck_cnt.color = Color.red;
        }else if(n < GameManager.YELLOW_LIMIT){
            bck_cnt.color = Color.yellow;
        }else{
            bck_cnt.color = Color.green;
        }
    }
    public void changeLvlCount(int n){
        lvl_cnt.text = n.ToString();
    }
}
