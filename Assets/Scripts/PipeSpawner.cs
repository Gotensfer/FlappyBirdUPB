using System.Collections;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject pipe;

    [Space, SerializeField] private float timeToSpawnFirstPipe;
    [SerializeField] private float timeToSpawnPipe;

    [Space, SerializeField] private Transform pipeSpawnPosition;

    [Space, SerializeField] private Transform pipeMinSpawnHeight;
    [SerializeField] private Transform pipeMaxSpawnHeight;

    private void Start()
    {
        StartCoroutine(SpawnPipes());
    }

    private void SpawnPipe()
    {
        Debug.Log("PipeSpawner :: SpawnPipe()");

        Instantiate(pipe, GetPipePosition(), Quaternion.identity);
    }

    private Vector3 GetPipePosition()
    {
        return new Vector3(pipeSpawnPosition.position.x, GetPipeHeight(), pipeSpawnPosition.position.z);
    }

    private float GetPipeHeight()
    {
        return Random.Range(pipeMinSpawnHeight.position.y, pipeMaxSpawnHeight.position.y);
    }

    private IEnumerator SpawnPipes()
    {
        yield return new WaitForSeconds(timeToSpawnFirstPipe);

        SpawnPipe();

        WaitForSeconds timToSpawnPipeWaitForSeconds = new WaitForSeconds(timeToSpawnPipe);

        do
        {
            yield return timToSpawnPipeWaitForSeconds;

            SpawnPipe();
        } while (!GameManager.Instance.isGameOver);
    }
}
