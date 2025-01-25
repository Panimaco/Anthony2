using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    //Array de sprites de la barra de salud
    public Sprite[] health;
    //Imagen de la barra de salud
    public Image healthBar;
    //Numero de salud actual
    private int healthcount;
    //Numero de vida actual
    int life;

    // Start is called before the first frame update
    void Awake()
    {
        //Numero de vidas y salud iniciales
        PlayerPrefs.SetInt("Vidas", 3);
        PlayerPrefs.SetInt("Salud", 4);
    }

    private void Start()
    {
        //Numero de vidas y salud al iniciar la escena
        healthcount = PlayerPrefs.GetInt("Salud");
        life = PlayerPrefs.GetInt("Vidas");
    }
    // Update is called once per frame
    void Update()
    {
        //Sprite actual de la barra de salud
        healthBar.sprite = health[PlayerPrefs.GetInt("Salud")];
    }

    //Método de perder salud
    public void LoseHealth()
    {
        //Salud  actual
        healthcount = PlayerPrefs.GetInt("Salud");
        //Perder salud con más de 1 punto de salud
        if (healthcount > 1)
        {
            healthcount--;
            PlayerPrefs.SetInt("Salud", healthcount);
            PlayerPrefs.Save();
            Debug.Log("Salud: " + healthcount);
        }
        //En caso de que tengas 1 punto de salud
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
        if (life > 0)
        {
            PlayerPrefs.SetInt("Salud", 4);
            PlayerPrefs.Save();
        }
        else
        {
            //GameOver()
            ResetRun();
        }
    }

    public void ResetRun()
    {
        PlayerPrefs.SetInt("Vidas", 3);
        PlayerPrefs.SetInt("Salud", 4);
        PlayerPrefs.Save();
        //LoadSceneByIndex
    }
}
