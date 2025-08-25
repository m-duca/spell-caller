using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

/// <summary>
/// Responsável pelo acionamento de spells via reconhecimento de voz
/// </summary>
public class SpellCaller : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private List<SpellData> _spellDatas;

    // Não serializadas
    private KeywordRecognizer _recognizer;
    private Dictionary<string, Action> _spellDict = new();

    private void Awake() => SetupSpells();

    private void OnApplicationQuit()
    {
        if (_recognizer.IsRunning)
            _recognizer.Stop();
    }

    private void CheckSpell(PhraseRecognizedEventArgs speechValue) => _spellDict[speechValue.text].Invoke();

    private void SetupSpells()
    {
        foreach (SpellData data in _spellDatas)
        {
            if (_spellDict.ContainsKey(data.Name))
            {
                Debug.LogError("Spell já adicionada ao dicionário!");
                return;
            }

            _spellDict.Add(data.Name, data.Cast);
        }

        SetupRecognizer();
    }

    private void SetupRecognizer()
    {
        _recognizer = new KeywordRecognizer(_spellDict.Keys.ToArray());
        _recognizer.OnPhraseRecognized += CheckSpell;
        _recognizer.Start();
    }
}
