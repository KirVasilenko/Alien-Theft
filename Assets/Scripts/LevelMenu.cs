using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    
    public int levelSceneIndex;

    
    public void LoadLevel()
    {
    
        SceneManager.LoadScene(levelSceneIndex);
    }
}