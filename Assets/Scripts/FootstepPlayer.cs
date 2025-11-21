using UnityEngine;

public class FootstepPlayer : MonoBehaviour
{
    public AudioClip[] FootstepClips;
    public float StepInterval = 0.5f;
    
    private AudioSource audioSource;
    private PlayerMovement playerMovement;
    private float stepTimer;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void PlayFootstep()
    {
        if (FootstepClips.Length == 0 || playerMovement.Movement == Vector2.zero)
            return;
        
        AudioClip clip = FootstepClips[Random.Range(0, FootstepClips.Length)];
        audioSource.PlayOneShot(clip);
    }

    public void Update() {
        stepTimer += Time.deltaTime;
        if (stepTimer >= StepInterval) {
            PlayFootstep();
            stepTimer = 0.0f;
        }
    }
}
