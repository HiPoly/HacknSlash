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

    //moves list Game Object

    [SerializeField]
    private GameObject MovesList;

    [SerializeField]
    private GameObject Buttons;
    



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
        Buttons.SetActive(true);

    }


    // turn the pause menu off/Un-pause the game
    private void PauseMenuOff()
    {
        Time.timeScale = 1;
        thePauseMenu.SetActive(false);
        GameIsPaused = false;
        MovesList.SetActive(false);

    }



    //Menu Button behaviours

    //Unpause as a button
    public void ResumeGame()
    {
        PauseMenuOff();
    }


    //veiw Moves List
    public void MovesListOn()
    {
        MovesList.SetActive(true);
        Buttons.SetActive(false);
    }

    //stop veiwing Moves List
    public void MovesListOff()
    {
        MovesList.SetActive(false);
        Buttons.SetActive(true);
    }



    //return to title/main menu
    public void TitleScreen()
    {

        SceneManager.LoadScene(TitleScene);

    }

    








}
