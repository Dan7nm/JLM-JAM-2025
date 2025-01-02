using UnityEngine;
using TMPro; // For TextMeshPro support

public class HelpToggleUI : MonoBehaviour
{
    public TextMeshProUGUI instructionText; // Reference to the instruction text

    private bool isHelpVisible = false; // State of the instruction text

    public void ToggleHelp()
    {
        isHelpVisible = !isHelpVisible; // Toggle visibility state
        instructionText.gameObject.SetActive(isHelpVisible); // Show/Hide instruction text
    }
}