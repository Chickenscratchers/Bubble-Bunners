using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderHealth : MonoBehaviour
{
    public GameObject player;
    public Sprite filledHeart;
    public Sprite emptyHeart;
    private PlayerHealth playerHealth;
    private List<GameObject> heartObjects;

    private void Start()
    {
        playerHealth = player.GetComponent<PlayerHealth>();
        heartObjects = new List<GameObject>();
        RenderSprites(30f, 25f);
    }

    private void RenderSprites(float offset, float scale)
    {
        int totalHealth = playerHealth.health;

        for (int i = 0; i < totalHealth; i++)
        {
            // create UI object, set parent
            GameObject heartObject = new GameObject("Heart" + i, typeof(Image));
            heartObject.transform.SetParent(gameObject.transform, false);

            // set position of the object
            RectTransform newHeartRectTransform = heartObject.GetComponent<RectTransform>();
            newHeartRectTransform.anchoredPosition = new Vector3(newHeartRectTransform.anchoredPosition.x + i * offset, newHeartRectTransform.anchoredPosition.y, 0);

            // set the sprite of the object
            heartObject.GetComponent<Image>().sprite = filledHeart;

            // set size of sprite
            newHeartRectTransform.sizeDelta = new Vector2(scale, scale);

            heartObjects.Add(heartObject);
        }
    }

    public void LoseHeart()
    {
        heartObjects[playerHealth.health].GetComponent<Image>().sprite = emptyHeart;
    }
}
