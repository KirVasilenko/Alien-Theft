using TMPro;
using UnityEngine;

public class CountControllers : MonoBehaviour
{
    [SerializeField] private TMP_Text _levelCount;
    [SerializeField] private TMP_Text _gemsCount;
   
    private void Start()
    {
        int levelCount = PlayerPrefs.GetInt("LevelCount", 1);
        _levelCount.text = "" + _levelCount; 

        int _gemsCountevelCount = PlayerPrefs.GetInt("LevelCount", 1);
        _gemsCount.text = "" + levelCount; 
    }

    
}
