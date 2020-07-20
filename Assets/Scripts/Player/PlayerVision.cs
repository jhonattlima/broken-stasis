using UnityEngine;

namespace Player
{
    public class PlayerVision: MonoBehaviour
    {
        [SerializeField] private Transform _headPosition;
        [SerializeField] private LayerMask _layersToDetect;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(GameInternalTags.ENEMY) && HasDirectViewOfHiddenObject(other.gameObject))
                other.gameObject.GetComponent<SkinnedMeshRenderer>().enabled = true;
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag(GameInternalTags.ENEMY) && HasDirectViewOfHiddenObject(other.gameObject))
                other.gameObject.GetComponent<SkinnedMeshRenderer>().enabled = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(GameInternalTags.ENEMY))
                other.gameObject.GetComponent<SkinnedMeshRenderer>().enabled = false;
        }

        private bool HasDirectViewOfHiddenObject(GameObject p_hiddenGameObject)
        {
            RaycastHit __hit;
            Vector3 __toPosition = p_hiddenGameObject.transform.position;
            Vector3 __direction = __toPosition - _headPosition.position;

            // Debug.DrawRay(_headPosition.position, __direction, Color.magenta);
        
            if(Physics.Raycast(_headPosition.position,__direction,out __hit, 50f, _layersToDetect))
                return __hit.collider.CompareTag(GameInternalTags.ENEMY);

            return false;
        }
    }
}
