using UnityEngine;
using UnityEngine.AI;

public class FormationMember : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform targetTr;
    public Vector3 formationPos;

    void Start()
    {
        transform.SetPositionAndRotation(
            targetTr.TransformPoint(formationPos),
            targetTr.rotation
        );
    }

    void Update()
    {
        agent.destination = targetTr.TransformPoint(formationPos);
    }

}
