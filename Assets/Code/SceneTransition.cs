using UnityEngine;
using UnityEngine.SceneManagement; 

public class SceneTransition : MonoBehaviour
{
    
    public string sceneToLoad;

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player")) 
        {
            Debug.Log("Player entered trigger!");
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
