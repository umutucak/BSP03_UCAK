using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    public int width;
    public int height;
    public float size;
    [SerializeField] private Transform wallPrefab;
    [SerializeField] private Transform preyPrefab;
    [SerializeField] private Transform hunterPrefab;
    private Transform hunter;
    private Transform prey;

    private MazeGenerator.Cell[,] maze;

    void Start()
    {
        maze = MazeGenerator.Generate(width, height);
        MazeRenderer();
        PlayerRenderer();
    }

    void Update()
    {
        if (CheckWin())
            Debug.Log("Hunter wins!");
    }

    public void MazeRenderer()
    {
        float wallThickness = 0.04493808f * size;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                MazeGenerator.Cell cell = maze[i,j];
                Vector3 position = new Vector3(i*size - width*size/2 + size/2, j*size - height*size/2 + size/2, 0);

                if (cell.HasFlag(MazeGenerator.Cell.UP))
                {
                    Transform topWall = Instantiate(wallPrefab, transform);  //place wall on the game scene
                    topWall.position = position + new Vector3(0, size/2, 0); //choose the location of the wall
                    topWall.localScale = new Vector3(wallThickness, size, 0); //the size of the wall
                    topWall.eulerAngles = new Vector3(0, 0, 90); //flip the horizontal walls
                }
                if (cell.HasFlag(MazeGenerator.Cell.LEFT))
                {
                    Transform leftWall = Instantiate(wallPrefab, transform); //place wall on the game scene
                    leftWall.position = position + new Vector3(-size/2, 0, 0); //choose the location of the wall
                    leftWall.localScale = new Vector3(wallThickness, size, 0); //the size of the wall
                }

                if (i == width - 1) //for the very far right of the grid, so that there arent multiple walls when only 1 is needed.
                {
                    if (cell.HasFlag(MazeGenerator.Cell.RIGHT))
                    {
                        Transform rightWall = Instantiate(wallPrefab, transform); //place wall on the game scene
                        rightWall.position = position + new Vector3(size/2, 0, 0); //choose the location of the wall
                        rightWall.localScale = new Vector3(wallThickness, size, 0); //the size of the wall
                    }
                }
                if (j == 0)
                {
                    if (cell.HasFlag(MazeGenerator.Cell.DOWN)) //for the very bottom of the grid, so that there arent multiple walls when only 1 is needed.
                    {
                        Transform bottomWall = Instantiate(wallPrefab, transform); //place wall on the game scene
                        bottomWall.position = position + new Vector3(0, -size/2, 0); //choose the location of the wall
                        bottomWall.localScale = new Vector3(wallThickness, size, 0); //the size of the wall
                        bottomWall.eulerAngles = new Vector3(0, 0, 90); //flip the horizontal walls
                    }
                }
            }
        }
    }

    public void PlayerRenderer()
    {
        System.Random random = new System.Random();
        //float playerSize = 0.2378656f * size;
        prey = Instantiate(preyPrefab, transform);
        prey.position = new Vector3(random.Next(0, 0), random.Next(0, 0), 0);
        hunter = Instantiate(hunterPrefab, transform);
        hunter.position = new Vector3(random.Next(1, 1), random.Next(0, 0), 0);
    }

    public bool CheckWin()
    {
        return (prey.position == new Vector3(hunter.position.x+1, hunter.position.y) || 
                prey.position == new Vector3(hunter.position.x-1, hunter.position.y) || 
                prey.position == new Vector3(hunter.position.x, hunter.position.y+1) || 
                prey.position == new Vector3(hunter.position.x, hunter.position.y-1));
    }
    
    public int GetMazeWidth()
    {
        return width;
    }

    public int GetMazeHeight()
    {
        return height;
    }
}
