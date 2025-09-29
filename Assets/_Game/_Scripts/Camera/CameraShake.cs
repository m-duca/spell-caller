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

        public void StartShake(float delay, float duration, float intensity)
        {
            _shakeTween?.Kill();

            _shakeTween = transform
                .DOShakePosition(duration, intensity, vibrato: 20, randomness: 90, snapping: false, fadeOut: true)
                .SetDelay(delay)
                .OnComplete(() => transform.localPosition = _initialLocalPos);
        }

        #endregion

        #region Contínuo (Shake infinito)

        public void StartContinuousShake(float intensity, float speed = 0.1f)
        {
            if (_shakeTween != null && _shakeTween.IsActive()) return;

            _shakeTween = transform
                .DOShakePosition(speed, intensity, vibrato: 10, randomness: 90, snapping: false, fadeOut: false)
                .SetLoops(-1, LoopType.Restart);
        }

        public void StopContinuousShake(float resetDuration = 0.2f)
        {
            if (_shakeTween != null)
            {
                _shakeTween.Kill();
                _shakeTween = null;
            }

            transform.DOLocalMove(_initialLocalPos, resetDuration);
        }

        #endregion

        #region HeadBob

        public void StartHeadBob(float intensity, float speed)
        {
            if (_headBobTween != null && _headBobTween.IsActive()) return;

            _headBobTween = transform
                .DOLocalMoveY(_initialLocalPos.y + intensity, 1f / speed)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo);

            _stoppedHeadBob = false;
        }

        public void StopHeadBob(float duration)
        {
            if (_stoppedHeadBob) return;

            if (_headBobTween != null)
            {
                _headBobTween.Kill();
                _headBobTween = null;
            }

            transform.DOLocalMove(_initialLocalPos, duration);

            _stoppedHeadBob = true;
        }

        #endregion
    }
}
