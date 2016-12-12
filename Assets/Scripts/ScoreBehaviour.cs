using UnityEngine;
using System.Collections;

public class ScoreBehaviour : MonoBehaviour
{
    public GameObject player;
    int score;

    void Start()
    {
        score = player.GetComponent<Playerehaviour>().getScore();
    }
	
	// Update is called once per frame
	void Update () {
        score = player.GetComponent<Playerehaviour>().getScore();

        this.GetComponent<Canvas>().GetComponent<TextEditor>().text = score.ToString();
	}
}
