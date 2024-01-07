using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject m_UIScreen;

    [SerializeField] private string m_SceneName;

    void Awake()
    {
        GameManager.instance.EventManager.Register(Constants.UI_SCREEN_SET_ACTIVE, ActivateUIScreen);
    }

    public void ActivateUIScreen(object[] param)
    {
        m_UIScreen.SetActive(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(m_SceneName);
    }
}
