using UnityEngine;
using UnityEngine.UI;

public class TextTest : MonoBehaviour
{
    private Text _text;

    private TextGenerator _textGenerator;

    private void Start()
    {
        _text = this.GetComponent<Text>();
        _textGenerator = _text.cachedTextGenerator;
    }

    private void Update()
    {
        Debug.Log(_textGenerator.characterCount);
    }
}