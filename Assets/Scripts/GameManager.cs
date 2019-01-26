using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Enums;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    private Player player;

    private Parent mom;

    private Parent dad;

    private bool gameStarted = false;
    public bool GameStarted
    {
        get { return gameStarted; }
    }

    private bool gameOver = false;
    public bool GameOver
    {
        get { return gameOver; }
    }

    public AudioSource bgmAudioSource;

    public  Image       bgImage;
    public  Button      playButton;
    public  Button      retryButton;

    public void FadeBGM(float fadeValue = 0.0f)
    {

    }

    public void Caught(eEntityType entityType)
    {

    }

    void Start()
    {
        InitLevel();
    }

    public void InitLevel()
    {
        bgmAudioSource = GetComponent<AudioSource>();

        mom = GameObject.FindGameObjectWithTag("Mom").GetComponent<Parent>();
        dad = GameObject.FindGameObjectWithTag("Dad").GetComponent<Parent>();

        bgImage.gameObject.SetActive(true);
        playButton.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        gameStarted = true;
        playButton.gameObject.SetActive(false);
        bgImage.gameObject.SetActive(false);
    }

    public void EndGame()
    {
        gameOver = true;
        bgImage.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);
    }

    public void CountDownToStart()
    {

    }

    public void RetryClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LevelComplete()
    {

    }

    void Awake()
    {
        DontDestroyOnLoad(this);

        if (Instance == null)
            Instance = this;
        else
            GameObject.DestroyImmediate(this.gameObject);
    }
}
