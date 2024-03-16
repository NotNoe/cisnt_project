using System.Collections;
public class DialogTree{

    public string pregunta;
    public int n;
    public string[] respuestas = {"","","",""};
    public DialogTree[] hijos = {null, null, null, null};
    public int[] limitaciones = {0,0,0,0};
    public int[] tiempo = {0,0,0,0};


    public DialogTree(string pregunta){
        this.pregunta = pregunta;
    }

}