using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{

    #region MonoBehaviour için Singelton yapmak
    public static UIManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    [SerializeField] GameObject finishGamePanel, startGamePanel, replayGamePanel;

    public bool startGame = false;

    public void StartGame()
    {
        startGamePanel.SetActive(false);
        startGame = true;
    }

    public void NextLevel()
    {
        //Next lvl sahne değişecek
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //finishGamePanel.SetActive(false);
        finishGamePanel.SetActive(false);
        replayGamePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void OpenFinishPanel()
    {
        finishGamePanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void OpenReplayPanel()
    {
        replayGamePanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
