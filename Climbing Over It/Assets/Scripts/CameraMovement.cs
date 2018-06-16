using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    #region Variables

    public GameObject player;

    public Vector3 cameraOffset;

    public float smoothAmount;

    PlayerMovement movementScript;

    #endregion

    void Start()
    {
        movementScript = player.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (movementScript.isAlive)
        {
            Vector3 desiredPos = player.transform.position + cameraOffset;
            desiredPos.z = -10;

            transform.position = Vector3.Lerp(transform.position, desiredPos,
            Time.deltaTime / smoothAmount);
        }
    }
}
