using TMPro;
using UnityEngine;

public class DebuControler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textDebug;

    public void Log(string log)
    {
        textDebug.text += $"\n{log}";
    }
}