using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(GameManager_UIs))]
public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get { return instance; }
    }
    public GameManager()
    {
        if (instance != null && instance != this) Destroy(this);
        else instance = this;
    }
    GameManager_UIs UIs;
    Player player;
    private void Awake()
    {
        UIs = GetComponent<GameManager_UIs>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseToggle();
        }
    }
    public UnityEvent onGameEnd = new UnityEvent(), onGameOver = new UnityEvent();
    public void GameOver()
    {
        onGameOver.Invoke();
    }
    public void GameWin()
    {
        onGameEnd.Invoke();
    }
    public int money = 0;
    public void EarnMoney(int m_money)
    {
        money += m_money;
        UIs.ShowEarnText("+" + m_money + "$");
    }
    public void EarnMoney(int m_money, string description)
    {
        money += m_money;
        UIs.ShowEarnText(description + ":+" + m_money + "$");
    }
    public void UseMoney(int m_money, string description)
    {
        money -= m_money;
        UIs.ShowUseText(description + ":-" + m_money + "$");
    }
    bool paused = false;
    public void PauseToggle()
    {
        paused = !paused;
        Time.timeScale = (paused) ? 0 : 1;
        player.active = !paused;
        UIs.PauseMenu(paused);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
