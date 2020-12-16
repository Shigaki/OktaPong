using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public Slider healthBar;
    public float health;

    private int pNum;

    void Awake()
    {
        //mHandler = GameObject.FindGameObjectWithTag("GameController").GetComponent<MatchHandler>();
        if (gameObject.CompareTag("Player1"))
        {
            pNum = 1;
            healthBar = GameObject.Find("Canvas/MatchUI/P1_HealthBar").GetComponent<Slider>();
        }
        else if (gameObject.CompareTag("Player2"))
        {
            pNum = 2;
            healthBar = GameObject.Find("Canvas/MatchUI/P2_HealthBar").GetComponent<Slider>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        health = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = health;
    }

    public void ApplyDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            MatchHandler._mHandler.OnPlayerDeath(pNum);
        }
    }
}
