using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public MainMenuController mainMenuController;
    public GameObject player;
    public DeathMenu deathMenu;
    public GameObject enemiesList;

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
        player.transform.position = playerStartPoint;
        player.SetActive(true);

        foreach(Transform child in enemiesList.transform)
        {
            child.GetComponent<BaseEnemy>().Reset();
        }
    }
}
