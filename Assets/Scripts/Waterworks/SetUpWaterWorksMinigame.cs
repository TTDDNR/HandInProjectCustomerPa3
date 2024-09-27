using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpWaterWorksMinigame : MonoBehaviour
{
    [SerializeField] List<GameObject> PossiblePieces;
    [SerializeField] GameObject StartPiece;
    [SerializeField] GameObject EndPiece;

    [SerializeField] List<GameObject> Game;

    public bool gameStarted = false;

    public void Start()
    {
    }

    public void StartMiniGame()
    {
        if (!gameStarted)
        {
            gameStarted = true;
            gameObject.SetActive(true);
            if (Game.Count > 0)
            {
                Game.Clear();
                var Children = FindObjectsOfType<SquareBehaviour>();
                foreach (SquareBehaviour child in Children)
                {
                    Destroy(child.gameObject);
                }

            }

            Game.Add(StartPiece);

            for (int i = 0; i < 7; i++)
            {
                var chosen = Random.Range(0, PossiblePieces.Count);
                Game.Add(PossiblePieces[chosen]);
            }

            Game.Add(EndPiece);

            foreach (GameObject piece in Game)
            {
                Instantiate(piece, parent: gameObject.transform);
            }
        }
    }

    public void EndGame()
    {
        gameObject.SetActive(false);
        FindAnyObjectByType<PlayerMovementScript>().playingMinigame = false;
        gameStarted = false;
    }
}
