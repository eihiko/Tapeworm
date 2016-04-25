using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreText : MonoBehaviour {

    Text score;

    // Use this for initialization
    void Start() {
        score = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Your score: " + (long)Pitpex.GetScore() + " points\n\n";
        score.text += "Is worth: " + (Pitpex.GetScore() / 1000000).ToString("C");
    }
}
