﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemyPrefab;
    public float width = 10;
    public float height = 5;
    public float speed = 5f;

    private float xmax;
    private float xmin;
    private bool movingRight = true;

    void Start() {

        float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
        Vector3 rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceToCamera));
        xmax = rightBoundary.x;
        xmin = leftBoundary.x;

        foreach (Transform child in transform)
        {
            GameObject enemy = Instantiate(enemyPrefab,
            child.transform.position,
            Quaternion.identity) as GameObject;

            enemy.transform.parent = child;
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }


    void Update() {

        if (movingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        else { 
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        float rightBoundaryOfFormation = transform.position.x + (0.5f* width);
        float leftBoundaryOfFormation = transform.position.x - (0.5f * width);
        if (leftBoundaryOfFormation < xmin )  {
            movingRight = true;
        }else if (rightBoundaryOfFormation > xmax)
        {
            movingRight = false;
        }     
    }
}
