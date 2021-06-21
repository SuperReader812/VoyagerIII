using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionController : MonoBehaviour
{


    // Variables
    // PARAMETERS
    [SerializeField]int LevelWait;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip finishSound;
    // CACHE
    Movement movement;
    AudioSource sound;

    
    // Start and update
    void Start() 
    {
        // Getting our components and storing them in variables
        movement=GetComponent<Movement>();
        sound = GetComponent<AudioSource>();
    }

    // Private Methods
    private void OnCollisionEnter(Collision other) 
    {
        switch(other.gameObject.tag)
        {
            // Checking if we crashed into something and running the appropriate methods
            case "Non-Collision":
                Debug.Log("This object is friendly");
                break;
            case "Finish":
                BetterNewLevel();
                break;
            default:
                CrashSequence();
                break;


            
        }
    }

    // Reloading the scene on death
    void ReloadScene()
    {
        int CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(CurrentSceneIndex);
    }

    // Loading the new scene on finish
    void LoadNextScene()
    {
        int CurrentLevel=SceneManager.GetActiveScene().buildIndex;
        int NextLevel=CurrentLevel+1;
        if (NextLevel == SceneManager.sceneCountInBuildSettings)
        {
            NextLevel=0;
        }
        SceneManager.LoadScene(NextLevel);
    }
    
    // Playing our particle system and sfx on death
    void CrashSequence()
    {
        movement.enabled=false;
        sound.PlayOneShot(crashSound);
        Invoke("ReloadScene",LevelWait);
    }

    // Playing our particle system and sfx on finish
    void BetterNewLevel()
    {
        movement.enabled=false;
        sound.PlayOneShot(finishSound);
        Invoke("LoadNextScene", LevelWait);
    }
}
