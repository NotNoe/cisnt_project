using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrayScript : MonoBehaviour
{
    [SerializeField]
    private GameObject number;
    [SerializeField]
    private GameObject background;
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
        number_rer = number.GetComponent<SpriteRenderer>();
        back_rer = background.GetComponent<SpriteRenderer>();
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
