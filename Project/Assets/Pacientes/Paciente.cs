using System;
using System.Collections;
using System.IO;
using TMPro;
using UnityEngine.Audio;

public class Paciente{
    private DialogTree dialog;

    public Paciente(string path) {
        StreamReader reader = new StreamReader(path);

    }
    private DialogTree generaNodo(StreamReader reader){
        DialogTree salida = new DialogTree(reader.ReadLine());
        int n = int.Parse(reader.ReadLine());
        for(int i = 0; i < n; i++){
            string[] aux = reader.ReadLine().Split("&");
            salida.respuestas[i] = aux[0];
            salida.limitaciones[i] = int.Parse(aux[1]);
            salida.tiempo[i] = int.Parse(aux[2]);
            salida.hijos[i] = generaNodo(reader);
        }
        return salida;
    }

}