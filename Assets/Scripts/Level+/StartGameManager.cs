using UnityEngine;

public class StartGameManager : MonoBehaviour
{
    public GameObject startPanel; // assign your UI object (Canvas child)

    private bool gameStarted = false;

    void Start()
    {
        Time.timeScale = 0f; // pause game at start
        startPanel.SetActive(true); // show UI panel
    }

    void Update()
    {
        if (!gameStarted && Input.GetKeyDown(KeyCode.Return))
        {
            Time.timeScale = 1f; // unpause game
            startPanel.SetActive(false); // hide panel
            gameStarted = true; // lock this from happening again
        }
    }
}
