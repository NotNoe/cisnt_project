using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConversationGenerator : MonoBehaviour
{
    private TMP_Text _dialog;
    private TMP_Text option1;
    private TMP_Text option2;
    private TMP_Text option3;
    private TMP_Text option4;
    private GameObject Button1;
    private GameObject Button2;
    private GameObject Button3;
    private GameObject Button4;



    private double time;
    private TMP_Text[] options = { null, null, null, null };
    private GameObject[] buttons = { null, null, null, null };
    // Start is called before the first frame update
    void Start()
    {
        _dialog = GetComponentInChildren<TMP_Text>();
        option1 = GameObject.Find("Option1").GetComponent<TMP_Text>();
        option2 = GameObject.Find("Option2").GetComponent<TMP_Text>();
        option3 = GameObject.Find("Option3").GetComponent<TMP_Text>();
        option4 = GameObject.Find("Option4").GetComponent<TMP_Text>();
        Button1 = GameObject.Find("Button 1");
        Button2 = GameObject.Find("Button 2");
        Button3 = GameObject.Find("Button 3");
        Button4 = GameObject.Find("Button 4");

        buttons[0] = Button1;
        buttons[1] = Button2;
        buttons[2] = Button3;
        buttons[3] = Button4;

        options[0] = option1;
        options[1] = option2;
        options[2] = option3;
        options[3] = option4;
        

    }

    // Update is called once per frame
    void Update()
    {
 
    }
    public void ChangeDialog(string dialog)
    {
        _dialog.text = dialog;
    }
    public void GenerateOptions(string[] treatments, int[] time, int[] level)
    {
        for(int i = 0; i < 4; i++){
            buttons[i].SetActive(false);
        }
        for(int i = 0; i < treatments.Length; i++)
        {
            options[i].text = treatments[i] + "          " + time[i] + " minutes" + "             " + level[i];
            buttons[i].SetActive(true);
            if (level[i] > GameManager.Instance.nivel_jugador)
            {
                buttons[i].GetComponent<Button>().interactable = false;
            }
        }
    }
    public void OnButton1()
    {
        GameManager.Instance.SelectOption(0);
    } 
    public void OnButton2()
    {
        GameManager.Instance.SelectOption(1);
    }
    public void OnButton3()
    {
        GameManager.Instance.SelectOption(2);
    }
    public void OnButton4()
    {
        GameManager.Instance.SelectOption(3);
    }


}
