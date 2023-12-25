using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterImageSprite : MonoBehaviour
{
    private float activeTime = 0.3f;
    private float timeActivated;
    private float alpha;
    private float alphaSet = 0.8f;

    private float alphaMultiplier = 0.85f;

    private Transform player;

    private SpriteRenderer sR;

    private SpriteRenderer playerSR;

    private Color color;

    private void OnEnable() 
    {
        sR = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player").transform;

        playerSR = player.GetComponent<SpriteRenderer>();

        alpha = alphaSet;

        sR.sprite = playerSR.sprite;

        transform.position = player.position;
        transform.rotation = player.rotation;

        timeActivated = Time.time;    
    }

    private void Update() 
    {
        alpha *= alphaMultiplier;
        color = new Color(1f, 1f, 1f, alpha);

        sR.color = color;

        if (Time.time >= timeActivated + activeTime)
        {
            PlayerAfterImagePool.Instance.AddToPool(gameObject);
        }
    }
}
