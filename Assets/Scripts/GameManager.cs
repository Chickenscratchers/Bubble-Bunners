using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public MainMenuController mainMenuController;
    public GameObject player;
    public DeathMenu deathMenu;

    private Vector3 playerStartPoint;

    void Start()
    {
        playerStartPoint = player.transform.position;
    }

    public void GameOver()
    {
        player.SetActive(false);
        deathMenu.gameObject.SetActive(true);
    }

    public void Reset()
    {
        deathMenu.gameObject.SetActive(false);
        
    }
}
