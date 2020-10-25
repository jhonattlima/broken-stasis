using UnityEngine;

namespace Player
{
    public class PlayerVision: MonoBehaviour
    {
        [SerializeField] private Transform _headPosition;
        [SerializeField] private LayerMask _layersToDetect;

        private void Awake()
        {
            if (_headPosition == null)
                throw new MissingComponentException("HeadPosition not found in PlayerVision!");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(GameInternalTags.ENEMY) && HasDirectViewOfHiddenObject(other.gameObject))
                other.gameObject.GetComponent<SkinnedMeshRenderer>().enabled = true;
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag(GameInternalTags.ENEMY))
            {
                if(HasDirectViewOfHiddenObject(other.gameObject))
                    other.gameObject.GetComponent<SkinnedMeshRenderer>().enabled = true;
                else
                    other.gameObject.GetComponent<SkinnedMeshRenderer>().enabled = false;
            } 
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

        
            if(Physics.Raycast(_headPosition.position,__direction,out __hit, 50f, _layersToDetect))
            {
                // if(__hit.collider.CompareTag(GameInternalTags.ENEMY))
                //     Debug.DrawRay(_headPosition.position, __direction, Color.magenta);
                
                return __hit.collider.CompareTag(GameInternalTags.ENEMY);
            }

            return false;
        }
    }
}
