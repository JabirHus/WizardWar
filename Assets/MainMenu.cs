using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  

public class MainMenu : MonoBehaviour
{
    //References 
    public GameObject difficultyPanel; //loads when play button clicked
    public GameObject instructionPanel;
    public GameObject settingsPanel;
    public Button playGame;
    public Button quitGame;
    public Button EasyButton; //to load easy gameplay
    public Button MediumButton; //to load medium gameplay
    public Button HardButton; //to load hard gameplay

    // Start is called before the first frame update
    public void StartGame()
    {
        SceneManager.LoadSceneAsync("MainMenu");

        //listeners for buttons and functions to do
        playGame.onClick.AddListener(OpenDifficultyPanel);
        quitGame.onClick.AddListener(QuitGame);
        EasyButton.onClick.AddListener(LoadEasyGame);
        MediumButton.onClick.AddListener(LoadMediumGame);
        HardButton.onClick.AddListener(LoadHardGame);
        difficultyPanel.SetActive(false); //hidden until play game button pressed
    }

    public void OpenDifficultyPanel()
    {
        difficultyPanel.SetActive(true); //hidden until play game button pressed
    }

    // method called when instruction button is called
    public void openInstructionPanel()
    {
        instructionPanel.SetActive(true);
    }

    // method called to close when "X" button is pressed
    public void closeInstructionsPanel()
    {
        instructionPanel.SetActive(false);
    }

    public void openSettingsPanel()
    {
        settingsPanel.SetActive(true);
    }

    // method called to close when "X" button is pressed
    public void closeSettingsPanel()
    {
        settingsPanel.SetActive(false);
    }

    public void LoadEasyGame()
    {
        SceneManager.LoadScene("Easy");
    }


    public void LoadMediumGame()
    {
        SceneManager.LoadScene("Medium");
    }

    public void LoadHardGame()
    {
        SceneManager.LoadScene("Hard");
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
