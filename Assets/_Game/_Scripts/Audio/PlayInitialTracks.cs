using System.Collections;
using FMODUnity;
using UnityEngine;

namespace SpellCaller
{
    /// <summary>
    /// Script simples para tocar música / faixa de ambientes iniciais
    /// </summary>
    public class PlayInitialTracks : MonoBehaviour
    {
        [Header("Pârametros")]
        [SerializeField] private MusicTrackType _initialMusic;
        [SerializeField] private AmbienceTrackType _initialAmbience;
        [SerializeField] private float _delay;

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(_delay);

            if (AudioManager.Instance == null)
            {
                Debug.LogError("AudioManager não foi encontrado!");
            }
            else
            {
                AudioManager.Instance.PlayMusic(_initialMusic);
                AudioManager.Instance.PlayAmbience(_initialAmbience);
            }

        }
    }
}
