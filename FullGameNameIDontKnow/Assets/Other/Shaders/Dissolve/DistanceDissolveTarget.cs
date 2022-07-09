using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceDissolveTarget : MonoBehaviour
{
    public Transform m_objectToTrack;
    public LayerMask whoIsTarget;
    public float updateRange;

    private Material m_materialRef = null;
    private Renderer m_renderer = null;
    private bool objectinTrack;

    public Renderer Renderer
    {
        get
        {
            if (m_renderer == null)
                m_renderer = this.GetComponent<Renderer>();
            return m_renderer;
        }
    }

    public Material MaterialRef
    {
        get
        {
            if (m_materialRef == null)
                m_materialRef = Renderer.material;
            return m_materialRef;
        }
    }

    private void Awake() {
        m_renderer = this.GetComponent<Renderer>();
        m_materialRef = m_renderer.material;
    }

    private void Update() {
        objectinTrack = Physics.CheckSphere(transform.position, updateRange, whoIsTarget);

        if (!objectinTrack)
            return;

        if (m_objectToTrack != null)
            MaterialRef.SetVector("_Position", m_objectToTrack.position);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, updateRange);
    }
}
