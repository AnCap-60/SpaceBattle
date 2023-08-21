using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ScoreView : MonoBehaviour
{
    VisualElement root;

    Label moneyLabel;

    public void SetVisualElement(VisualElement element)
    {
        root = element;
        moneyLabel = root.Q<Label>("coinAmount");
        OnMoneyChanged(0);
    }

    public void OnMoneyChanged(int newNum)
    {
        //Debug.Log(moneyLabel);
        moneyLabel.text = newNum.ToString();
    }
}
