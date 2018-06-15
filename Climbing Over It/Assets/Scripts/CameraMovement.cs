using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    #region Variables

    public GameObject player;

    public float smoothAmount;

    #endregion

    void Update()
    {
        Vector3 desiredPos = player.transform.position;
        desiredPos.z = -10;

        transform.position = Vector3.Lerp(transform.position, desiredPos,
        Time.deltaTime / smoothAmount);
    }
}
