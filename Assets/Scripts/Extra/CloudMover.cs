using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMover : MonoBehaviour
{
    [SerializeField] List<GameObject> pointsToMoveTo;

    public GameObject pointToMoveTo;

    public bool isMoving;
    public int speed;

    // Update is called once per frame
    void Update()
    {
        if(!isMoving)
        {
            var PointChosen = Random.Range(0, pointsToMoveTo.Count);
            pointToMoveTo = pointsToMoveTo[PointChosen];
            isMoving = true;
        }
        else
        {
            if (Vector3.Distance(pointToMoveTo.transform.position, gameObject.transform.position) > 0.1)
            {
                gameObject.transform.Translate((pointToMoveTo.transform.position - gameObject.transform.position).normalized * Time.deltaTime * speed);
            }
            else
                isMoving = false;
        }
    }
}
