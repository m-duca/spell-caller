using DG.Tweening;
using UnityEngine;

namespace SpellCaller
{
    /// <summary>
    /// Responsável por gerar o efeito de tremor na câmera
    /// </summary>
    public class CameraShake : MonoBehaviour
    {
        // Não serializadas
        private Tween _shakeTween;
        private Tween _headBobTween;
        private Vector3 _initialLocalPos;
        private bool _stoppedHeadBob = false;

        private void Start() => _initialLocalPos = transform.localPosition;

        #region Padrão (Shake temporário)

        public void StartShake(float delayValue, float durationValue, float intensityValue)
        {
            _shakeTween?.Kill();

            _shakeTween = transform
                .DOShakePosition(durationValue, intensityValue, vibrato: 20, randomness: 90, snapping: false, fadeOut: true)
                .SetDelay(delayValue)
                .OnComplete(() => transform.localPosition = _initialLocalPos);
        }

        #endregion

        #region Contínuo (Shake infinito)

        public void StartContinuousShake(float intensityValue, float speedValue)
        {
            if (_shakeTween != null && _shakeTween.IsActive()) return;

            _shakeTween = transform
                .DOShakePosition(speedValue, intensityValue, vibrato: 10, randomness: 90, snapping: false, fadeOut: false)
                .SetLoops(-1, LoopType.Restart);
        }

        public void StopContinuousShake(float resetDurationValue)
        {
            if (_shakeTween != null)
            {
                _shakeTween.Kill();
                _shakeTween = null;
            }

            transform.DOLocalMove(_initialLocalPos, resetDurationValue);
        }

        #endregion

        #region HeadBob

        public void StartHeadBob(float intensityValue, float speedValue)
        {
            if (_headBobTween != null && _headBobTween.IsActive()) return;

            _headBobTween = transform
                .DOLocalMoveY(_initialLocalPos.y + intensityValue, 1f / speedValue)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo);

            _stoppedHeadBob = false;
        }

        public void StopHeadBob(float durationValue)
        {
            if (_stoppedHeadBob) return;

            if (_headBobTween != null)
            {
                _headBobTween.Kill();
                _headBobTween = null;
            }

            transform.DOLocalMove(_initialLocalPos, durationValue);

            _stoppedHeadBob = true;
        }

        #endregion
    }
}
