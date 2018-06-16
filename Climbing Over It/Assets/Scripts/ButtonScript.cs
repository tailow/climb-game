using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonScript : MonoBehaviour
{

    #region Variables

    public GameObject ground;
    public GameObject menuButtonParent;
    public GameObject player;

    public GameManager gameManager;

    #endregion

    public void Play()
    {
        gameManager.StartCoroutine("PlayCoroutine");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
