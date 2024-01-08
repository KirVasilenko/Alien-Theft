using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] private int _maxMoves = 10;
    [SerializeField] private TMP_Text _movesText;
    [SerializeField] private TMP_Text _noiseText;
    [SerializeField] private TMP_Text gemsCountText;
    [SerializeField] private TMP_Text levelCountText;

    private int _currentNoiseLevel = 0;
    private int _currentMoves;
    private int _completedLevels;
    
    public GameObject WinObject;
    public GameObject LoseObject;

  

    private void Start()
    {
        _currentMoves = _maxMoves;
        WinObject.gameObject.SetActive(false);
        LoseObject.gameObject.SetActive(false);

        if (_noiseText != null)
        {
            UpdateNoiseText();
        }
        else
        {
            Debug.LogError("NoiseText object not assigned!");
        }

        UpdateMovesText();
        UpdateGemsCountText();
        UpdateLevelCountText();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _currentMoves--;

            if (_currentMoves < 0)
            {
                LoseObject.gameObject.SetActive(true);
                Debug.Log("Game Over! You Lose!");
            }
            else
            {
                UpdateMovesText();
            }
        }
    }

    private void UpdateMovesText()
    {
        _movesText.text = "Moves: " + _currentMoves.ToString();
    }

    private void UpdateNoiseText()
    {
        _noiseText.text = "Noise: " + _currentNoiseLevel.ToString() + "%";
    }

    private void UpdateGemsCountText()
    {
        gemsCountText.text = "" + GemsCountManager.GetTotalGems().ToString();
    }

    private void UpdateLevelCountText()
    {
        levelCountText.text = "" + _completedLevels.ToString();
    }

    public void IncreaseNoiseLevel(int amount)
    {
        _currentNoiseLevel += amount;

        _currentNoiseLevel = Mathf.Clamp(_currentNoiseLevel, 0, 100);

        if (_currentNoiseLevel >= 100)
        {
            LoseObject.gameObject.SetActive(true);
        }

        UpdateNoiseText();
    }

    public int GetCurrentNoiseLevel()
    {
        return _currentNoiseLevel;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            WinObject.gameObject.SetActive(true);

            GemsCountManager.AddGems(10);
            UpdateGemsCountText();

            LevelCountManager.CompleteLevel();
            _completedLevels = LevelCountManager.GetCompletedLevels();
            UpdateLevelCountText();
        }

        if (other.CompareTag("Alarm"))
        {
            IncreaseNoiseLevel(50);
        }
        else if (other.CompareTag("Player"))
        {
            IncreaseNoiseLevel(100);
        }
    }
}
