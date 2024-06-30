using UnityEngine;

public class Raycast : MonoBehaviour
{
    public LineRenderer line;
    [SerializeField] Transform endPos;
    [SerializeField] Vector3 endLine;

    public Collider raycastingObj = null;
    RaycastHit hit;
    [SerializeField] bool isActive = true;
    public bool IsActive
    {
        set
        {
            isActive = value;
            line.enabled = value;
        }
        get => isActive;
    }
    private void Start()
    {
        line.positionCount = 2;
    }
    public virtual void Update()
    {
        if (!IsActive)
        {
            return;
        }

        if (IsRaycast(out Vector3 point))
        {
            endLine = point;
        }
        else
            raycastingObj = null;

        endLine = endPos.position;

        line.SetPosition(0, transform.position);
        line.SetPosition(1, endPos.transform.position);
    }
    bool IsRaycast(out Vector3 point)
    {
        point = Vector3.zero;
        if (Physics.Raycast(transform.position, (endPos.position - transform.position).normalized, out hit, 100))
        {
            if (hit.collider != null)
            {
                point = hit.point;
                raycastingObj = hit.collider;
                return true;
            }
        }
        return false;
    }
    public Vector3 Direction()
    {
        return endPos.position - transform.position;
    }
    public void SetEndPos(Vector3 pos)
    {
        endPos.position = pos;
    }
}
