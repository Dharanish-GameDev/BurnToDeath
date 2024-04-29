using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DamagingEnemy : MonoBehaviour
{
    public EnemyBoss boss;
    public EnemyHealthBar healthBar; 
    public PlayerController controller;
    [SerializeField] GameObject[] _karins;
    [SerializeField] GameObject[] _wildBoars;
    // 1- FireBallJutsu  , 2- Forward slice, 3 - running slice 

    private void Update()
    {
        //attackInt = controller.attackFind;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag($"Enemy"))
        {
            if (controller.attackFind == 1)
            {
                boss.hitPoints-=30;
            }
            else if(controller.attackFind==2)
            {
                boss.hitPoints -= 4;
            }
            else if(controller.attackFind==3)
            {
                boss.hitPoints -= 5;
            }
            healthBar.EnemyHeathBar(boss.hitPoints,boss.maxHitPoints);
        }
        for(int i =0;i<_karins.Length;i++)
        {
            if (other.gameObject == _karins[i])
            { // 20 , 5 , 7
                GameObject temp = _karins[i];
                switch (controller.attackFind)
                {
                    case 1:
                        temp.GetComponent<Karin>().KarinHP -= 35 ;
                        break;
                    case 2:
                        temp.GetComponent<Karin>().KarinHP -= 5;
                        break;
                    case 3:
                        temp.GetComponent<Karin>().KarinHP -= 7;
                        break;
                }
            }
        }
        for (int i =0; i<_wildBoars.Length;i++)
        {
            if(other.gameObject == _wildBoars[i])
            {
                GameObject temp = _wildBoars[i];
                switch (controller.attackFind)
                {
                    case 1:
                        {
                            temp.GetComponent<WildBoar>().boarHp = 0;
                            break;
                        }
                    case 2:
                        {
                            temp.GetComponent<WildBoar>().boarHp -= 5;
                            break;
                        }
                       
                    case 3:
                        {
                            temp.GetComponent<WildBoar>().boarHp -= 8;
                            break;
                        }
                        
                }
            }
            
        }
        
    }

    // array of karins 
    // check 
}
