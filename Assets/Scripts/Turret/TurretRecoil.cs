using UnityEngine;
using System.Collections;

public class TurretRecoil : MonoBehaviour
{
    [Header("Recoil Settings")]
    public Transform turretBarrel; // The part to recoil
    public Vector2 recoilOffset = new Vector2(-0.1f, 0); // Kickback in local space
    public float recoilTime = 0.05f;
    public float returnTime = 0.1f;

    private Coroutine recoilRoutine;

    public void Fire()
    {
        if (recoilRoutine != null)
            StopCoroutine(recoilRoutine);

        recoilRoutine = StartCoroutine(DoRecoil());
    }

    private IEnumerator DoRecoil()
    {
        Vector3 startPos = turretBarrel.localPosition;
        Vector3 recoilPos = startPos + (Vector3)recoilOffset;

        // Move to recoil position
        float elapsed = 0;
        while (elapsed < recoilTime)
        {
            turretBarrel.localPosition = Vector3.Lerp(startPos, recoilPos, elapsed / recoilTime);
            elapsed += Time.deltaTime;
            yield return null;
        }
        turretBarrel.localPosition = recoilPos;

        // Move back to start
        elapsed = 0;
        while (elapsed < returnTime)
        {
            turretBarrel.localPosition = Vector3.Lerp(recoilPos, startPos, elapsed / returnTime);
            elapsed += Time.deltaTime;
            yield return null;
        }
        turretBarrel.localPosition = startPos;
    }
}
