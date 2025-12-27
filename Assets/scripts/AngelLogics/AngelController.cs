using System;
using DefaultNamespace;
using UnityEngine;

public class AngelController : MonoBehaviour
{
    public float radius = 10f;
    [Range(0, 360)] public float angle = 90f;
    public LayerMask targetMask;
    public LayerMask obstructionMask;

    private Vector3 _rotationVector = new Vector3(0, 1, 0);

    private void Update()
    {
        FieldOfViewCheck();
        transform.Rotate(_rotationVector * 60 * Time.deltaTime);
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    OnDetected();
                }
            }
        }
    }

    void OnDetected()
    {
        Debug.Log("Player Detected!");
        GlobalEvents.PlayerDetectedToAngel?.Invoke();
    }
}