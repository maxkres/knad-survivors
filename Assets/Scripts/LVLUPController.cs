using UnityEngine;
using UnityEngine.UI;

public class LVLUPController : MonoBehaviour
{
    public Button speedButton;
    public Button hpButton;
    public Button attackButton;

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
        PlayerStats.GainMaxHP(20);
        gameObject.SetActive(false);
    }

    private void attackUP() {
        PlayerStats.baseAttack += 10;
        gameObject.SetActive(false);
    }

    private void speedUP() {
        PlayerStats.speed += 1;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
