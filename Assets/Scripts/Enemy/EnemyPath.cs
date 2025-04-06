using UnityEngine;
using DG.Tweening;
using System.Linq;

public class EnemyPath : MonoBehaviour
{
    [SerializeField] private Transform[] pathPoints; // Use Transforms so you can assign GameObjects in the Editor

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

        // Use DOPath with positions
        transform.DOPath(positions, 20f, PathType.CatmullRom);
    }

    void Update()
    {
        // Optional: Add dynamic behavior here
    }
}
