using UnityEngine;

public class BlooDRemover : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 3f);
    }
}
