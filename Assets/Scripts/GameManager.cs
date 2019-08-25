using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    private BoardScript boardScript;
    public float speed = 1f;
    public GameObject[] downTiles;
    public static GameManager instance;
    public GameObject player;

    internal int timeCount = 1;
    private float endDelay = 3f;
    private readonly float interval = 1f;
    private float accelerationTime = 0f;
    private float intervaltime = 0;
    private readonly List<int> startPos = new List<int>();
    private GameObject startCanvas;
    private GameObject endImage;
    

    void Awake()
    {
        instance = this;
        boardScript = GetComponent<BoardScript>();
        startCanvas = GameObject.Find("StartCanvas");
        endImage = GameObject.Find("UI").transform.Find("EndImage").gameObject;

        intervaltime += interval;

        for (int x = 0; x < boardScript.columns; x++) startPos.Add(x);
    }

    void OnEnable()
    {
        Instantiate(player);
        boardScript.BGSetup();
        startCanvas.SetActive(false);
    }

    void DownComeUp(int timeCount)
    {
        List<int> downPos = new List<int>(startPos.ToArray());
        int downCount = (int)Mathf.Log(timeCount, 4f) + Random.Range(0, 2);
        for (int x = 0; x < downCount; x++)
        {
            int randomIndex = Random.Range(0, downPos.Count);
            int randomPos = downPos[randomIndex];
            downPos.RemoveAt(randomIndex);

            GameObject downTile = downTiles[Random.Range(0, downTiles.Length)];

            Instantiate(downTile, new Vector3(randomPos, boardScript.rows, 0), Quaternion.identity);
        }
    }

    public void EndGame()
    {
        endImage.SetActive(true);
        instance.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        
        if (intervaltime > 0)
        {
            intervaltime -= Time.deltaTime;
        }
        else
        {
            timeCount += 1;
            intervaltime += interval;
        }

        if (accelerationTime > 0)
        {
            accelerationTime -= Time.deltaTime;
        }
        else
        {
            DownComeUp(timeCount);
            accelerationTime +=  Mathf.Pow(0.99f, timeCount)  * interval / speed;
        }

    }
}
