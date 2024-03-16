using System;
using System.Collections;
using System.IO;
using TMPro;
using UnityEngine.Audio;

public class Paciente{
    private DialogTree dialog;
    private DialogTree current_dialog;

    public Paciente(string path) {
        StreamReader reader = new StreamReader(path);
        dialog = generaNodo(reader);
        reader.Close();
        current_dialog = dialog;
    }
    private DialogTree generaNodo(StreamReader reader){
        DialogTree salida = new DialogTree(reader.ReadLine());
        int n = int.Parse(reader.ReadLine());
        salida.init_arrays(n);
        for(int i = 0; i < n; i++){
            string[] aux = reader.ReadLine().Split("&");
            salida.respuestas[i] = aux[0];
            salida.limitaciones[i] = int.Parse(aux[1]);
            salida.tiempo[i] = int.Parse(aux[2]);
            salida.hijos[i] = generaNodo(reader);
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
    }
}