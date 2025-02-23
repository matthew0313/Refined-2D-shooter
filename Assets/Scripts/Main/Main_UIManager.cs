using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_UIManager : MonoBehaviour
{
    [SerializeField] GameObject defaultTab;
    GameObject openTab;
    private void Awake()
    {
        OpenTab(defaultTab);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenTab(defaultTab);
        }
    }
    public void OpenTab(GameObject tab)
    {
        if (openTab != null) openTab.SetActive(false);
        openTab = tab;
        openTab.SetActive(true);
    }
    public void GameStart()
    {
        SceneSwitcher.Instance.SwitchScene("Game");
    }
}
