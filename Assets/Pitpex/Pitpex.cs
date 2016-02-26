using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pitpex : MonoBehaviour {

    private static Pitpex ins;
    public double score = 0;
    public Text userText;
    public Button play;
    private PitpetsUser user;
    public string pitpetsUrl = "www.pitpets.net";
    private string usersUrl = "/api/users/";
    private string dollazUrl = "/pay/";
    private WWW www;
    private string usernameKey = "username";
    private string guidKey = "guid";
    private string dollazKey = "dollaz";
    private enum States {Login, Play, ScoreScreen, ScoreSubmitted };
    private States state = States.Login;
    private int sceneIndex = 1;


    public static bool IsScoreSent()
    {
        return (GetInstance().state == States.ScoreSubmitted);
    }

    public static double GetScore()
    {
        return  GetInstance().score;
    }

    public static void GameOver()
    {
        GetInstance().GotoScoreScreen();
    }

    public static void SendScore()
    {
        GetInstance().SubmitScore();
    }

    public static void SetScore(double score)
    {
        GetInstance().score = score;
    }

    public static void AddScore(double score)
    {
        GetInstance().score += score;
    }

    public static Pitpex GetInstance()
    {
        if (null == ins)
        {
            ins = FindObjectOfType<Pitpex>();
        }
        return ins;
    }

    void Start()
    {
        if (GameObject.FindObjectsOfType<Pitpex>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        userText.text = "Logging in...";
        play.interactable = false;
        Application.ExternalCall("PitpexLoaded");
        //DebugLogin("1b323c66-9431-4c60-9e15-f4f6f959cf42");
    }

	void Login(string guid)
    {
        userText.text = "Logging in... done.\nLoading user data...";
        www = new WWW(usersUrl + guid);
    }

    void DebugLogin(string guid)
    {
        userText.text = "retrieving user data...";
        www = new WWW(pitpetsUrl + usersUrl + guid);
    }

    void Update()
    {
        if(States.Login == state && null != www && www.isDone)
        {
            userText.text = "Logging in... done.\nLoading user data... done.\nReady to play!";
            play.interactable = true;
            JSONObject userdata = new JSONObject(www.text);
            user = new PitpetsUser();
            user.guid = userdata[guidKey].str;
            user.username = userdata[usernameKey].str;
            user.dollaz = (int)userdata[dollazKey].n;
            www.Dispose();
            www = null;
        }
        else if(States.ScoreScreen == state && null != www && www.isDone)
        {
            if(www.error != "")
            {
                state = States.ScoreSubmitted;
            }
        }
    }

    public void Play()
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void GotoScoreScreen()
    {
        SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1);
        state = States.ScoreScreen;
    }

    public void SubmitScore()
    {
        WWWForm form = new WWWForm();
        form.AddField("dollaz", ((long)score).ToString());
        www = new WWW(usersUrl + user.guid + dollazUrl, form);
    }

}
