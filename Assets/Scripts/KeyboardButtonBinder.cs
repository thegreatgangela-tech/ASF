using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class KeyboardButtonBinder : MonoBehaviour
{
    public Button restartButton;
    public Button quitButton;

    void Update()
    {
        if (Keyboard.current == null) return;

        if (Keyboard.current.rKey.wasPressedThisFrame)
            restartButton.onClick.Invoke();

        if (Keyboard.current.qKey.wasPressedThisFrame)
            quitButton.onClick.Invoke();
    }
}