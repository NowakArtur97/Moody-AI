using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TipOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private readonly string TARGET_TEXT_GAME_OBJECT_NAME = "Tip Text (TMP)";

    [SerializeField] private string _textToDisplay = "CHANGE ME!";
    [SerializeField] private float _timeBetweenRevealingLetters = 0.05f;
    [SerializeField] private float _fontSize = 15;

    private TMP_Text _targetText;
    private TextMeshProUGUI _myTextMeshPro;
    private IEnumerator _currentTextAnimationCoroutine;

    private void OnEnable()
    {
        if (_targetText == null)
        {
            _targetText = GameObject.Find(TARGET_TEXT_GAME_OBJECT_NAME).GetComponent<TMP_Text>();
            _myTextMeshPro = _targetText.GetComponent<TextMeshProUGUI>();
        }
    }

    private IEnumerator AnimateTextCoroutine()
    {
        _targetText.text = _textToDisplay;
        _targetText.fontSize = _fontSize;

        int totalCharacters = _textToDisplay.Length;
        int counter = 0;

        while (counter <= totalCharacters)
        {
            int visibleCount = counter % (totalCharacters + 1);
            _myTextMeshPro.maxVisibleCharacters = visibleCount;

            counter++;
            yield return new WaitForSeconds(_timeBetweenRevealingLetters);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _currentTextAnimationCoroutine = AnimateTextCoroutine();
        StartCoroutine(_currentTextAnimationCoroutine);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_currentTextAnimationCoroutine != null)
        {
            StopCoroutine(_currentTextAnimationCoroutine);
        }
    }
}
