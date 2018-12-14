using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int score;
    public int numAIToSpawn;
    public int numAICurrent;

    public GameObject playerPrefab;
    public GameObject aiPrefab;

    public Transform charactersHolder;
    public Transform pickupsHolder;

    public List<TankData> players;
    public List<TankData> aiUnits;

    public List<Transform> characterSpawns;
    public List<GameObject> spawnedItems;

    public MapGenerator mapMaker;
    public MenuSettings settingsLoader;

    public float sfxVol;
    public float musicVol;
    public bool isMultiplayer;
    public int seedNum;
    public int mapMode;
    public int highScore;
    public int scorePerKill;

    // Created temporarily
    public GameObject player1;
    public GameObject player2;

    public int p1Lives;
    public int p2Lives;
    public int pLivesTotal;

    public int p1Controller = 0;
    public int p2Controller = 1;

    public int p1Score;
    public int p2Score;

    // Use this for initialization
    void Awake ()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        if (settingsLoader != null)
        {
            settingsLoader.loadSettings();
        }
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        loadSettings();
    }

    // Update is called once per frame
    void Update ()
    {
        if (!characterSpawns.Count.Equals(0))
        {
            respawnAI();
        }
        if (!characterSpawns.Count.Equals(0))
        {
            if (player1 == null)
            {
                p1Lives -= 1;
                if (p1Lives >= 0)
                {
                    player1 = respawnPlayer();
                    setUpPlayer1();
                } 
            }
            if (isMultiplayer == true)
            {
                if (player2 == null)
                {
                    p2Lives -= 1;
                    if (p2Lives >= 0)
                    {
                        player2 = respawnPlayer();
                        setUpPlayer2();
                    }
                }
            }
        }
        setHighScore();
        gameOver();
    }

    void loadSettings()
    {
        p1Score = 0;
        p2Score = 0;
        p1Lives = pLivesTotal;
        p2Lives = pLivesTotal;
        characterSpawns.Clear();
        players.Clear();
        aiUnits.Clear();
        spawnedItems.Clear();
        numAICurrent = 0;
    }
    void gameOver()
    {
        if (isMultiplayer == true)
        {
            if (p1Lives < 0 && p2Lives < 0)
            {
                SceneManager.LoadScene(0);
                loadSettings();
            }
        }
        else
        {
            if (p1Lives < 0)
            {
                SceneManager.LoadScene(0);
                loadSettings();
            }
        }
    }

    void setUpPlayer1()
    {
        TankData playerData = player1.GetComponent<TankData>();
        playerData.score = p1Score;
        playerData.lives = p1Lives;
        playerData.myName = "Player1";
        Controller_Player playerController = player1.GetComponent<Controller_Player>();
        if (p1Controller == 0)
        {
            playerController.selectedController = Controller_Player.controlType.wasd;
        }
        
    }
    void setHighScore()
    {
        foreach (TankData person in players)
        {
            if (person.score > highScore)
            {
                PlayerPrefs.SetInt("HighScore", person.score);
                highScore = person.score; 
            }
        }
    }
    void setUpPlayer2()
    {
        TankData playerData = player2.GetComponent<TankData>();
        playerData.score = p2Score;
        playerData.lives = p2Lives;
        playerData.myName = "Player2";
        Controller_Player playerController = player2.GetComponent<Controller_Player>();
        if (p2Controller == 1)
        {
            playerController.selectedController = Controller_Player.controlType.arrows;
        }

    }



    // Respawn AIs
    void respawnAI()
    {
        while (numAICurrent < numAIToSpawn)
        {
            int randomNum = Random.Range(0, characterSpawns.Count-1);
            Transform locationToSpawn = characterSpawns[randomNum];
            GameObject newAI = Instantiate(aiPrefab, locationToSpawn);
            newAI.transform.SetParent(charactersHolder);
            setAiWaypoints(newAI, locationToSpawn);
            numAICurrent++;
        }
    }

    // Set waypoints for new AI
    void setAiWaypoints(GameObject aiSpawned, Transform spawnLocation)
    {
        foreach (Transform point in spawnLocation.gameObject.GetComponentInParent<Room>().waypoints)
        {
            aiSpawned.GetComponent<Controller_AI>().waypoints.Add(point);
        }
    }

    // Respawn players
    GameObject respawnPlayer()
    {
        int randomNum = Random.Range(0, characterSpawns.Count-1);
        Transform spawnLocation = characterSpawns[randomNum];
        GameObject player = Instantiate(playerPrefab, spawnLocation);
        player.transform.SetParent(charactersHolder);
        return player;
    }
    private void OnDestroy()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }
}
