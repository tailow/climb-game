using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    #region Variables

    public GameObject ladderParent;

    public float climbRate;
    public float sideMoveAmount;
    public float sideJumpAmount;

    public int currentSide;
    public int currentLadder = 1;

    public bool isAlive = false;

    public int score;

    float movementAmount;

    #endregion

    void Update()
    {
        if (!isAlive)
        {
            return;
        }

        movementAmount = sideMoveAmount;

        if (currentSide == 0)
        {
            movementAmount = sideMoveAmount / 2;
        }

        if (currentSide != -1 && Input.GetKeyDown(KeyCode.Q))
        {
            transform.Translate(Vector2.up * 0.1f * climbRate);
            transform.GetChild(0).Translate(Vector2.left * 0.1f * movementAmount);

            currentSide = -1;

            score++;
        }

        if (currentSide != 1 && Input.GetKeyDown(KeyCode.E))
        {
            transform.Translate(Vector3.up * 0.1f * climbRate);
            transform.GetChild(0).Translate(Vector2.right * 0.1f * movementAmount);

            currentSide = 1;

            score++;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            SwitchLadder(-1);

            transform.GetChild(0).localPosition = Vector3.zero;

            currentSide = 0;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            SwitchLadder(1);

            transform.GetChild(0).localPosition = Vector3.zero;

            currentSide = 0;
        }
    }

    void SwitchLadder(int direction)
    {
        if (direction == -1)
        {
            if (currentLadder == 1)
            {
                transform.position = new Vector2(
                ladderParent.transform.GetChild(0).position.x, transform.position.y);

                currentLadder = 0;
            }

            else if (currentLadder == 2)
            {
                transform.position = new Vector2(
                ladderParent.transform.GetChild(1).position.x, transform.position.y);

                currentLadder = 1;
            }
        }

        if (direction == 1)
        {
            if (currentLadder == 0)
            {
                transform.position = new Vector2(
                ladderParent.transform.GetChild(1).position.x, transform.position.y);

                currentLadder = 1;
            }

            else if (currentLadder == 1)
            {
                transform.position = new Vector2(
                ladderParent.transform.GetChild(2).position.x, transform.position.y);

                currentLadder = 2;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        isAlive = false;
    }
}
