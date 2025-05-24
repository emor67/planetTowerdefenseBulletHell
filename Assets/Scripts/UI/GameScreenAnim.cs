using UnityEngine;

public class GameScreenAnim : MonoBehaviour
{
    //references
    [SerializeField] private Animator anim;
    //variables
    private bool isGameScreenActive = false;
    private bool isPaused = false;

    public void OnclickMenuButton()
    {
        if (!isGameScreenActive)
        {
            anim.SetBool("isMenuToggled", true);
            isGameScreenActive = true;
        }
        else
        {
            anim.SetBool("isMenuToggled", false);
            isGameScreenActive = false;
        }
    }

     void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f; // Pause the game
            // Optionally enable pause menu UI here
        }
        else
        {
            Time.timeScale = 1f; // Resume the game
            // Optionally disable pause menu UI here
        }
    }

}
