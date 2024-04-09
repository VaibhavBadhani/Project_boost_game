using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    [SerializeField]float delay = 1f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;
     

    AudioSource audioSource ;
     
    bool isTransitioning = false ;
    bool collisionDisable = false;    
    void Update()
    {
        RespondToDebugKeys();
    }
     void RespondToDebugKeys()
     {
        if(Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
collisionDisable=!collisionDisable;
        }
     }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void OnCollisionEnter(Collision other)
     {
        if(isTransitioning  || collisionDisable){return ;}
        
        switch(other.gameObject.tag)
        {
            
            case "Friendly":
                Debug.Log("This is Friendly");
                break;
            case "Finish":
                StartEndCrashSequence();
                break;
            default:
                StartCrashSequence();
                break ;
        }
     }
     void StartCrashSequence()
     {
        isTransitioning=true;
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel",delay);
     }
     void StartEndCrashSequence()
     {
        isTransitioning=true;
         audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Debug.Log("Entering New Level");
        Invoke("LoadNextLevel",delay +1);
     }
     void LoadNextLevel()
     {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex=currentSceneIndex+1;
        if(nextSceneIndex==SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex=0;
        }
        SceneManager.LoadScene(nextSceneIndex);
     }
     void ReloadLevel()
     {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
     }   
 }


