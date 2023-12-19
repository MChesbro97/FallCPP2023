using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using Unity.Burst.Intrinsics;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    AudioSourceManager asm;
    public AudioClip deathSound;

    static GameManager instance = null;
    public static GameManager Instance => instance;

    private int _score = 0;
    public int score
    {
        get => _score;
        set
        {
            _score = value;
            Debug.Log("Score has ben set to: " + _score.ToString());
        }
    }

    private int _lives = 3;
    public int lives
    {
        get => _lives;
        set
        {
            if (_lives > value)
                Respawn();

            _lives = value;

            if (_lives > maxLives)
                _lives = maxLives;

            if (_lives <= 0)
            {
                // Null check for asm
                if (asm != null)
                {
                    // Null check for deathSound
                    if (deathSound != null)
                    {
                        asm.PlayOneShot(deathSound, false);
                    }
                    else
                    {
                        Debug.LogError("Death sound is null!");
                    }
                }
                else
                {
                    Debug.LogError("AudioSourceManager is null!");
                }
                SceneManager.LoadScene("GameOver");
            }
                

            Debug.Log("Lives has been set to: " + _lives.ToString());
            OnLivesValueChanged?.Invoke(_lives);

        }
    }

    public AudioClip lifeSound;
    public AudioClip scoreSound;


    public int maxLives = 5;

    public PlayerController playerPrefab;
    public UnityEvent<int> OnLivesValueChanged;
    

    [HideInInspector] public PlayerController playerInstance;
    [HideInInspector] public Transform spawnPoint;

    // Start is called before the first frame update
    private void Start()
    {
        asm = GetComponent<AudioSourceManager>();
        if (instance)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    if (SceneManager.GetActiveScene().name == "Level")
        //        SceneManager.LoadScene("Title");
        //    else if (SceneManager.GetActiveScene().name == "GameOver")
        //        SceneManager.LoadScene("Title");
        //    else
        //        SceneManager.LoadScene("Level");
        //}
    }

    public void changeScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    public void SpawnPlayer(Transform spawnLocation)
    {
        playerInstance = Instantiate(playerPrefab, spawnLocation.position, spawnLocation.rotation);
        spawnPoint = spawnLocation;
    }

    public void Respawn()
    {
        playerInstance.transform.position = spawnPoint.position;
    }

    public void UpdateSpawnPoint(Transform updatedPoint)
    {
        spawnPoint = updatedPoint;
    }
}
