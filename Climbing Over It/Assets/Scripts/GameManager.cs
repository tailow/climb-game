using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    #region Variables

    public GameObject player;
    public GameObject rockPrefab;
    public GameObject ground;
    public GameObject menuButtonParent;

    public Transform ladderParent;
    public Transform rockParent;

    public TMP_Text scoreText;
    public TMP_Text deathScoreText;
    public TMP_Text deathText;
    public TMP_Text newHighScoreText;
    public TMP_Text countDownText;
    public TMP_Text highScoreText;

    public float groundClimbRate;

    public List<string> deathLines;

    PlayerMovement movementScript;

    bool deathCoroutineRunning;
    bool isInMenu = true;

    #endregion

    void Start()
    {
        movementScript = player.GetComponent<PlayerMovement>();

        highScoreText.text = "Highscore: " + PlayerPrefs.GetInt("highScore").ToString();
    }

    void Update()
    {
        if (ground.transform.position.y < Camera.main.transform.position.y)
        {
            ground.transform.Translate(Vector3.up * Time.deltaTime * groundClimbRate);

            deathCoroutineRunning = false;
        }

        else if (!deathCoroutineRunning && !isInMenu)
        {
            deathCoroutineRunning = true;

            StartCoroutine("DeathCoroutine");
        }

        if (ground.transform.position.y + (ground.transform.localScale.y / 2)
        > player.transform.position.y + (player.transform.localScale.y / 2))
        {
            movementScript.isAlive = false;
        }

        scoreText.text = movementScript.score.ToString();
    }

    IEnumerator RockSpawnCoroutine()
    {
        while (movementScript.isAlive)
        {
            Transform randomLadder = ladderParent.GetChild(Random.Range(0, 3));

            Vector3 spawnPosition = new Vector3(randomLadder.position.x, player.transform.position.y + 15, 0);

            GameObject rock = Instantiate(rockPrefab, spawnPosition, Quaternion.identity);

            rock.transform.parent = rockParent;
            rock.transform.localRotation = Quaternion.Euler(Vector3.forward * Random.Range(0, 360));

            yield return new WaitForSeconds(Random.Range(0.5f, 2f));
        }
    }

    IEnumerator DeathCoroutine()
    {
        deathText.text = deathLines[Random.Range(0, deathLines.Count)];
        deathText.gameObject.SetActive(true);

        yield return new WaitForSecondsRealtime(2f);

        deathText.gameObject.SetActive(false);

        deathScoreText.text = "Score: " + movementScript.score.ToString();
        deathScoreText.gameObject.SetActive(true);

        yield return new WaitForSecondsRealtime(2f);

        deathScoreText.gameObject.SetActive(false);

        if (movementScript.score > PlayerPrefs.GetInt("highScore"))
        {
            PlayerPrefs.SetInt("highScore", movementScript.score);

            newHighScoreText.gameObject.SetActive(true);

            yield return new WaitForSecondsRealtime(2f);

            newHighScoreText.gameObject.SetActive(false);
        }

        menuButtonParent.SetActive(true);

        highScoreText.text = "Highscore: " + PlayerPrefs.GetInt("highScore").ToString();
        highScoreText.gameObject.SetActive(true);

        isInMenu = true;
    }

    IEnumerator PlayCoroutine()
    {
        movementScript.score = 0;

        player.transform.position = Vector2.zero;

        movementScript.currentSide = 0;
        movementScript.currentLadder = 1;

        int amountOfRocks = rockParent.childCount;

        for (int i = 0; i < amountOfRocks; i++)
        {
            Destroy(rockParent.GetChild(0).gameObject);
        }

        menuButtonParent.SetActive(false);
        highScoreText.gameObject.SetActive(false);

        ground.transform.position = new Vector2(0, -15);

        Camera.main.transform.position = ground.transform.position + new Vector3(0, 1, -10);
        countDownText.text = "3";
        countDownText.gameObject.SetActive(true);

        yield return new WaitForSecondsRealtime(0.5f);

        countDownText.text = "2";

        yield return new WaitForSecondsRealtime(0.5f);

        countDownText.text = "1";

        yield return new WaitForSecondsRealtime(0.5f);

        countDownText.gameObject.SetActive(false);

        movementScript.isAlive = true;

        StartCoroutine("RockSpawnCoroutine");

        isInMenu = false;
    }
}