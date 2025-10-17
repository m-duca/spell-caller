using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SpellCaller
{
    /// <summary>
    /// Responsável por fazer a troca dos elementos de UI presentes no livro de magias
    /// </summary>
    public class BookUI : MonoBehaviour
    {
        [Header("Pârametros")]
        [SerializeField] private TextMeshProUGUI _spellNameTxt;
        [SerializeField] private Image _spellIconImg;

        public void ChangeContent(SpellData dataValue)
        {
            _spellNameTxt.text = dataValue.Name;
            _spellIconImg.sprite = dataValue.IconSprite;
        }

        public void SetContentVisualization(bool value)
        {
            _spellNameTxt.enabled = value;
            _spellIconImg.enabled = value;
        }
    }
}
