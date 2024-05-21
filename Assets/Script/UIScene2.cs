using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIScene2 : MonoBehaviour
{
    public static UIScene2 instance {  get; private set; }
    public GameObject[] panelScene2;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeInGame;
    private GameObject[,] backgrounds = new GameObject[10, 10];
    public bool isEndGame;
    public GameObject backgroundPrefab;

    private int score;
    private int minutes = 0, seconds = 0;
    private float elapsedTime = 0f;
    private void Awake()
    {
    }
    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    private void Update()
    {
        UpdateScoreText();
        TimeInGame();
    }
    public void AddScore(int value)
    {
        score += value;
        UpdateScoreText();
    }
    private void UpdateScoreText()
    {
        scoreText.text = "Score : " + score;
    }
    private void TimeInGame()
    {
        if(!isEndGame)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= 1f)
            {
                seconds += 1;
                elapsedTime = 0f;

                if (seconds >= 60)
                {
                    seconds = 0;
                    minutes += 1;
                }
                timeInGame.text = string.Format("{0:D2} : {1:D2}", minutes, seconds);
            }
        }
    }
    private void BackgroundsMatrix()
    {
        for (int i = 0; i < backgrounds.GetLength(0); i++)
        {
            for (int j = 0; j < backgrounds.GetLength(1); j++)
            {
                backgrounds[i,j] = Instantiate(backgroundPrefab,transform);
                backgrounds[i,j].transform.position = new Vector3(i * 6f, j * 5.7f, 0f);
            }
        }
    }
    public void ReLoadGame()
    {
        SceneManager.LoadScene(1);
    }
    public void SceneMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void EndGame()
    {
        panelScene2[0].SetActive(true);
        isEndGame = true;
    }
}
