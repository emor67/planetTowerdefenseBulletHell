using UnityEngine;

public class DroneScreenAnim : MonoBehaviour
{
    //references
    [SerializeField] private Animator anim;
    //variables
    private bool isGameScreenActive = false;

    public void OnclickDroneButton()
    {
        if(!isGameScreenActive)
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
