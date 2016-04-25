using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreSender : MonoBehaviour {

	public void SendScore()
    {
        Pitpex.SendScore();
        GetComponentInChildren<Text>().text = "Score Sent!";
        GetComponent<Button>().interactable = false;
    }
}
