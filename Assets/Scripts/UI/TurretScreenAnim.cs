using UnityEngine;

public class TurretScreenAnim : MonoBehaviour
{
    //references
    [SerializeField] private Animator anim;
    //variables
    private bool isGameScreenActive = false;

    public void OnclickTurretButton()
    {
        if (!isGameScreenActive)
        {
            anim.SetBool("isToggled", true);
            isGameScreenActive = true;
        }
        else
        {
            anim.SetBool("isToggled", false);
            isGameScreenActive = false;
        }
    }

}
