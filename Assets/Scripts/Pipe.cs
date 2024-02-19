using System.Collections;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField, Range(0, 5)] private float speed;
    [SerializeField] private float timeToDestroyPipe;

    private void Start()
    {
        StartCoroutine(DestroyPipe());
    }

    private void Update()
    {
        if (GameManager.Instance.isGameOver)
            return;

        transform.position += Vector3.left * speed * Time.deltaTime;
    }

    private IEnumerator DestroyPipe()
    {
        yield return new WaitForSeconds(timeToDestroyPipe);

        if (!GameManager.Instance.isGameOver)
        {
            Debug.Log("Pipe :: DestroyPipe()");

            Destroy(this.gameObject);
        }
    }
}
