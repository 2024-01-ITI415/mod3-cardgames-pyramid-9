using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
    public static Scoreboard S;

    [Header("Set in Inspector")]
    public GameObject prefabFloatingScore;

    [Header("Set Dynamically")]
    [SerializeField] private int _score = 0;
    [SerializeField] private string _scoreString;
    private Transform canvasTrans;

    public int score
    {
        get { return _score; }
        set
        {
            _score = value;
            scoreString = _score.ToString("N0");
        }
    }

    public string scoreString
    {
        get { return _scoreString; }
        set
        {
            _scoreString = value;
            GetComponent<Text>().text = _scoreString;
        }
    }

    void Awake()
    {
        if (S == null)
        {
            S = this;
        }
        else
        {
            Debug.LogError("ERROR: Scoreboard.Awake(): S is already set!");
            Destroy(gameObject); // Destroy duplicate Scoreboard object
            return;
        }

        // Find canvas transform dynamically
        canvasTrans = transform.parent;
        if (canvasTrans == null)
        {
            Debug.LogError("ERROR: Scoreboard.Awake(): Canvas transform not found!");
        }
    }

    public void FSCallback(FloatingScore fs)
    {
        score += fs.score;
    }

    public FloatingScore CreateFloatingScore(int amt, List<Vector2> pts)
    {
        if (prefabFloatingScore == null)
        {
            Debug.LogError("ERROR: Scoreboard.CreateFloatingScore(): Prefab not set!");
            return null;
        }

        GameObject go = Instantiate(prefabFloatingScore);
        go.transform.SetParent(canvasTrans);
        FloatingScore fs = go.GetComponent<FloatingScore>();
        if (fs == null)
        {
            Debug.LogError("ERROR: Scoreboard.CreateFloatingScore(): FloatingScore component not found!");
            return null;
        }

        fs.score = amt;
        fs.reportFinishTo = this.gameObject;
        fs.Init(pts);
        return fs;
    }
}
