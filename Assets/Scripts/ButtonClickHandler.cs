using UnityEngine;

public class ButtonClickHandler : MonoBehaviour
{
    public GameObject _objectToToggle;
    private Animator _objectAnimator;

    private bool isVisible = false;

    void Start()
    {
        _objectAnimator = _objectToToggle.GetComponent<Animator>();
        _objectToToggle.SetActive(false);
    }

    public void ToggleObjectVisibility()
    {
        isVisible = !isVisible;
        _objectAnimator.SetBool("isVisible", isVisible);
        _objectToToggle.SetActive(isVisible);
    }

    public void HidePopup()
    {
        if (isVisible)
        {
            ToggleObjectVisibility();
            _objectToToggle.SetActive(false);
        }
    }
}
