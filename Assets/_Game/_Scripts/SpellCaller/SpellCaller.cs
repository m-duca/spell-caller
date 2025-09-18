using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

namespace SpellCaller
{
    /// <summary>
    /// Responsável pelo acionamento de spells via reconhecimento de voz
    /// </summary>
    public class SpellCaller : MonoBehaviour
    {
        // Não serializadas
        private KeywordRecognizer _recognizer;
        private Dictionary<string, Action> _spellDict = new();

        private IEnumerator Start()
        {
            yield return new WaitForEndOfFrame();

            SetupSpells();
        }

        private void OnApplicationQuit()
        {
            if (_recognizer.IsRunning)
                _recognizer.Stop();
        }

        private void CheckSpell(PhraseRecognizedEventArgs speechValue) => _spellDict[speechValue.text].Invoke();

        private void SetupSpells()
        {
            List<SpellData> _spells = PlayerManager.Instance.PlayerSpells.SpellDatas;

            foreach (SpellData data in _spells)
            {
                if (_spellDict.ContainsKey(data.Name))
                {
                    Debug.LogError("Spell já adicionada ao dicionário!");
                    return;
                }

                _spellDict.Add(data.Name, data.Cast);
                Debug.Log($"Adicionando spell: {data.Name}");
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
}