using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    [Header("Audio Sources (2 sources)")]
    [SerializeField] private AudioSource audioSourceA;
    [SerializeField] private AudioSource audioSourceB;

    [Header("Random Interval Range (Seconds)")]
    [SerializeField] private float minInterval = 5f;
    [SerializeField] private float maxInterval = 10f;

    private float timer;

    void Start()
    {
        if (audioSourceA == null)
        {
            audioSourceA = GetComponent<AudioSource>();
        }
        if (audioSourceB == null)
        {
            audioSourceB = GetComponent<AudioSource>();
        }

        timer = Random.Range(minInterval, maxInterval);
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            int randomIndex = Random.Range(0, 2);

            if (randomIndex == 0 && audioSourceA != null)
            {
                audioSourceA.Play();
            }
            else if (randomIndex == 1 && audioSourceB != null)
            {
                audioSourceB.Play();
            }
            timer = Random.Range(minInterval, maxInterval);
        }
    }
}
