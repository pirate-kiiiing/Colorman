using System;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveBase : MonoBehaviour
{
    public Text _text;
    public uint Objective;
    public Image BorderImage;

    protected void Validate()
    {
        if (Objective == 0)
            throw new ArgumentException($"{nameof(Objective)} must be greater than 0");
    }

    public void Refresh()
    {
        _text.text = text;
        _text.color = color;

        BorderImage.color = (isComplete == true)
            ? UnityEngine.Color.white
            : UnityEngine.Color.black;
    }

    protected virtual string text => throw new NotImplementedException();

    protected virtual bool isComplete => throw new NotImplementedException();

    private UnityEngine.Color color =>
        (isComplete == true)
            ? UnityEngine.Color.white
            : UnityEngine.Color.black;
}
