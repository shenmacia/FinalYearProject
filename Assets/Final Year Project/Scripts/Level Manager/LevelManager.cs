using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int health = 10;


    public int TotalHealth { get; set; }

   
    private void Start()
    {
        TotalHealth = health;
    }
    
    
    
    
    private void TakeDamage()
    {
       TotalHealth--;
        if(TotalHealth <= 0)
        {
            TotalHealth = 0;
            // Game Over
        }    
    }
    
    
    private void OnEnable()
    {
        Movement.OnEndReached += TakeDamage;
    }
    
    private void OnDisable()
    {
        Movement.OnEndReached -= TakeDamage;
    }

          
}
