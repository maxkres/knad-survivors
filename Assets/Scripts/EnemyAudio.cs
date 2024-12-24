using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    [Header("Random Audio Sources")]
    [SerializeField] private AudioSource audioSourceA;
    [SerializeField] private AudioSource audioSourceB;
    [SerializeField] private AudioSource audioSourceC;

    [Header("Death Sound")]
    [SerializeField] private AudioClip deathSound;

    [Header("Random Interval Range (Seconds)")]
    [SerializeField] private float minInterval = 5f;
    [SerializeField] private float maxInterval = 10f;

    private float timer;
    private Vector3 lastPosition;

    // Tracks if the application (or scene) is quitting
    private static bool isQuitting;

    void OnApplicationQuit()
    {
        isQuitting = true;
    }

    void Start()
    {
        timer = Random.Range(minInterval, maxInterval);
    }

    void Update()
    {
        lastPosition = transform.position;
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            int randomIndex = Random.Range(0, 3);
            if (randomIndex == 0 && audioSourceA && audioSourceA.clip)
                PlayAudioSourceAtPoint(audioSourceA, lastPosition);
            else if (randomIndex == 1 && audioSourceB && audioSourceB.clip)
                PlayAudioSourceAtPoint(audioSourceB, lastPosition);
            else if (randomIndex == 2 && audioSourceC && audioSourceC.clip)
                PlayAudioSourceAtPoint(audioSourceC, lastPosition);

            timer = Random.Range(minInterval, maxInterval);
        }
    }

    void OnDestroy()
    {
        // If the app (or scene) is quitting, do nothing here
        if (isQuitting) return;

        if (deathSound != null)
        {
            AudioSource.PlayClipAtPoint(deathSound, lastPosition);
        }
    }

    void PlayAudioSourceAtPoint(AudioSource source, Vector3 position)
    {
        if (source == null || source.clip == null) return;

        GameObject tempGO = new GameObject("TempAudioSource");
        tempGO.transform.position = position;
        AudioSource tempSource = tempGO.AddComponent<AudioSource>();

        tempSource.clip = source.clip;
        tempSource.volume = source.volume;
        tempSource.pitch = source.pitch;
        tempSource.spatialBlend = source.spatialBlend;
        tempSource.minDistance = source.minDistance;
        tempSource.maxDistance = source.maxDistance;
        tempSource.rolloffMode = source.rolloffMode;
        tempSource.outputAudioMixerGroup = source.outputAudioMixerGroup;

        tempSource.Play();
        Destroy(tempGO, source.clip.length);
    }
}
