using UnityEngine;
using DG.Tweening;
using System.Collections;

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

        [Header("Referências")]
        [SerializeField] private Transform _playerMesh;

        // Não serializadas
        private Animator _anim;

        private const string ANIM_PARAM_FLIP_FORWARDS = "_flipForwards";
        private const string ANIM_PARAM_FLIP_BACKWARDS = "_flipBackwards";

        private void Start() => _anim = GetComponent<Animator>();

        public void PlayFlip(int incrementValue)
        {
            if (incrementValue == 0) return;

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
            StartCoroutine(AnimateHand_Coroutine());
        }

        private IEnumerator AnimateHand_Coroutine()
        {
            Vector3 originalPos = _playerMesh.localPosition;
            Vector3 loweredPos = originalPos - new Vector3(0, _meshMoveDistance, 0);

            // Move a mão para baixo
            _playerMesh.DOLocalMove(loweredPos, _meshMoveDuration)
                .SetEase(_easeType);

            yield return new WaitForSeconds(_meshMoveDuration + 0.05f);

            // Volta à posição original
            _playerMesh.DOLocalMove(originalPos, _meshMoveDuration)
                .SetEase(_easeType);
        }
    }
}
