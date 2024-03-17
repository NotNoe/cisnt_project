using System;
using System.Collections;
using System.IO;
using TMPro;
using UnityEngine.Audio;

public class Paciente{
    private DialogTree dialog;
    private DialogTree current_dialog;
    private string good_ending;
    private string bad_ending;
    private bool is_good_ending;

    public Paciente(string path) {
        StreamReader reader = new StreamReader(path);
        dialog = generaNodo(reader);
        good_ending = reader.ReadLine();
        bad_ending = reader.ReadLine();
        reader.Close();
        current_dialog = dialog;
    }
    private DialogTree generaNodo(StreamReader reader){
        DialogTree salida;
        string aux1 = reader.ReadLine();

        int n = int.Parse(reader.ReadLine());
        if(n == 0){
            salida = new DialogTree(aux1.Split("&")[0]);
            if(aux1.Split("&")[1] == "0") salida.good_ending = false;
            else salida.good_ending = true;
            salida.init_arrays(1);
            salida.respuestas[0] = "Siguiente paciente";
            salida.limitaciones[0] = 0;
            salida.tiempo[0] = 0;
            salida.hijos[0] = new DialogTree("")
            {
                good_ending = salida.good_ending
            };
            salida.n = 1;
        }else{
            salida = new DialogTree(aux1);
            salida.init_arrays(n);
            for(int i = 0; i < n; i++){
                string[] aux = reader.ReadLine().Split("&");
                salida.respuestas[i] = aux[0];
                salida.limitaciones[i] = int.Parse(aux[1]);
                salida.tiempo[i] = int.Parse(aux[2]);
                salida.hijos[i] = generaNodo(reader);
            }
        }
        
        return salida;
    }

    public string getDialog(){ //Devuelve lo que dice el paciente
        return this.current_dialog.pregunta;
    }
    public bool hasEnded(){ //Devuelve si la conversacion ha terminado
        return this.current_dialog.n == 0; //Si no tiene respuestas posibles, ha terminado la conversacion
    }
    public string[] getAnswers(){ //Devuelve las posibles respuestas
        return this.current_dialog.respuestas;
    }
    public int getNAswers(){
        return this.current_dialog.n;
    }
    public int[] getLimitations(){
        return this.current_dialog.limitaciones;
    }
    public int[] getTimes(){
        return this.current_dialog.tiempo;
    }
    public void selectAnswer(int i){ //Avanza por el diálogo
        if(i < this.current_dialog.n){
            this.current_dialog = this.current_dialog.hijos[i];
        }
        if(this.current_dialog.n == 0) this.is_good_ending = this.current_dialog.good_ending;
    }
    public string getEnding(){
        return this.is_good_ending ? this.good_ending : this.bad_ending;
    }
    public bool isGoodEnding(){
        return this.is_good_ending;
    }
}