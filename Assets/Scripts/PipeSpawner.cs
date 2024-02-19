using System.Collections;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject dayPipe;
    [SerializeField] private GameObject nightPipe;

    [Space, SerializeField] private float timeToSpawnFirstPipe;
    [SerializeField] private float timeToSpawnPipe;

    [Space, SerializeField] private Transform pipeSpawnPosition;

    [Space, SerializeField] private Transform pipeMinSpawnHeight;
    [SerializeField] private Transform pipeMaxSpawnHeight;

    private void Start()
    {
        StartCoroutine(SpawnPipes());
    }

    private GameObject SpawnPipe()
    {
        Debug.Log("PipeSpawner :: SpawnPipe()");

        if (ThemePicker.isNight) return Instantiate(nightPipe, GetPipePosition(), Quaternion.identity);
        else return Instantiate(dayPipe, GetPipePosition(), Quaternion.identity);
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
        InitializePipePool();

        yield return new WaitForSeconds(timeToSpawnFirstPipe);

        GetPipe();

        WaitForSeconds timToSpawnPipeWaitForSeconds = new WaitForSeconds(timeToSpawnPipe);

        do
        {
            yield return timToSpawnPipeWaitForSeconds;

            GetPipe();
        } while (!GameManager.Instance.isGameOver);
    }


    #region"Pipe pooling system"
    [Space, SerializeField] int pipeAmount = 3;

    GameObject[] pipePool;
    int currentPoolIndex;
    int maxPoolIndex;

    void InitializePipePool()
    {
        pipePool = new GameObject[pipeAmount];
        maxPoolIndex = pipePool.Length - 1;

        for (int i = 0; i < pipePool.Length; i++)
        {
            pipePool[i] = SpawnPipe();
            pipePool[i].transform.position = new Vector3(-99, -99, 0);
        }
    }

    void GetPipe()
    {
        pipePool[currentPoolIndex].transform.position = GetPipePosition();

        currentPoolIndex = currentPoolIndex == maxPoolIndex ? 0 : ++currentPoolIndex;
    }
    #endregion
}
