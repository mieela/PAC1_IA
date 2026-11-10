using System.Collections;
using UnityEngine;

public class FlockMember : MonoBehaviour
{
    public FlockManager managerRef;
    
    public float Speed { get; private set; }
    public Vector3 Direction { get; private set; }


    void Start()
    {
        Speed = Random.Range(managerRef.minSpeed, managerRef.maxSpeed);
        Direction = transform.forward * Speed;
        StartCoroutine(FlockingLoop());
    }

    void Update()
    {
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            Quaternion.LookRotation(Direction),
            managerRef.rotationSpeed * Time.deltaTime
        );
        transform.Translate(0.0f, 0.0f, Time.deltaTime * Speed);
    }

    IEnumerator FlockingLoop()
    {
        while (true)
        {
            float waitTime = Random.Range(0.25f, 0.55f);
            yield return new WaitForSeconds(waitTime);
            FlockingBehaviour();
        }
    }

    void FlockingBehaviour()
    {
        Vector3 cohesion = Vector3.zero;
        Vector3 separation = Vector3.zero;
        Vector3 align = Vector3.zero;
        int num = 0;

        foreach (GameObject go in managerRef.allFlocks)
        {
            if (go != this.gameObject)
            {
                float distance = Vector3.Distance(go.transform.position, transform.position);
                if (distance <= managerRef.neighbourDistance)
                {
                    cohesion += go.transform.position;
                    align += go.GetComponent<FlockMember>().Direction;
                    num++;
                    separation -= (transform.position - go.transform.position) / (distance * distance);
                }
            }
        }

        if (num > 0)
        {
            align /= num;
            Speed = Mathf.Clamp(align.magnitude, managerRef.minSpeed, managerRef.maxSpeed);
            cohesion = (cohesion / num - transform.position).normalized * Speed;

            Vector3 rawDir = cohesion + align + separation;

            Direction = rawDir.normalized * Speed;
        }
    }
}
