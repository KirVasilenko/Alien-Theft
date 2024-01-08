using UnityEngine;

public class TriggerControler : MonoBehaviour
{
    private GameController _gameController;

    private void Start()
    {
        _gameController = FindObjectOfType<GameController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _gameController.WinObject.gameObject.SetActive(true);
        }
        else if (other)
        {
            Destroy(other.gameObject);
            
        }
    }
}

