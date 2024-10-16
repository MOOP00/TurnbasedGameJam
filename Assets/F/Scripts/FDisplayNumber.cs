using TMPro;
using UnityEngine;

public class FDisplayNumber : MonoBehaviour
{
    public TextMeshPro numberText;
    public GameObject num;
    public FDiceFaceChecker dch;

    void Update()
    {
        if(dch.moving == false)
        {
            num.SetActive(true);
            numberText.text = dch.value.ToString();
        }
        else
        {
            num.SetActive(false);
        }
        transform.up = Vector3.up;
    }
}