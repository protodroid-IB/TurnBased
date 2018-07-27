using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVisuals : MonoBehaviour
{
    private Enemy enemy;
    private SpriteRenderer enemySR;

    private Sprite idleSprite;


	// Use this for initialization
	void Start ()
    {
        enemy = GetComponent<Enemy>();
        enemySR = transform.GetChild(0).GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(idleSprite == null)
        {
            idleSprite = enemy.GetSprite();
            enemySR.sprite = idleSprite;
        }
	}
}
