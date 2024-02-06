using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public MainMenuController mainMenuController;
    public GameObject player;
    public DeathMenu deathMenu;
    public GameObject enemiesList;
    public GameObject healthParent;

    private Vector3 playerStartPoint;
    private RenderHealth healthRenderer;
    private PlayerHealth playerHealth;

    void Start()
    {
        playerStartPoint = player.transform.position;
        playerHealth = player.GetComponent<PlayerHealth>();
        healthRenderer = healthParent.GetComponent<RenderHealth>();
    }

    public void GameOver()
    {
        player.SetActive(false);
        deathMenu.gameObject.SetActive(true);
    }

    public void Reset()
    {
        playerHealth.currentHealth = playerHealth.maxHealth;
        healthRenderer.RefillHearts();
        deathMenu.gameObject.SetActive(false);
        player.transform.position = playerStartPoint;
        player.SetActive(true);

        foreach(Transform child in enemiesList.transform)
        {
            child.GetComponent<BaseEnemy>().Reset();
        }
    }
}
