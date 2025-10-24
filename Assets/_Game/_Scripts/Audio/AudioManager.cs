using FMOD.Studio;
using FMODUnity;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using STOP_MODE = FMOD.Studio.STOP_MODE;

namespace SpellCaller
{
    /// <summary>
    /// Responsável pela execução sonora do jogo, tocando SFX, músicas e outras faixas sonoras 
    /// </summary>
    public class AudioManager : MonoBehaviour
    {
        [Header("Parâmetros")]

        [Header("Volumes Iniciais")]
        [SerializeField, Range(0f, 1f)] private float _masterDefaultVolume;
        //[SerializeField, Range(0f, 1f)] private float _cutsceneDefaultVolume;
        [SerializeField, Range(0f, 1f)] private float _ambienceDefaultVolume;
        [SerializeField, Range(0f, 1f)] private float _musicDefaultVolume;
        [SerializeField, Range(0f, 1f)] private float _sfxDefaultVolume;
        //[SerializeField, Range(0f, 1f)] private float _uiDefaultVolume;
        //[SerializeField, Range(0f, 1f)] private float _voiceDefaultVolume;

        /*
        [Header("Referências")]
        [SerializeField] private Slider _masterSlider;
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _ambienceSlider;
        [SerializeField] private Slider _sfxSlider;
        [SerializeField] private Slider _uiSlider;
        [SerializeField] private Slider _cutsceneSlider;
        [SerializeField] private Slider _voiceSlider;
        */

        [Header("FMOD Events")]
        [SerializeField] private EventReference[] _environmentEvents;
        [SerializeField] private EventReference[] _musicEvents;
        //[SerializeField] private EventReference[] _cutsceneEvents;

        // Não serializadas
        private EventInstance _ambienceInstance;
        private EventInstance _musicInstance;
        //private EventInstance _cutsceneInstance;

        private Bus _masterBus;
        //private Bus _cutsceneBus;
        private Bus _ambienceBus;
        private Bus _musicBus;
        private Bus _sfxBus;
        //private Bus _uiBus;
        //private Bus _voiceBus;

        private const string BUS_MASTER = "bus:/";
        //private const string BUS_CUTSCENE = "bus:/Cutscene";
        private const string BUS_AMBIENCE = "bus:/Ambience";
        private const string BUS_MUSIC = "bus:/Music";
        private const string BUS_SFX = "bus:/SFX";
        //private const string BUS_UI = "bus:/UI";
        //private const string BUS_VOICE = "bus:/Voice";

        private MusicTrackType _lastMusicTrack;

        private bool _isInitialVolumeSetted = false;

        public static AudioManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                return;
            }

