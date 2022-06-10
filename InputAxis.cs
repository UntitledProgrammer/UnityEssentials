using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct InputAxis
{
    //Attributes:
    /// <summary>When pressed returns a value of one or larger.</summary>
    public KeyCode positive;
    /// <summary>When pressed returns a value of negative one or larger.</summary>
    public KeyCode negative;

    private const float positive_result = 1.0f;
    private const float negative_result = -1.0f;
    private const float neutral_result = 0.0f;

    //Constructor:
    public InputAxis(KeyCode positive, KeyCode negative)
    {
        this.positive = positive;
        this.negative = negative;
    }

    //Methods:
    public float GetInput()
    {
        if (!Input.GetKey(positive) && !Input.GetKey(negative)) return neutral_result;
        return Input.GetKey(positive) ? positive_result : negative_result;
    }
}
