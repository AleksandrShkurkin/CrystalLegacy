using System.Collections;
using UnityEngine;

namespace GameManagement
{
    public class RecourceZone : MonoBehaviour
    {
        public MaterialType materialType;
        private readonly CooldownManager _cooldownManager = new CooldownManager(3f);
        private Coroutine _collectionCoroutine;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            _collectionCoroutine ??= StartCoroutine(CollectResources());
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            if (_collectionCoroutine == null) return;
            StopCoroutine(_collectionCoroutine);
            _collectionCoroutine = null;
        }

        private IEnumerator CollectResources()
        {
            while (true)
            {
                if (_cooldownManager.IsCooldownFinished(Time.time))
                {
                    _cooldownManager.InitiateCooldown(Time.time);
                    Inventory.Instance.AddMaterial(materialType, 1);
                }
            
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
