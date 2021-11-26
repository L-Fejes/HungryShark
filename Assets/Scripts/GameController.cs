using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityMovementAI;


public class GameController : MonoBehaviour
{
    public TMP_Text main_text;
    public TMP_Text health_text;

    private int health;
    private int score;

    public Spawner spawnerOne;
    public Spawner spawnerTwo;

    // Start is called before the first frame update
    void Start ()
    {
        StartCoroutine("Countdown");
    }

    private void Update ()
    {
        if (health <= 0)
        {
            //Quit
        }
    }



    IEnumerator Countdown ()
    {
        yield return new WaitForSeconds(1);
        main_text.text = "3";
        main_text.color = Color.red;
        yield return new WaitForSeconds(1);
        main_text.text = "2";
        main_text.color = Color.yellow;
        yield return new WaitForSeconds(1);
        main_text.text = "1";
        main_text.color = Color.green;
        yield return new WaitForSeconds(1);
        main_text.text = "0";
        yield return new WaitForSeconds(1);

        main_text.text = "Score:" + score;
        health_text.text = "Health: " + 10;

        spawnerOne.enabled = true;
        spawnerTwo.enabled = true;

    }

    public void UpdatePlayerInfo (int _health, bool _addPoints)
    {
        if (_addPoints)
        {
            spawnerTwo.SpawnNewTarget();
            score += 10;
            health = _health;
            main_text.text = "Score: " + score;
            health_text.text = "Health: " + _health;
        }
        else
        {
            spawnerOne.SpawnNewTarget();
            health = _health;
            health_text.text = "Health: " + _health;
        }
    }
}