            Destroy(gameObject);
        }

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(0.1f);

            _masterBus = RuntimeManager.GetBus(BUS_MASTER);
            _musicBus = RuntimeManager.GetBus(BUS_MUSIC);
            _ambienceBus = RuntimeManager.GetBus(BUS_AMBIENCE);
            _sfxBus = RuntimeManager.GetBus(BUS_SFX);
            //_uiBus = RuntimeManager.GetBus(BUS_UI);
            //_cutsceneBus = RuntimeManager.GetBus(BUS_CUTSCENE);
            //_voiceBus = RuntimeManager.GetBus(BUS_VOICE);

            //if (_isInitialVolumeSetted)
            //SetPreviousVolumes();
            //else
            SetInitialVolumes();
        }

        //private void OnApplicationQuit() => SaveVolumesData();

        #region Volume

        public void SetInitialVolumes()
        {
            SetMasterBus(_masterDefaultVolume);
            SetMusicBus(_musicDefaultVolume);
            SetAmbienceBus(_ambienceDefaultVolume);
            SetSFXBus(_sfxDefaultVolume);
            //SetUIBus(_uiDefaultVolume);
            //SetCutsceneBus(_cutsceneDefaultVolume);
            //SetVoiceBus(_voiceDefaultVolume);

            /*
            _masterSlider.value = _masterDefaultVolume;
            _musicSlider.value = _musicDefaultVolume;
            _ambienceSlider.value = _ambienceDefaultVolume;
            _sfxSlider.value = _sfxDefaultVolume;
            _uiSlider.value = _uiDefaultVolume;
            _cutsceneSlider.value = _cutsceneDefaultVolume;
            _voiceSlider.value = _voiceDefaultVolume;
            */

            _isInitialVolumeSetted = true;
        }

        /*
        private void SetPreviousVolumes()
        {
            SetMasterBus(DataManager.Instance.GetVolume(VolumeType.Master));
            SetMusicBus(DataManager.Instance.GetVolume(VolumeType.Music));
            SetAmbienceBus(DataManager.Instance.GetVolume(VolumeType.Ambience));
            SetSFXBus(DataManager.Instance.GetVolume(VolumeType.SFX));
            SetUIBus(DataManager.Instance.GetVolume(VolumeType.UI));
            SetCutsceneBus(DataManager.Instance.GetVolume(VolumeType.Cutscene));
            SetVoiceBus(DataManager.Instance.GetVolume(VolumeType.Voice));

            _masterSlider.value = DataManager.Instance.GetVolume(VolumeType.Master);
            _musicSlider.value = DataManager.Instance.GetVolume(VolumeType.Music);
            _ambienceSlider.value = DataManager.Instance.GetVolume(VolumeType.Ambience);
            _sfxSlider.value = DataManager.Instance.GetVolume(VolumeType.SFX);
            _uiSlider.value = DataManager.Instance.GetVolume(VolumeType.UI);
            _cutsceneSlider.value = DataManager.Instance.GetVolume(VolumeType.Cutscene);
            _voiceSlider.value = DataManager.Instance.GetVolume(VolumeType.Voice);
        }
        */

        /*
        public void SaveVolumesData()
        {
            _masterBus.getVolume(out float masterValue);
            _musicBus.getVolume(out float musicValue);
            _ambienceBus.getVolume(out float ambienceValue);
            _sfxBus.getVolume(out float sfxValue);
            _uiBus.getVolume(out float uiValue);
            _cutsceneBus.getVolume(out float cutsceneValue);
            _voiceBus.getVolume(out float voiceValue);

            List<float> volumeValues = new()
            {
                masterValue,
                musicValue,
                ambienceValue,
                sfxValue,
                uiValue,
                cutsceneValue,
                voiceValue
            };
        }
        */

        // Alterando Valor dos Volumes
        public void SetMasterBus(float volumeValue)
        {
            float clamped = Mathf.Clamp01(volumeValue);
            _masterBus.setVolume(clamped);
        }

        public void SetMusicBus(float volumeValue)
        {
            float clamped = Mathf.Clamp01(volumeValue);
            _musicBus.setVolume(clamped);
        }

        public void SetAmbienceBus(float volumeValue)
        {
            float clamped = Mathf.Clamp01(volumeValue);
            _ambienceBus.setVolume(clamped);
        }

        public void SetSFXBus(float volumeValue)
        {
            float clamped = Mathf.Clamp01(volumeValue);
            _sfxBus.setVolume(clamped);
        }

        /*
        public void SetUIBus(float volumeValue)
        {
            float clamped = Mathf.Clamp01(volumeValue);
            _uiBus.setVolume(clamped);
        }

        public void SetCutsceneBus(float volumeValue)
        {
            float clamped = Mathf.Clamp01(volumeValue);
            _cutsceneBus.setVolume(clamped);
        }

        public void SetVoiceBus(float volumeValue)
        {
            float clamped = Mathf.Clamp01(volumeValue);
            _voiceBus.setVolume(clamped);
        }
        */

        /*
        public void OnMasterSlider(Slider sliderValue) => SetMasterBus(sliderValue.value);
        public void OnMusicSlider(Slider sliderValue) => SetMusicBus(sliderValue.value);
        public void OnEnvironmentSlider(Slider sliderValue) => SetEnvironmentBus(sliderValue.value);
        public void OnSFXSlider(Slider sliderValue) => SetSFXBus(sliderValue.value);
        public void OnUISlider(Slider sliderValue) => SetUIBus(sliderValue.value);
        public void OnCutsceneSlider(Slider sliderValue) => SetCutsceneBus(sliderValue.value);
        public void OnVoiceSlider(Slider sliderValue) => SetVoiceBus(sliderValue.value);
        */

        #endregion

        #region SFX

        public void PlayOneShot(EventReference eventRefValue, Vector3? positionValue = null)
        {
            if (positionValue.HasValue)
                RuntimeManager.PlayOneShot(eventRefValue, positionValue.Value);
            else
                RuntimeManager.PlayOneShot(eventRefValue);
        }

        public void PlayOneShotAttached(EventReference eventRefValue, GameObject targetValue)
        {
            RuntimeManager.PlayOneShotAttached(eventRefValue, targetValue);
        }

        private EventInstance CreateEventInstance(EventReference eventRefValue)
        {
            return RuntimeManager.CreateInstance(eventRefValue);
        }

        #endregion

        #region Ambiente

        public void PlayAmbience(AmbienceTrackType trackValue)
        {
            EventReference ambience = _environmentEvents[(int)trackValue];

            if (ambience.IsNull)
            {
                Debug.LogError("Faixa de Ambiente não encontrada!");
                return;
            }

            PlayTrack(ref _ambienceInstance, ambience);
        }

        public void StopAmbience()
        {
            StopTrack(ref _ambienceInstance);
        }

        public void PauseAmbience() => PauseTrack(ref _ambienceInstance);

        #endregion

        #region Música

        public void PlayMusic(MusicTrackType trackValue)
        {
            EventReference music = _musicEvents[(int)trackValue];

            if (music.IsNull)
            {
                Debug.LogError("Música não encontrada!");
                return;
            }

            PlayTrack(ref _musicInstance, music);
        }

        public bool IsLastMusicTrack(MusicTrackType trackValue) => _lastMusicTrack == trackValue;

        public void StopMusic() => StopTrack(ref _musicInstance);

        public void PauseMusic() => PauseTrack(ref _musicInstance);

        public void ResumeMusic() => ResumeTrack(ref _musicInstance);

        #endregion

        #region Cutscene
        /*
        public void PlayCutscene(CutsceneTrackType trackValue)
        {
            EventReference cutscene = _cutsceneEvents[(int)trackValue];

            if (cutscene.IsNull)
            {
                Debug.LogError("Cutscene não encontrada!");
                return;
            }

            PlayTrack(ref _cutsceneInstance, cutscene);
        }
        
        public void StopCutscene() => StopTrack(ref _cutsceneInstance);

        public void PauseCutscene() => PauseTrack(ref _cutsceneInstance);

        public void ResumeCutscene() => ResumeTrack(ref _cutsceneInstance);
        */
        #endregion

        #region General

        /*
        public void SetGlobalParameter(FMODParametersType type, float value)
        {
            RuntimeManager.StudioSystem.setParameterByName(_parametersDict[type], value);
        }

        public float GetGlobalParameterValue(FMODParametersType type)
        {
            float value;
            RuntimeManager.StudioSystem.getParameterByName(_parametersDict[type], out value);
            return value;
        }
        */

        private void StopTrack(ref EventInstance instanceValue)
        {
            if (!instanceValue.isValid()) return;

            instanceValue.stop(STOP_MODE.ALLOWFADEOUT);
            instanceValue.release();
        }

        private void PauseTrack(ref EventInstance instanceValue)
        {
            if (!instanceValue.isValid()) return;

            instanceValue.setPaused(true);
        }

        private void ResumeTrack(ref EventInstance instanceValue)
        {
            if (!instanceValue.isValid()) return;

            instanceValue.setPaused(false);
        }

        private void PlayTrack(ref EventInstance instanceValue, EventReference referenceValue)
        {
            StopTrack(ref instanceValue);

            instanceValue = CreateEventInstance(referenceValue);
            instanceValue.start();
        }

        #endregion
    }
}
