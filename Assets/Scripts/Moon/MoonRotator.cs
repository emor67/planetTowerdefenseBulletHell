using UnityEngine;
using DG.Tweening;

public class MoonRotator : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //DOTween.SetTweensCapacity(1250,50);
        transform.DOLocalRotate(new Vector3(0,0,360), 8f, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
