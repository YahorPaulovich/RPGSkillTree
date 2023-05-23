using System.Collections;
using UnityEngine;

public class UICustomHeartBeatEffect : MonoBehaviour
{
    [SerializeField] private float _strength;
    [SerializeField] private float _speed;

    private void Start()
    {
        StartCoroutine(Pulse());
    }

    private IEnumerator Pulse()
    {
        // Loops forever
        while (true)
        {
            float timer = 0f;
            float originalSize = transform.localScale.x;

            // Heart beat twice
            for (int i = 0; i < 2; i++)
            {
                // Zoom in
                while (timer < 0.1f)
                {
                    yield return new WaitForEndOfFrame();
                    timer += Time.deltaTime;

                    transform.localScale = new Vector3
                    (
                        transform.localScale.x + (Time.deltaTime * _strength * 2),
                        transform.localScale.y + (Time.deltaTime * _strength * 2)
                    );
                }
            }

            // Return to normal
            while (transform.localScale.x < originalSize)
            {
                yield return new WaitForEndOfFrame();

                transform.localScale = new Vector3
                (
                    transform.localScale.x - Time.deltaTime * _strength,
                    transform.localScale.y - Time.deltaTime * _strength
                );
            }

            transform.localScale = new Vector3(originalSize, originalSize);

            yield return new WaitForSeconds(_speed);
        }
    }
}
