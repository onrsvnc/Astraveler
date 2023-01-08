using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] int levelLoadDelay = 2;
    [SerializeField] AudioClip deathSfx;
    [SerializeField] AudioClip finishSfx;
    [SerializeField] ParticleSystem deathParticle;
    [SerializeField] ParticleSystem winParticle;
    [SerializeField] GameObject mainBody;
    [SerializeField] GameObject engineExp;

    public bool isTransitioning = false;
    bool collisionDisabled = false;


    void Update()
    {
       // RespondToDebugKeys();
    }

    void OnCollisionEnter(Collision other) 
    {
        Debug.Log("Gadgdag");
        if (isTransitioning || collisionDisabled) {return;}
        
            switch (other.gameObject.tag)
            {
                case "Friendly":
                Debug.Log("Friendly Fire");
                break;

                case "Finish":
                FinishSequence();
                break;

                default:
                StartCrashSequence();
                break;
            }
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        deathParticle.Play();
        GetComponent<Movement>().enabled = false;
        GetComponent<AudioSource>().Stop();
        FindObjectOfType<SideThrust>().GetComponent<AudioSource>().enabled = false;
        GetComponent<AudioSource>().PlayOneShot(deathSfx);
        Destroy(mainBody,1f);
        Destroy(engineExp,1f);
        Invoke ("ReloadLevel", levelLoadDelay);
    }

    void FinishSequence()
    {    
        isTransitioning = true;
        winParticle.Play();
        GetComponent<Movement>().enabled = false;
        GetComponent<AudioSource>().Stop();
        FindObjectOfType<SideThrust>().GetComponent<AudioSource>().enabled = false;
        GetComponent<AudioSource>().PlayOneShot(finishSfx);
        Invoke ("LoadNextLevel", levelLoadDelay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
           LoadNextLevel(); 
           Debug.Log("Cheat: Level Skiped");
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            
            collisionDisabled = !collisionDisabled;  //toggle collision
            Debug.Log("Cheat: Collisions Switched");
        }
    }

}
