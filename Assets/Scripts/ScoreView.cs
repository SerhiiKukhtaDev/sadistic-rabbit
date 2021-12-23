using UnityEngine;
using UnityEngine.UI;

public class ScoreView : MonoBehaviour
{
    private Text _text;

    private void Awake()
    {
        _text = GetComponent<Text>();

        ChangeValue(0);
    }

    public void ChangeValue(float value)
    {
        _text.text = $"Score: {value}";
    }
}
