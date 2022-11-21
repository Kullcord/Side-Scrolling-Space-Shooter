using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject deathScreen;
    private bool done = false;

    private void Update()
    {
        if (PlayerController.instance.health <= 0.0f && !done)
            ActivateDeathScreen();
    }

    private void ActivateDeathScreen()
    {
        done = true;

        PlayerController.instance.gameObject.SetActive(false);
        
        deathScreen.SetActive(true);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
       
    }
}
