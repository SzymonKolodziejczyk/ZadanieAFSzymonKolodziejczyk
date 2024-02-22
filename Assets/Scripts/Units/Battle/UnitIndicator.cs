using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AFSInterview.Battle
{
    public class UnitIndicator : MonoBehaviour
    {
        [SerializeField] private float yOffset;

        public void Show(Vector3 position)
        {
            position.y += yOffset;
            transform.position = position;
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}

