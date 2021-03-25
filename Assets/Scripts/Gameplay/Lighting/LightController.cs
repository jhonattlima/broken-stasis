using UnityEngine;

namespace Gameplay.Lighting
{
    [RequireComponent(typeof(Light))]
    [RequireComponent(typeof(Animator))]
    public class LightController : MonoBehaviour
    {
        private Animator _animator;
        private Light _light;

        [SerializeField] private LightEnum _initialLightState;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _light = GetComponent<Light>();

            SetLightState(_initialLightState);
        }

        public void SetLightState(LightEnum p_lightState)
        {
            _animator.Play(p_lightState.ToString());
        }
    }
}
