using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Contador : MonoBehaviour
{
    // Start is called before the first frame update
    private TextMeshPro lvl;
    private TextMeshPro pacientes;
    private Image bg;
    void Start()
    {
        lvl = gameObject.transform.Find("Nivel").GetComponent<TextMeshPro>();
        pacientes = gameObject.transform.Find("Pacientes").GetComponentInChildren<TextMeshPro>();
        bg = gameObject.transform.Find("Pacientes").GetComponentInChildren<Image>();
    }

    // Update is called once per frame
    
    public void changeLvl(int n){
        lvl.text = n.ToString();
    }
    public void changePatiens(int n){
        pacientes.text = n.ToString();
        if(n < GameManager.RED_LIMIT){
            bg.color = Color.red;
        }else if(n < GameManager.YELLOW_LIMIT){
            bg.color = Color.yellow;
        }else{
            bg.color = Color.green;
        }
    }

}
