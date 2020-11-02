using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //pause menu direct things
    [SerializeField]
    private GameObject thePauseMenu;
    [SerializeField]
    private KeyCode PauseTheGame;

    private bool GameIsPaused;


    //scene Loading variables

    [SerializeField]
    private string TitleScene;



    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(PauseTheGame))
        {

            if (GameIsPaused)
            {
                PauseMenuOff();
            }
            else
            {
                PauseMenuOn();
            }


        }

    }

    //turn pause menu on/pause the game
    private void PauseMenuOn()
    {

        Time.timeScale = 0;
        thePauseMenu.SetActive(true);
        GameIsPaused = true;

    }


    // turn the pause menu off/Un-pause the game
    private void PauseMenuOff()
    {
        Time.timeScale = 1;
        thePauseMenu.SetActive(false);
        GameIsPaused = false;

    }



    //Menu Button behaviours
    public void ResumeGame()
    {
        PauseMenuOff();
    }





    public void TitleScreen()
    {

        SceneManager.LoadScene(TitleScene);

    }

    








}
