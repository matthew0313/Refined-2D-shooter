using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderedTab : MonoBehaviour
{
    [SerializeField] GameObject[] tabs;
    [SerializeField] GameObject next, prev;
    int index = 0;
    private void OnEnable()
    {
        index = 0;
        tabs[index].SetActive(true);
        Check();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            NextTab();
        }
        if(Input.GetKeyDown(KeyCode.Q)) 
        {
            PrevTab();
        }
    }
    void NextTab()
    {
        if (index == tabs.Length-1) return;
        tabs[index].SetActive(false);
        tabs[++index].SetActive(true);
        Check();
    }
    void PrevTab()
    {
        if (index == 0) return;
        tabs[index].SetActive(false);
        tabs[--index].SetActive(true);
        Check();
    }
    void Check()
    {
        next.SetActive(index != tabs.Length - 1);
        prev.SetActive(index != 0);
    }
}
