using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public GameObject[] obstaclePrefabs;
    public GameObject[] zombiePrefabs;
    public Transform[] lanes;
    public float minObstacleDelay = 10f, maxObstacleDelay = 40f;

    private float halfGroundSize;
    private int zombieKillCount;

    BaseController playerController;
    Text scoreText;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] Text finalScore;


    void Awake()
    {
        MakeInstance();
    }

    // Start is called before the first frame update
    void Start()
    {
        halfGroundSize = GameObject.Find("GroundBlock Main").GetComponent<GroundBlock>().halfLength;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<BaseController>();

        StartCoroutine("GenerateObstacles");

        scoreText = GameObject.Find("Score Text").GetComponent<Text>();
    }

    private void MakeInstance()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);
    }

    private IEnumerator GenerateObstacles()
    {
        float timer = Random.Range(minObstacleDelay, maxObstacleDelay) / playerController.speed.z;
        yield return new WaitForSeconds(timer);

        CreateObstacles(playerController.gameObject.transform.position.z + halfGroundSize);

        StartCoroutine("GenerateObstacles");
    }

    private void CreateObstacles(float zPos)
    {
        int r = Random.Range(0, 10);

        if (0 <= r && r < 7)
        {
            int obstacleLane = Random.Range(0, lanes.Length);

            AddObstacle(new Vector3(lanes[obstacleLane].transform.position.x, 0, zPos), Random.Range(0, obstaclePrefabs.Length));

            int zombieLane = 0;

            if (obstacleLane == 0)
            {
                zombieLane = Random.Range(0, 2) == 1 ? 1 : 2;
            }
            else if (obstacleLane == 1)
            {
                zombieLane = Random.Range(0, 2) == 1 ? 0 : 2;
            }
            else if (obstacleLane == 2)
            {
                zombieLane = Random.Range(0, 2) == 1 ? 1 : 0;
            }

            AddZombies(new Vector3(lanes[zombieLane].transform.position.x, 0, zPos));
        }
    }

    private void AddObstacle(Vector3 pos, int type)
    {
        GameObject obstacle = Instantiate(obstaclePrefabs[type], pos, Quaternion.identity);
        bool mirror = Random.Range(0, 2) == 1;

        switch (type)
        {
            case 0:
                obstacle.transform.rotation = Quaternion.Euler(0, mirror ? -20f : 20f, 0);
                break;
            case 1:
                obstacle.transform.rotation = Quaternion.Euler(0, mirror ? -20f : 20f, 0);
                break;
            case 2:
                obstacle.transform.rotation = Quaternion.Euler(0, mirror ? -1f : 1f, 0);
                break;
            case 3:
                obstacle.transform.rotation = Quaternion.Euler(0, mirror ? -170f : 170f, 0);
                break;
        }

        obstacle.transform.position = pos;
    }

    private void AddZombies(Vector3 pos)
    {
        int count = Random.Range(0, 3) + 1;

        for (int i = 0; i < count; i++)
        {
            Vector3 shift = new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(1f, 10f) * i);
            Instantiate(zombiePrefabs[Random.Range(0, zombiePrefabs.Length)],
                pos + shift * i, Quaternion.identity);
        }
    }

    public void IncreaseScore()
    {
        zombieKillCount++;
        scoreText.text = zombieKillCount.ToString();
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
        Debug.Log("Exiting game");
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
        finalScore.text = "Killed: " + zombieKillCount;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }
}
