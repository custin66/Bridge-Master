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

    public void StartGame()
    {
        startGamePanel.SetActive(false);
        
    }

    public void NextLevel()
    {
        //Next lvl sahne değişecek
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //finishGamePanel.SetActive(false);
    }

    public void OpenFinishPanel()
    {
        finishGamePanel.SetActive(true);
    }
    public void OpenReplayPanel()
    {
        replayGamePanel.SetActive(true);
    }
    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
