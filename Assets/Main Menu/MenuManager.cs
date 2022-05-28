using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _mainView;
    [SerializeField] private GameObject _optionsView;

    private bool _isInMainView;

    private void Awake() => ChangeView();

    public void ChangeView()
    {
        _isInMainView = !_isInMainView;

        _mainView.SetActive(_isInMainView);
        _optionsView.SetActive(!_isInMainView);
    }
}
