using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAI
{
    private Transform hunterPrefab;
    private Transform preyPrefab;
    private System.Random random;
    // private float size;

    public RandomAI(int seed)
    {
        this.random = new System.Random(seed);
        // this.size = GameObject.Find("Game").GetComponent<GameLoop>().size;
    }

    public List<Vector3> GetPath(Vector3 startingPosition)
    {
        List<Vector3> randomPath = new List<Vector3>();

        for (int i = 0; i < 10; i++)
        {
            randomPath.Add(GetDestination(startingPosition));
            startingPosition = randomPath[i];
        }

        return randomPath;
    }

    public Vector3 GetDestination(Vector3 startingPosition)
    {
        float rnd = random.Next(1, 5);

        if (rnd == 1)
        {
            return new Vector3(startingPosition.x + 1, startingPosition.y, 0);
        }
        else if (rnd == 2)
        {
            return new Vector3(startingPosition.x - 1, startingPosition.y, 0);
        }
        else if (rnd == 3)
        {
            return new Vector3(startingPosition.x, startingPosition.y + 1 , 0);
        }
        else
        {
            return new Vector3(startingPosition.x, startingPosition.y - 1 , 0);
        }
    }
}
