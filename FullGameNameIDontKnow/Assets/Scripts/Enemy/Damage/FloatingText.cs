using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private Vector3 offset = new Vector3(0, 2, 0);
    [SerializeField] private Vector3 randomizeIntensity = new Vector3(0.5f, 0, 0);

    private void Start() {
        transform.localPosition += offset;
        transform.localPosition += new Vector3(
            Random.Range(-randomizeIntensity.x, randomizeIntensity.x),
            Random.Range(-randomizeIntensity.y, randomizeIntensity.y),
            Random.Range(-randomizeIntensity.z, randomizeIntensity.z)
        );
    }

    public void DestroyGameObject() {
        Destroy(gameObject);
    }

    /*
Vector3 impactVelocity = collision.relativeVelocity;
                float damage = Mathf.Max(0f, impactVelocity.magnitude) + PlayerPrefs.GetInt("PlusDamage");
                damage += PlayerPrefs.GetInt("PreocentageUpgradeDamage");
    */
}
