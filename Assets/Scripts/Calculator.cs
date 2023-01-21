using System;
using TMPro;
using UnityEngine;

public class Calculator : MonoBehaviour
{
    public TextMeshProUGUI InputText;
    public TextMeshProUGUI resultText;
    public Animator resultAnim, inputAnim;
    private float result;
    private float input;
    private float input2;
    private string operation;
    private string current;
    private bool equalIsPressed, resultDisplayed, decimalIsPressed;

    private void Awake()
    {
        resultDisplayed = false;
        InputText.text = resultText.text = "";
    }

    public void ClickNumber(int val)
    {
        Debug.Log($" check val: {val}");
        if (!string.IsNullOrEmpty(current))
        {
            if (current.Length < 10)
            {
                current += val;
            }
        }
        else
        {
            current = val.ToString();
        }

        if (equalIsPressed && resultDisplayed)
        {
            resultAnim.SetTrigger("Hide");
            inputAnim.SetTrigger("Hide");
            resultDisplayed = false;

            if (operation == "")
            {
                InputText.text = "";
                input = 0;
            }
        }

        InputText.text += val; // $"{current}";
    }

    public void ClickOperation(string val)
    {
        Debug.Log($" ClickOperation val: {val}");
        if (input == 0)
        {
            Setcurrent();
            operation = val;
        }
        else
        {
            if (equalIsPressed)
            {
                equalIsPressed = false;
                operation = val;
                if (resultDisplayed)
                    InputText.text = input.ToString();
                input2 = 0;
            }
            else
            {
                /*if (operation.Equals(val, StringComparison.OrdinalIgnoreCase))
                {
                    Calculate();
                }
                else
                {
                    operation = val;
                    input2 = 0;
                }*/

                Calculate();
                operation = val;
                input2 = 0;
            }
        }
        
        if(InputText.text.EndsWith("+") || InputText.text.EndsWith("-") ||
            InputText.text.EndsWith("/") || InputText.text.EndsWith("*"))
        {
            if (!InputText.text.EndsWith(operation))
            {
                InputText.text = InputText.text.Remove(InputText.text.Length - 1, 1);
                InputText.text += operation;
            }
        } else
        {
            InputText.text += operation;
        }
            
    }

    public void ClickEqual(string val)
    {
        Debug.Log($" ClickEqual val: {val}");
        Calculate();
        equalIsPressed = true;
        operation = "";
    }

    private void Calculate()
    {
        if (input != 0 && !string.IsNullOrEmpty(operation))
        {
            Setcurrent();
            switch (operation)
            {
                case "+":
                    result = input + input2;
                    break;
                case "-":
                    result = input - input2;
                    break;
                case "*":
                    result = input * input2;
                    break;
                case "/":
                    result = input / input2;
                    break;
            }

            // show the result
            resultText.SetText(result.ToString());

            if (!resultDisplayed)
            {
                resultAnim.SetTrigger("Show");
                inputAnim.SetTrigger("Show");
                resultDisplayed = true;
            }

            // save the last result for next calculation
            input = result;
        }
    }

    private void Setcurrent()
    {
        if (!string.IsNullOrEmpty(current))
        {
            if (input == 0)
            {
                input = int.Parse(current);
            }
            else
            {
                input2 = int.Parse(current);
            }
            current = "";
        }
    }

    // clear all the inputs
    public void ClearInput()
    {
        resultAnim.SetTrigger("Hide");
        inputAnim.SetTrigger("Hide");
        resultDisplayed = false;
        current = "";
        input = 0;
        input2 = 0;
        result = 0;
        InputText.SetText("");
        resultText.SetText("");
    }
}
