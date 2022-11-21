using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Header("References")]
    public GameObject player;
    public GameObject spawnController;

    public GameObject deathScreen;
    public GameObject pauseScreen;

    public GameObject tutTxt1;
    public GameObject tutTxt2;
    public GameObject tutTxt3;

    private bool done = false;
    private bool dead = false;
    private bool paused = false;
    private bool tutorialActive = true;

    private void Update()
    {
        if (deathScreen == null)
            return;
        if (pauseScreen == null)
            return;
        if (player == null)
            return;

        if (tutorialActive)
        {
            TutorialManager();
        }

        if (PlayerController.instance.health <= 0.0f && !done)
            ActivateDeathScreen();

        if (!dead)
            if (Input.GetKeyDown(KeyCode.Escape))
                if (!paused)
                    ActivatePauseScreen(true, 0);
                else
                    ActivatePauseScreen(false, 1);
    }
    private void TutorialManager()
    {
        if (tutTxt1.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A)
                || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
                TutorialTxtManager(tutTxt1, tutTxt2);
        }
        else if (tutTxt2.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
                TutorialTxtManager(tutTxt2, tutTxt3);
        }
        else if (tutTxt3.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
                TutorialTxtManager(tutTxt3, spawnController);
        }
        else
            tutorialActive = false;
    }

    private void TutorialTxtManager(GameObject objToDeactivate, GameObject objToActivate)
    {
        objToDeactivate.SetActive(false);
        objToActivate.SetActive(true);
    }

    private void ActivateDeathScreen()
    {
        done = true;

        PlayerController.instance.gameObject.SetActive(false);

        pauseScreen.SetActive(false);
        deathScreen.SetActive(true);
    }

    private void ActivatePauseScreen(bool action, int val)
    {
        paused = action;

        player.SetActive(!action);

        Time.timeScale = val;

        pauseScreen.SetActive(action);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void Resume()
    {
        ActivatePauseScreen(false, 1);
    }
    public void Menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
