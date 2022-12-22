using UnityEngine;
using UnityEngine.UI;

public class AdditionalMoves : MonoBehaviour
{
    public Text text;

    void Start()
    {
        text.text = $"+ {GameManager.BonusMoves} Moves?";
    }
}
