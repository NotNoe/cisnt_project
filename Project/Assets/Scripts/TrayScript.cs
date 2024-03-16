using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrayScript : MonoBehaviour
{
    private SpriteRenderer number_rer;
    private SpriteRenderer back_rer;
    [SerializeField]
    private Sprite[] number_sprites;
    [SerializeField]
    private Sprite empty_tray;
    [SerializeField]
    private Sprite full_tray;
    

    private int papers = 0;
    void Start()
    {
        back_rer = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        number_rer = gameObject.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
        number_rer.sprite = number_sprites[0];
        back_rer.sprite = empty_tray;

    }
    public void ChangeNumber(int n){
        papers = n;
        number_rer.sprite = number_sprites[n];
        if(papers == 0){
            back_rer.sprite = empty_tray;
        }else{
            back_rer.sprite = full_tray;
        }
    }

    
}
