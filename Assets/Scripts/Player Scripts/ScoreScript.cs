﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{

    private Text coinTextScore;
    private Text lifeTextScore;
    private AudioSource audioSource;
    private int scoreCount;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        coinTextScore = GameObject.Find("CoinText").GetComponent<Text>();

    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == MyTags.COIN_TAG)
        {
            collision.gameObject.SetActive(false);
            scoreCount++;
            coinTextScore.text = "x" + scoreCount;

            audioSource.Play();
        }
    }
}