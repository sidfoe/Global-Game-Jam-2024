using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public InputController controller;
    public PlayerBehaviour player;

    public GameObject startPanel;
    public TextMeshProUGUI ageText;
    public TextMeshProUGUI genderText;

    public GameObject tipPanel;
    public GameObject endPanel;
    public Image scoreBar;

    public TextMeshProUGUI timerText;
    public float roundTime = 10;

    private bool gameStart = false;
    private bool tipPanelOn = false;

    public int age { get; private set; }
    public int gender { get; private set; }

    void Start()
    {
        timerText.text = roundTime.ToString();

        controller.PauseControls();

        GameSetup();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStart) //timer for game rounds
        {
            if (roundTime > 0)
            {
                roundTime -= Time.deltaTime;
                timerText.text = ((int)roundTime).ToString();
            }

            if (roundTime <= 0)
            {
                EndGame();
            }
        }

        if (!gameStart && roundTime > 0 && controller.interactInputs) //start panel use interact button to start the game but also make sure the game has stoppped
        {
            controller.PauseControls();
            startPanel.SetActive(false);
            gameStart = true;
        }

        if(controller.tipInputs) //open up tip menu
        {
            controller.PauseControls();

            if (tipPanelOn)
            {
                tipPanel.SetActive(false);
            }
            else
            {
                tipPanel.SetActive(true);
            }

            tipPanelOn = !tipPanelOn;
        }

        if(!gameStart && roundTime <= 0 && controller.interactInputs) //game is over have interact button take you back to main menu
        {
            SceneManager.LoadScene(0);
        }
    }

    public void GameSetup()
    {
        startPanel.SetActive(true);
        gender = Random.Range(0, 2);
        age = Random.Range(0, 2);

        switch(gender)
        {
            case 0: //boy
                genderText.text = "Boy Birthday Party \n Likes: Yellow";
                break;
            case 1: //girl
                genderText.text = "Girl Birthday Party \n Likes: Blue";
                break;
            case 2: //gender neutral
                genderText.text = "Gender Neutral Birthday Party \n Likes: Green";
                break;
        } 
        
        switch(age)
        {
            case 0: //5yr
                ageText.text = "For a 5 year old \n Likes: Solid";
                break;
            case 1: //7yr
                ageText.text = "For a 7 year old \n Likes: Polka Dots";
                break;
            case 2: //10yr
                ageText.text = "For a 10 year old \n Likes: Stripes";
                break;
            //case 3: //12yr
            //    ageText.text = "For a 12 year old \n Likes: Chevron";
            //    break;
        }
    }

    public void EndGame()
    {
        gameStart = false;
        controller.PauseControls();
        endPanel.SetActive(true);

        int score = player.CalculateScore(gender, age);

        scoreBar.fillAmount = score / 100f;
    }
}
