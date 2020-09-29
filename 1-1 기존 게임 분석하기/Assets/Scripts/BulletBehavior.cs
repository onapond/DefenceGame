﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour {
     

    public float speed = 10.0f;
    public GameObject character;

    public float activeTime = 3.0f;
    public float spawnTime;

    private int damage;

    public void setDamage(int input)
    {
        damage = input;
    }

    public void Spawn()
    {
        gameObject.SetActive(true);
        spawnTime = Time.time;
    }
	void Start () {
        Spawn();
	}
	
	void Update () {
        if(Time.time-spawnTime>=activeTime)
        {
            gameObject.SetActive(false);
        }
        else
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }     
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Monster")
        {
            gameObject.SetActive(false);
            other.GetComponent<MonsterStat>().attacked(damage);
        }   
    }
}
