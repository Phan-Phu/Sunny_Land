using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : MonoBehaviour
{
    [SerializeField] Vector3 targetPosition;
    [SerializeField] float period;
    private Vector3 startPosition;

    private void Awake()
    {
    }

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        float cycles = Time.time / period;
        float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(tau * cycles); // -1 to 1
        float factor = (rawSinWave + 1) / 2;
        transform.position = startPosition + (targetPosition * factor);

        Flip(rawSinWave);
    }

    private void Flip(float rawSinWave)
    {
        float dir = targetPosition.x - transform.position.x;
        if((Mathf.Abs(rawSinWave) - .95f) >= Mathf.Epsilon)
        {
            transform.localScale = new Vector3(-Mathf.Sign(rawSinWave), 1, 1);
        }
    }
}
