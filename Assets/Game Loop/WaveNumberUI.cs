using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI), typeof(Animator))]
public class WaveNumberUI : MonoBehaviour
{
    [SerializeField] float _timeBetweenRevealingLetters = 0.5f;

    private TextMeshProUGUI _myTextMeshPro;
    private Animator _myAnimator;

    private const string DISSAPEAR_TRIGGER = "dissapear";

    private void Awake()
    {
        _myTextMeshPro = GetComponent<TextMeshProUGUI>();
        _myAnimator = GetComponent<Animator>();

        FindObjectOfType<WaveManager>().OnStartWave += DisplayNumberOfWave;
    }

    private void OnDestroy() => FindObjectOfType<WaveManager>().OnStartWave -= DisplayNumberOfWave;

    private void DisplayNumberOfWave(int numberOfWave) => StartCoroutine(AnimateTextCoroutine(numberOfWave));

    private IEnumerator AnimateTextCoroutine(int numberOfWave)
    {
        string text = "Wave " + numberOfWave;
        _myTextMeshPro.SetText(text);

        int totalCharacters = text.Length;
        int counter = 0;

        while (counter <= totalCharacters)
        {
            int visibleCount = counter % (totalCharacters + 1);
            _myTextMeshPro.maxVisibleCharacters = visibleCount;

            counter++;
            yield return new WaitForSeconds(_timeBetweenRevealingLetters);
        }

        _myAnimator.SetTrigger(DISSAPEAR_TRIGGER);
    }

    public void TextDissapearedTrigger() => _myTextMeshPro.maxVisibleCharacters = 0;
}
