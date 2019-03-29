using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEditor.SceneManagement;


public class GameController : MonoBehaviour {

    const int STARTING_LIVES = 3;
    public static GameController Instance;

    public List<GameObject> breakableObjs;
    public Text scoreText;
    public Text gameOverText;
    public PlayerStates playerStates;
    public int numLives = STARTING_LIVES;
    public GameObject GameOverObj;
    public int score = 0;
    public GameObject readyObj;

    public bool gameOver = false;
    public bool mainMenu = true;
    public bool readyToPlay = false;

    bool hasChangedLevel = false;
    bool livesSetup = false;
    List<float> enemySpeeds;
    List<Image> lives;
    GameObject tempReady;

    void Awake()
    {
        ///Set this to be main Game Controller
        if (Instance)
        {
            Debug.Log("I already exist: destroying self");
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        enemySpeeds = new List<float>();
        FillEnemySpeeds(enemySpeeds);
    }

    void FillEnemySpeeds(List<float> speedList)
    {
        int numLevels = UnityEngine.SceneManagement.SceneManager.sceneCount;

        speedList.Clear();

        ///Get Speeds from Json
        //for (int i = 0; i <= numLevels; i++)
        //{
        //}


        ///Temp hardcode of speeds
            
            speedList.Add(1.2f);
            speedList.Add(1.5f);
            speedList.Add(1.8f);
            speedList.Add(2f);

    }

	// Use this for initialization
    void OnLevelWasLoaded() 
    {
        if (Application.loadedLevelName != "Menu")
        {
            lives = new List<Image>();
            breakableObjs = new List<GameObject>();

            tempReady = Instantiate(readyObj);

            foreach (GameObject gObj in GameObject.FindGameObjectsWithTag("Breakable"))
            {
                breakableObjs.Add(gObj);
            }

            foreach (GameObject gObj in GameObject.FindGameObjectsWithTag("Life"))
            {
                lives.Add(gObj.GetComponent<Image>());
            }
            
            foreach (GameObject gObj in GameObject.FindGameObjectsWithTag("UiText"))
            {
                if (gObj.name == "Score")
                {
                    scoreText = gObj.GetComponent<Text>();
                }

                if (gObj.name == "GameOver")
                {
                    gameOverText = gObj.GetComponent<Text>();
                }
            }

            playerStates = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStates>();
            hasChangedLevel = false;
            livesSetup = false;
            readyToPlay = false;
        } //End check for not in menu
        else
        {
            gameOver = false;
            numLives = 3;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!(Application.loadedLevelName == "Menu"))
        {
            if (!gameOver && readyToPlay)
            {
                UpDateScore();
                CheckForPlayerDeath();
            }

           if (!livesSetup)
           {
                for (int i = STARTING_LIVES; i > numLives; i--)
                {
                    if (lives[i-1] == null)
                    {
                        Debug.Log("This is null");
                    }
                    lives[i-1].enabled = false;
                }
                livesSetup = true;
           }

           

            CheckForEndLevel();
        }
        
	}

    void UpDateScore()
    {
        scoreText.text = "Score: " + score;
    }

    void CheckForPlayerDeath()
    {
        if (playerStates.isHit)
        {
            if (numLives > 0)
            {
                lives[numLives - 1].enabled = false;
                numLives--;
                playerStates.isHit = false;
            }
            else 
            {
                gameOver = true;
                Instantiate(GameOverObj);
            }
        }
    }

    void CheckForEndLevel()
    {
        if (breakableObjs.Count <= 0 && !hasChangedLevel)
        {
            if (Application.loadedLevelName == "Level1")
            {
                Application.LoadLevel(2);
                hasChangedLevel = true;
                return;
            }

            if (Application.loadedLevelName == "Level2")
            {
                Application.LoadLevel(3);
                hasChangedLevel = true;
                return;
            }

            if (Application.loadedLevelName == "Level3")
            {
                Application.LoadLevel(4);
                hasChangedLevel = true;
                return;
            }

            if (Application.loadedLevelName == "Level4")
            {
                Application.LoadLevel(5);
                hasChangedLevel = true;
                return;
            }

            if (Application.loadedLevelName == "Level5")
            {
                Application.LoadLevel(0);
                hasChangedLevel = true;
                return;
            }
        }
    }

    public float GetEnemySpeed()
    {
        switch (Application.loadedLevelName)
        {
            case "Level2":
                {
                    return enemySpeeds[0];
                    break;
                }
            case "Level3":
                {
                    return enemySpeeds[1];
                    break;
                }
            case "Level4":
                {
                    return enemySpeeds[2];
                    break;
                }
            case "Level5":
                {
                    return enemySpeeds[0];
                    break;
                }

        }
        return 0;
    }

}
