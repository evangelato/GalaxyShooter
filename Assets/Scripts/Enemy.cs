﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;

    private Player _player;

    private Animator _anim;

    void Start() 
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null) 
        {
            Debug.LogError("Player is NULL");
        }

        _anim = GetComponent<Animator>();
        if (_anim == null)
        {
            Debug.LogError("Animator is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * _speed);

        if (transform.position.y < -5f) 
        {
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX, 7, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null) 
            {
                player.Damage();
            }
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            Destroy(this.gameObject, 2.8f);
        } 
        else if (other.tag == "Laser")
        {   
            if (_player != null) 
            {
                _player.AddScore(10);
            }       
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            Destroy(other.gameObject);
            Destroy(this.gameObject, 2.8f);
        }
    }
}
