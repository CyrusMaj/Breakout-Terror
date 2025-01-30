using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject brickPrefab;
    [SerializeField] Vector3 startPos;
    [SerializeField] int gridWidth = 8;
    [SerializeField] int gridHeight = 5;

    /*
    [SerializeField]
    GameObject wallPrefab;
    [SerializeField]
    Vector3 spawnPos;
    [SerializeField]
    int wallWidth = 1;
    [SerializeField]
    int wallHeight = 20;
    */

    void Start()
    {
        Vector2 brickSize = brickPrefab.GetComponent<BoxCollider2D>().size;

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Instantiate(brickPrefab, startPos + new Vector3(brickSize.x * x, brickSize.y / 2f * y, 0f), Quaternion.identity);
            }
        }

/*
        Vector2 wallSize = wallPrefab.GetComponent<BoxCollider2D>().size;

        for (int x = 0; x < wallWidth; x++)
        {
            for (int y = 0; y < wallHeight; y++)
            {
                Instantiate(wallPrefab, spawnPos + new Vector3(wallSize.x * x, wallSize.y / 2f * y, 0f), Quaternion.identity);
            }
        }

*/
    }

	void Update()
	{
		if(Input.GetKeyDown("escape"))
			{
				SceneManager.LoadScene("MainMenu");
				Debug.Log ("Main Menu");
			}
	}
}
