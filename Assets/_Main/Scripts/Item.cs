using TigerForge;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    public float fixScale = 1.1f;
    // Start is called before the first frame update
    [SerializeField] eItemState state;
    public UnityAction onBeGrab;
    public eItemState State
    {
        set
        {
            state = value;
            switch (state)
            {
                case eItemState.flying:
                    rig.isKinematic = true;
                    break;

                case eItemState.beGrab:
                    GetComponent<ParabolicMovement>().StopTweening();
                    transform.localPosition = Vector3.zero;
                    rig.isKinematic = true;
                    break;

                case eItemState.free:
                    break;
            }
        }
        get => state;
    }

    Renderer ren;
    Rigidbody _rig;
    Rigidbody rig
    {
        get
        {
            if (_rig == null)
                _rig = GetComponent<Rigidbody>();
            return _rig;
        }
    }

    public float force = 10;
    public float longg;

    void Start()
    {
        State = eItemState.flying;
        transform.localScale = transform.localScale * fixScale;

        ren = GetComponent<Renderer>();
        //ren.material.shader = Shader.Find("Standard");//Shader.Find("Unlit/Texture");

        EventManager.StartListening(EventKey.OnWin.ToString(), ()=> gameObject.SetActive(false));

        EventManager.StartListening(EventKey.OnLose.ToString(), () => gameObject.SetActive(false));

    }
    public void BeGrab(Transform parent)
    {
        transform.parent = parent;
        State = eItemState.beGrab;

        onBeGrab?.Invoke();
    }
    public void Free(Vector3 dir)
    {
        State = eItemState.free;
        transform.parent = null;
        rig.isKinematic = false;

        rig.AddForce(dir * 5000);

    }
    public void ToggleOutline(bool isTrue)
    {
        //if (ren != null)
            //ren.material.shader = isTrue ? Shader.Find("Standard") : Shader.Find("Unlit/Texture");
    }
}
public enum eItemState
{
    flying, beGrab, free
}
