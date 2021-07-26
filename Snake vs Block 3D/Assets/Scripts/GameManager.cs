using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    private int _score = 0;

    public TextMeshProUGUI scoreText;


    public GameObject levelCompletedMenu;

    public GameObject gameOverMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore()
    {
        _score++;
        UpdateScoreText();
    }

    public void UpdateScoreText()
    {
        scoreText.SetText(_score.ToString());
    }

    public void CompleteLevel()
    {
        levelCompletedMenu.SetActive(true);
    }

    public void GameOver()
    {
        gameOverMenu.SetActive(true);
    }


}
