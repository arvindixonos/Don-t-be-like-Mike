using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class GameManager : MonoBehaviour
{
	public	static	GameManager	Instance = null;

    public Player player;

    public Parent mom;

    public Parent dad;

    public bool gameStarted = false;

    public bool gameOver = false;

	public	AudioSource		bgmAudioSource;

	public	void FadeBGM(float fadeValue = 0.0f)
	{
		
	}

    public void Caught(eEntityType entityType)
    {

    }

    public void InitLevel()
    {

    }

    public void StartGame()
    {

    }

    public void EndGame()
    {

    }

    public void CountDownToStart()
    {

    }

    public void RestartClicked()
    {

    }

    public void PlayClicked()
    {

    }

    public void LevelComplete()
    {

    }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void OnDestroy()
    {
        Instance = null;
    }
}
