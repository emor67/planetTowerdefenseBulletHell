using UnityEngine;

public class GameScreenAnim : MonoBehaviour
{
    //references
    [SerializeField] private Animator anim;
    //variables
    private bool isGameScreenActive = false;

    public void OnclickMenuButton()
    {
        if(!isGameScreenActive)
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

}
