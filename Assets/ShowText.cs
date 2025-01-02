using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowText : MonoBehaviour
{
    public Animator anim;
    public TextMeshProUGUI text;

    // show "Defend the fortress!" text during animation
    public void showOnScreenText()
    {
        text.enabled = true;
    }

    // hide "Defend the fortress!" text during animation
    public void hideOnScreenText()
    {
        text.enabled = false;
    }
}
