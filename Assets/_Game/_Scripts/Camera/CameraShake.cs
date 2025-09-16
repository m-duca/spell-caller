using System.Collections;
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
        private Coroutine _forcedShakeCoroutine;
        private Tween _headBobTween;
        private Vector3 _initialLocalPos;

        private void Start() => _initialLocalPos = transform.localPosition;

        #region  Padrão

        public void StartShake(float delay, float duration, float intensity)
        {
            StartCoroutine(Shake_Coroutine(delay, duration, intensity));
        }

        private IEnumerator Shake_Coroutine(float delay, float duration, float intensity)
        {
            yield return new WaitForSeconds(delay);

            float curTime = 0;

            while (curTime < duration)
            {
                curTime += Time.deltaTime;
                transform.eulerAngles = new Vector3(Random.Range(-intensity, intensity), Random.Range(-intensity, intensity), 0f);
                yield return null;
            }

            transform.eulerAngles = Vector3.zero;
        }

        #endregion

        #region  Contínuo

        public void StartContinuosShake(float intensity)
        {
            if (_forcedShakeCoroutine == null)
                _forcedShakeCoroutine = StartCoroutine(ContinuosShake_Coroutine(intensity));
            else
                Debug.LogError("Impossível começar um novo shake! Já está ocorrendo algum.");
        }

        private IEnumerator ContinuosShake_Coroutine(float intensity)
        {
            while (true)
            {
                transform.eulerAngles = new Vector3(Random.Range(-intensity, intensity), Random.Range(-intensity, intensity), 0f);

                yield return null;
            }
        }

        public void StopContinuosShake()
        {
            if (_forcedShakeCoroutine == null) return;

            StopCoroutine(_forcedShakeCoroutine);
            _forcedShakeCoroutine = null;

            transform.eulerAngles = Vector3.zero;
        }

        #endregion

        #region HeadBob

        public void StartHeadBob(float intensity, float speed)
        {
            if (_headBobTween != null && _headBobTween.IsActive()) return;

            _headBobTween = transform.DOLocalMoveY(_initialLocalPos.y + intensity, 1f / speed)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo);
        }

        public void StopHeadBob(float duration)
        {
            if (_headBobTween != null)
            {
                _headBobTween.Kill();
                _headBobTween = null;
            }

            transform.DOLocalMove(_initialLocalPos, duration);
        }

        #endregion
    }
}
