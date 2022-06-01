using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI), typeof(Animator))]
public class WaveNumberUI : MonoBehaviour
{
    private const string DISSAPEAR_TRIGGER = "dissapear";

    [SerializeField] float _timeBetweenRevealingLetters = 0.5f;
    [SerializeField] private TMP_Text _myText;

    private TextMeshProUGUI _myTextMeshPro;
    private Animator _myAnimator;
    private WaveManager _waveManager;
    private int _numberOfWave;

    private int _characterCounter;

    public bool IsAnimating { get; private set; }

    private void Awake()
    {
        _myTextMeshPro = GetComponent<TextMeshProUGUI>();
        _myAnimator = GetComponent<Animator>();

        _waveManager = FindObjectOfType<WaveManager>();
        _waveManager.OnStartWave += DisplayNumberOfWave;
    }

    private void OnDestroy() => _waveManager.OnStartWave -= DisplayNumberOfWave;

    private void DisplayNumberOfWave(int numberOfWave)
    {
        _numberOfWave = numberOfWave;
        StartCoroutine(AnimateTextCoroutine());
    }

    public IEnumerator AnimateTextCoroutine()
    {
        IsAnimating = true;

        string text = "Wave " + _numberOfWave;

        _myText.text = text;

        int totalCharacters = text.Length;

        if (_characterCounter >= totalCharacters)
        {
            _characterCounter = 0;
        }

        while (_characterCounter <= totalCharacters)
        {
            int visibleCount = _characterCounter % (totalCharacters + 1);
            _myTextMeshPro.maxVisibleCharacters = visibleCount;

            _characterCounter++;
            yield return new WaitForSecondsRealtime(_timeBetweenRevealingLetters);
        }

        _myAnimator.SetTrigger(DISSAPEAR_TRIGGER);
    }

    public void TextDissapearedTrigger()
    {
        _myTextMeshPro.maxVisibleCharacters = 0;
        IsAnimating = false;
    }
}
