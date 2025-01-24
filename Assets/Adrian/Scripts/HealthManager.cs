using System.Collections;
using System.Collections.Generic;
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
        healthcount = PlayerPrefs.GetInt("Vidas");
        life = PlayerPrefs.GetInt("Salud");
    }
    // Update is called once per frame
    void Update()
    {
        healthBar.sprite = health[PlayerPrefs.GetInt("Salud")];
    }
}
