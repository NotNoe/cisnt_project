using System.Collections;
public class DialogTree{

    public string pregunta;
    public int n;
    public string[] respuestas;
    public DialogTree[] hijos;
    public int[] limitaciones;
    public int[] tiempo;

    public bool good_ending;


    public DialogTree(string pregunta){
        this.pregunta = pregunta;
    }
    public void init_arrays(int n){
        this.n = n;
        this.respuestas = new string[n];
        this.hijos = new DialogTree[n];
        this.limitaciones = new int[n];
        this.tiempo = new int[n];
    }

}