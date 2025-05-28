using UnityEngine;

public class ButtonClick : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip shootSound; 
    
    public void PlayClickSound()
    {    
        audioSource.PlayOneShot(shootSound);    
    }
}
