using UnityEngine;

namespace SpellCaller
{
    /// <summary>
    /// Controla o acionamento das animações do livro de magias
    /// </summary>
    public class BookAnimationCaller : MonoBehaviour
    {
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
        }
    }
}
