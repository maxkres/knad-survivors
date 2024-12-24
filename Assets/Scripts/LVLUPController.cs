using UnityEngine;
using UnityEngine.UI;

public class LVLUPController : MonoBehaviour
{
    public Button speedButton;
    public Button hpButton;
    public Button attackButton;

    public int maxHPGain = 20;
    public int attackGain = 10;
    public int speedGain = 2;

    public static LVLUPController instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;

        speedButton.onClick.AddListener(speedUP);
        hpButton.onClick.AddListener(hpUP);
        attackButton.onClick.AddListener(attackUP);
        gameObject.SetActive(false);
    }

    public void lvlup() {
        gameObject.SetActive(true);
    }

    private void hpUP() {
        PlayerStats.GainMaxHP(maxHPGain);
        gameObject.SetActive(false);
    }

    private void attackUP() {
        PlayerStats.baseAttack += attackGain;
        gameObject.SetActive(false);
    }

    private void speedUP() {
        PlayerStats.speed += speedGain;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }
}