using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text healthText;
    public Text coinText;
    public Text waveNumText;

    [SerializeField] public static int health = 10;
    [SerializeField] public static int coins = 200;
    [SerializeField] public static int waveNum = 1;


    void Update()
    {
        healthText.text = "Health: " + health;
        coinText.text = "Coin: " + coins;
        waveNumText.text = "Wave Number: " + waveNum;

        GameEnd();
    }


    private void GameEnd()
    {
        if(health <= 0)
        {
            //Debug.Log("Game Over");
            //Application.Quit();
        }
    }

}
