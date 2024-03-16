using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    private System.Random rnd;
    private string[] niveles =  {"Assets\\Pacientes\\level1.txt"};
    private Paciente[] pacientes;
    private Paciente paciente_actual;
    private int nivel_actual = 0;
    private int n_paciente = 0;
    public int nivel_jugador = 0;
    public static GameManager Instance { get; private set; }
    [SerializeField]
    private GameObject PauseCanvas;
    [SerializeField]
    private GameObject MainCanvas;
    [SerializeField]
    private ConversationGenerator conv;
    private TMP_Text Clock;
    private int time = 540; //9am
    [SerializeField]
    private Sprite[] sprites;
    [SerializeField]
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
        rnd = new System.Random();
        Clock = GameObject.Find("GameCanvas/Clock/Time").GetComponent<TMP_Text>();
        paciente_rer = paciente.GetComponent<SpriteRenderer>();
        UpdateTime();
        CargaNivel();
    }

    void StartLvl(){
        n_paciente = 0;
        paciente.SetActive(true);
        SiguientePaciente();
    }
    private void SiguientePaciente(){ //Llama al siguiente y lo inicia
        if(n_paciente == pacientes.Length){
            EndLvl();
            return;
        }
        paciente_actual = pacientes[n_paciente];
        n_paciente++;
        paciente_rer.sprite = sprites[rnd.Next(sprites.Length)];
        conv.ChangeDialog(paciente_actual.getDialog());
        conv.GenerateOptions(paciente_actual.getAnswers());
    }

    public void SelectOption(int n){
        paciente_actual.selectAnswer(n);
        if(paciente_actual.hasEnded()){
            SiguientePaciente();
            return;
        }
        conv.ChangeDialog(paciente_actual.getDialog());
        conv.GenerateOptions(paciente_actual.getAnswers());
    }
    void EndLvl(){
        paciente.SetActive(false);
        Debug.Log("Nivel terminado\n");
        nivel_actual++;
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
    }
}
