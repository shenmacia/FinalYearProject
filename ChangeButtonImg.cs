using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeButtonImg : MonoBehaviour
{
    [SerializeField]
    private Sprite[] switchSprites;
    private Image switchImage;
    private int OnOff;
    // Start is called before the first frame update
    void Start()
    {
        OnOff = 0;
        switchImage = GetComponent<Button>().image;
        switchImage.sprite = switchSprites[OnOff];
        gameObject.GetComponent<Button>().onClick.AddListener(TurnOnAndOff);
    }

    // Update is called once per frame
    private void TurnOnAndOff()
    {
        OnOff = 1 - OnOff;
        switchImage.sprite = switchSprites[OnOff];
    }


}
