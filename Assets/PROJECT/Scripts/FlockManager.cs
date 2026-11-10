using UnityEngine;

public class FlockManager : MonoBehaviour {
    public GameObject flockPrefab;
    public int numFlocks = 20;
    public float limit = 5f;
    public GameObject[] allFlocks;


    [Header("Flocking Settings")]
    [Range(0.0f, 5.0f)]
    public float minSpeed;
    [Range(0.0f, 5.0f)]
    public float maxSpeed;
    [Range(1.0f, 10.0f)]
    public float neighbourDistance;
    [Range(0.0f, 5.0f)]
    public float rotationSpeed;

    void Start() {
        allFlocks = new GameObject[numFlocks];
        for (int i = 0; i < numFlocks; ++i) {
            Vector3 pos = 
                this.transform.position + 
                new Vector3(
                    Random.Range(-limit, limit),
                    Random.Range(-limit, limit),
                    Random.Range(-limit, limit)
                );
            
            Vector3 randRot = new Vector3 (Random.Range(-1f,2f), Random.Range(-1f, 2f), Random.Range(-1f, 2f));
            allFlocks[i] = Instantiate(flockPrefab, pos, Quaternion.LookRotation(randRot));
            allFlocks[i].GetComponent<FlockMember>().managerRef = this;
        }
    }
}
