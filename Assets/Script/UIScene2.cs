using UnityEngine;
using UnityEngine.SceneManagement;
public class UIScene2 : MonoBehaviour
{
    public static UIScene2 instance {  get; private set; }
    public GameObject[] panelScene2;
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
    }
}
