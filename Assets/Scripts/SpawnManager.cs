using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] workers;
    public GameObject scrumMaster;
    public ProductivityTracker productivityTracker;
    private float zSpawnRange = 12.0f;
    private float xSpawnRange = 5.0f;
    private float ySpawn = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        productivityTracker = GameObject.Find("Productivity Tracker").GetComponent<ProductivityTracker>();
    }

    public void SpawnWorker(int difficulty) {
        int workerIndex = workers.Length * difficulty; //modify this to implement difficulty levels based on number of workers
        
        
        for (int i=0; i <= workerIndex; i++) {
            float randomX = Random.Range(-xSpawnRange, xSpawnRange);
            int randomWorker = Random.Range(0, workers.Length);

            Vector3 spawnPos = new Vector3(randomX, ySpawn, zSpawnRange);

            Instantiate(workers[randomWorker], spawnPos, workers[0].gameObject.transform.rotation);
        }
    }

    public void SpawnScrumMaster() {
        float xScrumMasterRange = 2.0f;
        float zScrumMasterRange = 12.5f;
        float randomX = Random.Range(-xScrumMasterRange, xScrumMasterRange);

        Vector3 scrumMasterPos = new Vector3(randomX, ySpawn, zScrumMasterRange);

        Instantiate(scrumMaster, scrumMasterPos, scrumMaster.gameObject.transform.rotation);
    }
}
