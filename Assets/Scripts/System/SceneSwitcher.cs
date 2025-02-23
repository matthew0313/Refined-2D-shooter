using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    private static SceneSwitcher instance;
    public static SceneSwitcher Instance
    {
        get { return instance; }
    }
    [SerializeField] Animator changeAnim;
    private void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
    bool changing = false;
    string changingScene = null;
    public void SwitchScene(string sceneName)
    {
        if (changing) return;
        changeAnim.SetBool("Switching", true);
        changingScene = sceneName;
        changing = true;
    }
    public void LoadScene()
    {
        SceneManager.LoadScene(changingScene);
        changeAnim.SetBool("Switching", false);
        changing = false;
    }
}
