using UnityEngine;
using DG.Tweening;
using System.Linq;
using UnityEngine.Animations;

public class EnemyPath : MonoBehaviour
{
    [SerializeField] private Transform[] pathPoints; // Use Transforms so you can assign GameObjects in the Editor
    [SerializeField] private float pathDuration = 25f; // Duration for the path movement

    void Start()
    {
        GameObject[] points = GameObject.FindGameObjectsWithTag("Player");
        Transform[] pathPoints = points.Select(p => p.transform).ToArray();

        // Convert Transform array to Vector3 array
        Vector3[] positions = new Vector3[pathPoints.Length];
        for (int i = 0; i < pathPoints.Length; i++)
        {
            positions[i] = pathPoints[i].position;
        }

        // Use DOPath with positions and make the enemy look ahead along the path
        transform.DOPath(positions, pathDuration, PathType.CatmullRom);
    }
    public void KillTween()
    {
        DOTween.Kill(transform);
    }
}