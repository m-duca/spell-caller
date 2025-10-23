using UnityEngine;

namespace SpellCaller
{
    /// <summary>
    /// Responsável por exibir / esconder o cursor na tela
    /// </summary> <summary>
    public class CursorHandler : MonoBehaviour
    {
        [Header("Parâmetros")]
        [SerializeField] private bool _isShowing;

        private void OnValidate()
        {
            if (_isShowing) ShowCursor();
            else HideCursor();
        }

        private void ShowCursor()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;    
        }

        private void HideCursor()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
