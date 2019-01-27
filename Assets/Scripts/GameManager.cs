using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using Enums;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    private Player player;
    private BoxCollider playerBoxCollider;

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

    public string[] levelNames;

    public eLevel currentLevel = eLevel.LEVEL_1;

    public AudioSource bgmAudioSource;

    public Image bgImage;
    public Button playButton;
    public Button retryButton;
    public Image bustedImage;

    public Image missionCompletedImage;

    public void FadeBGM(float fadeValue = 0.0f)
    {
        bgmAudioSource.DOKill();
        bgmAudioSource.DOFade(fadeValue, 1f);
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
        player = FindObjectOfType<Player>();

        playerBoxCollider = player.GetComponent<BoxCollider>();

        bgImage.gameObject.SetActive(true);
        playButton.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(false);
        bustedImage.gameObject.SetActive(false);
        missionCompletedImage.gameObject.SetActive(false);

        FadeBGM(0.04f);

        if (currentLevel == eLevel.LEVEL_2)
        {
            StartGame();

            mom.EnableVision();
            dad.EnableVision();
        }
    }

    public Bounds GetPlayerBounds()
    {
        return playerBoxCollider.bounds;
    }

    public void CaughtPlayer()
    {
        gameOver = true;

        dad.LookAtPosition(player.transform.position);
        dad.CaughtPlayer();

        mom.LookAtPosition(player.transform.position);
        mom.CaughtPlayer();

        player.LookAtPosition(dad.visionCamera.transform.position);

        player.enabled = false;

        SoundManager.Instance.PlaySound(eSoundType.SOUND_DAD_HEY, eSoundSourceType.SOUND_SOURCE_DAD,
            0.1f, 0.5f);

        SoundManager.Instance.PlaySound(eSoundType.SOUND_MOM_HEY, eSoundSourceType.SOUND_SOURCE_MOM,
            0.1f, 0.7f);

        SoundManager.Instance.PlaySound(eSoundType.SOUND_BUSTED, eSoundSourceType.SOUND_SOURCE_GENERAL,
            0.2f, 1.5f);

        Color color = Color.white;
        bgImage.gameObject.SetActive(true);
        bgImage.DOKill();
        color = bgImage.color;
        color.a = 0f;
        bgImage.color = color;
        bgImage.DOFade(0.8f, 0.3f).SetDelay(1.5f);

        bustedImage.gameObject.SetActive(true);
        bustedImage.DOKill();
        color = bustedImage.color;
        color.a = 0f;
        bustedImage.color = color;
        bustedImage.DOFade(1f, 0.3f).SetDelay(1.5f);

        retryButton.gameObject.SetActive(true);
        retryButton.image.DOKill();
        color = retryButton.image.color;
        color.a = 0f;
        retryButton.image.color = color;
        retryButton.image.DOFade(1f, 0.3f).SetDelay(1.5f);

        FadeBGM(0.01f);
    }

    public void StartGame()
    {
        gameStarted = true;
        playButton.gameObject.SetActive(false);
        bgImage.gameObject.SetActive(false);

        player.enabled = true;

        if (currentLevel == eLevel.LEVEL_1)
            FadeBGM(0.04f);
        else if (currentLevel == eLevel.LEVEL_2)
            FadeBGM(0.01f);

    }

    public void EndGame()
    {
        gameOver = true;
        bgImage.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);
    }

    public void RetryClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LevelComplete()
    {
        gameOver = true;

        currentLevel = currentLevel + 1;

        if (currentLevel < eLevel.LEVEL_TOTAL)
        {
            CancelInvoke("DelayLoadNextScene");
            Invoke("DelayLoadNextScene", 5f);

            bgImage.gameObject.SetActive(true);
            bgImage.DOKill();
            bgImage.DOFade(0f, 0.01f);
            bgImage.DOFade(0.8f, 0.3f).SetDelay(1.5f);

            missionCompletedImage.gameObject.SetActive(true);
            missionCompletedImage.DOKill();
            missionCompletedImage.DOFade(0f, 0.01f);
            missionCompletedImage.DOFade(1f, 0.3f).SetDelay(1f);
        }
        else
        {
            bgImage.gameObject.SetActive(true);
            bgImage.DOKill();
            bgImage.DOFade(0f, 0.01f);
            bgImage.DOFade(0.8f, 0.3f).SetDelay(1.5f);

            missionCompletedImage.gameObject.SetActive(true);
            missionCompletedImage.DOKill();
            missionCompletedImage.DOFade(0f, 0.01f);
            missionCompletedImage.DOFade(1f, 0.3f).SetDelay(1f);
        }
    }

    void DelayLoadNextScene()
    {
        string levelName = levelNames[(int)currentLevel];
        SceneManager.LoadScene(levelName);
    }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
}
