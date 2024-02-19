using System.Collections;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField, Range(0, 5)] private float speed;

    private void Update()
    {
        if (GameManager.Instance.isGameOver)
            return;

        transform.position += Vector3.left * speed * Time.deltaTime;
    }
}
