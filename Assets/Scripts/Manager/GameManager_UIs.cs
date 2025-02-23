using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager_UIs : MonoBehaviour
{
    [SerializeField] Text MoneyText, MoneyEarnText;
    [SerializeField] Color EarnColor, UseColor;
    [SerializeField] float moneyEarnShowTime;
    [SerializeField] Animator anim;
    [SerializeField] GameObject pauseMenu;
    GameManager origin;
    private void Awake()
    {
        origin = GetComponent<GameManager>();
        origin.onGameOver.AddListener(delegate { anim.Play("GameOver"); });
    }
    private void Update()
    {
        MoneyText.text = origin.money + "$";
    }
    IEnumerator MoneyEarnShowing = null;
    float counter = 0.0f;
    public void ShowEarnText(string earnText)
    {
        MoneyEarnText.color = EarnColor;
        MoneyEarnText.gameObject.SetActive(true);
        MoneyEarnText.text = earnText;
        counter = 0.0f;
        if(MoneyEarnShowing == null)
        {
            MoneyEarnShowing = MoneyEarnShow();
            StartCoroutine(MoneyEarnShowing);
        }
    }
    public void ShowUseText(string earnText)
    {
        MoneyEarnText.color = UseColor;
        MoneyEarnText.gameObject.SetActive(true);
        MoneyEarnText.text = earnText;
        counter = 0.0f;
        if (MoneyEarnShowing == null)
        {
            MoneyEarnShowing = MoneyEarnShow();
            StartCoroutine(MoneyEarnShowing);
        }
    }
    IEnumerator MoneyEarnShow()
    {
        while(counter < moneyEarnShowTime)
        {
            counter += Time.deltaTime;
            yield return null;
        }
        MoneyEarnText.gameObject.SetActive(false);
        MoneyEarnShowing = null;
    }
    public void PauseMenu(bool active)
    {
        pauseMenu.SetActive(active);
    }
    public void PauseToggle()
    {
        origin.PauseToggle();
    }
    public void QuitGame()
    {
        origin.QuitGame();
    }
}
