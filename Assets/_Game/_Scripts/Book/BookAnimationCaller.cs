using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace SpellCaller
{   
    /// <summary>
    /// Controla o acionamento das animações do livro de magias
    /// </summary>
    public class BookAnimationCaller : MonoBehaviour
    {
        [Header("Parâmetros Mão")]
        [SerializeField] private float _startDelay;
        [SerializeField] private float _meshMoveDistance;
        [SerializeField] private float _meshMoveDuration;
        [SerializeField] private Ease _easeType = Ease.InOutSine;
        [SerializeField] private float _hideUIDelay;

        [Header("Referências")]
        [SerializeField] private Transform _playerMesh;

        // Não serializadas
        private Animator _anim;
        private BookUI _bookUI;

        private const string ANIM_PARAM_FLIP_FORWARDS = "_flipForwards";
        private const string ANIM_PARAM_FLIP_BACKWARDS = "_flipBackwards";

        private bool _isFlipping = false;
        private Tween _currentHandTween;

        private void Start()
        {
            _anim = GetComponent<Animator>();
            _bookUI = GetComponent<BookUI>();
        }

        public void PlayFlip(int incrementValue)
        {
            if (incrementValue == 0 || _isFlipping) return;
            _isFlipping = true;

            StartCoroutine(HideUIContent_Coroutine());

            if (incrementValue == 1)
                _anim.SetTrigger(ANIM_PARAM_FLIP_FORWARDS);
            else // -1
                _anim.SetTrigger(ANIM_PARAM_FLIP_BACKWARDS);

            if (_playerMesh != null)
                StartCoroutine(CallHandAnimation_Coroutine());
        }

        private IEnumerator CallHandAnimation_Coroutine()
        {
            yield return new WaitForSeconds(_startDelay);
            yield return AnimateHand_Coroutine();
            _isFlipping = false;
        }

        private IEnumerator AnimateHand_Coroutine()
        {
            Vector3 originalPos = _playerMesh.localPosition;
            Vector3 loweredPos = originalPos - new Vector3(0, _meshMoveDistance, 0);

            _currentHandTween?.Kill();

            _currentHandTween = _playerMesh.DOLocalMove(loweredPos, _meshMoveDuration)
                .SetEase(_easeType);

            yield return _currentHandTween.WaitForCompletion();

            _currentHandTween = _playerMesh.DOLocalMove(originalPos, _meshMoveDuration)
                .SetEase(_easeType);

            yield return _currentHandTween.WaitForCompletion();

            _bookUI.SetContentVisualization(true);
        }

        private IEnumerator HideUIContent_Coroutine()
        {
            yield return new WaitForSeconds(_hideUIDelay);
            _bookUI.SetContentVisualization(false);
        }
    }
}
