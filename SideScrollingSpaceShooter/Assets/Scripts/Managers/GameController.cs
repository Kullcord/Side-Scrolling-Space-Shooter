using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject deathScreen;
    public GameObject pauseScreen;
    private bool done = false;
    private bool dead = false;
    private bool paused = false;
    public GameObject player;

    private void Update()
    {
        if (deathScreen == null)
            return;
        if (pauseScreen == null)
            return;
        if (player == null)
            return;

        if (PlayerController.instance.health <= 0.0f && !done)
            ActivateDeathScreen();

        if (!dead)
            if (Input.GetKeyDown(KeyCode.Escape))
                if (!paused)
                    ActivatePauseScreen(true, 0);
                else
                    ActivatePauseScreen(false, 1);
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
