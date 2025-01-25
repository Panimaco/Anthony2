using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Sprite[] health;
    public Image healthBar;
    private int healthcount;
    int life;

    // Start is called before the first frame update
    void Awake()
    {
        PlayerPrefs.SetInt("Vidas", 3);
        PlayerPrefs.SetInt("Salud", 4);
    }

    private void Start()
    {
        healthcount = PlayerPrefs.GetInt("Salud");
        life = PlayerPrefs.GetInt("Vidas");
    }
    // Update is called once per frame
    void Update()
    {
        healthBar.sprite = health[PlayerPrefs.GetInt("Salud")];
    }


    public void LoseHealth()
    {
        healthcount = PlayerPrefs.GetInt("Salud");
        if (healthcount > 1)
        {
            healthcount--;
            PlayerPrefs.SetInt("Salud", healthcount);
            PlayerPrefs.Save();
            Debug.Log("Salud: " + healthcount);
        }
        else
        {
            healthcount--;
            PlayerPrefs.SetInt("Salud", healthcount);
            PlayerPrefs.Save();
            LoseLife();
            //ResetScene();
        }
    }

    public void LoseLife()
    {
        life = PlayerPrefs.GetInt("Vidas");
        life--;
        PlayerPrefs.SetInt("Vidas", life);
        PlayerPrefs.Save();
        if (life == 0)
        {
            //GameOver();
            //ResetGame();
        }
    }
}
