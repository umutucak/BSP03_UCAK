using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPrey : MonoBehaviour
{
	[SerializeField] private float playerSpeed;
	// private float size;

	// private float timeNeeded = 1f;
	// private float timeElapsedLerp = 0;

	private readonly string pathFindingType = "random";

	private RandomAI randomAI;
	private GameClock gameClock;
	private List<Vector3> path;

	private bool pathGenerationGate;
	private int index;
	private int randomMoveCap;

	void Start()
	{
		gameClock = new GameClock();
		// size = GameObject.Find("Game").GetComponent<GameLoop>().size;
		randomAI = new RandomAI(1);
		index = 0;
		pathGenerationGate = false;
		randomMoveCap = 10;
	}

	void Update()
	{
		gameClock.Update(Time.time);

		switch(pathFindingType)
		{
			case "random":
				if (!pathGenerationGate)
				{
					path = randomAI.GetPath(transform.position);
				}
				pathGenerationGate = true;

				if (gameClock.Step() == gameClock.GetClockGate())
				{
					if (index < randomMoveCap)
					{
						gameClock.SetClockGate(gameClock.GetClockGate()+1);
						TeleportMovementTest(transform.position, path[index]);
						index += 1;
					}
				}
				break;
		}
	}

	public void TeleportMovementTest(Vector3 startPosition, Vector3 destination)
	{
		transform.position = destination;
	}

	// public void LerpTest(Vector3 startPosition, Vector3 destination)
	// {
	// 	timeElapsedLerp += Time.deltaTime;
	// 	float pathPercentage = timeElapsedLerp/timeNeeded;
	// 	transform.position = Vector3.Lerp(startPosition, destination, pathPercentage);
	// }
	
	// public void ManualMovement()
	// {
	// 	float x = Input.GetAxis("Horizontal");
	// 	float y = Input.GetAxis("Vertical");

	// 	Vector3 movement = new Vector3(x, y, 0);
	
	// 	transform.Translate((movement * playerSpeed) * Time.deltaTime);
	// }
}