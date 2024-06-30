using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class ParabolicMovement : MonoBehaviour
{
    public Tween tween;
    public UnityAction onEndMove;
    public void StopTweening()
    {
        tween.Kill();
        tween = null;
    }
    public void MoveParabolic(Vector3 start, Vector3 end, float height, float duration)
    {
        // Tạo tween với thời gian và hàm callback cho việc cập nhật vị trí
        tween = DOTween.To(() => 0f, t =>
        {
            Vector3 currentPos = CalculateParabola(start, end, height, t);
            transform.position = currentPos;
            transform.Rotate(1, 1, 1);
        }, 1f, duration).SetEase(Ease.Linear).OnComplete(OnEndMoveParabolic);
    }
    void OnEndMoveParabolic()
    {
        onEndMove?.Invoke();
    }

    Vector3 CalculateParabola(Vector3 start, Vector3 end, float height, float t)
    {
        // Interpolate giữa start và end
        Vector3 result = Vector3.Lerp(start, end, t);

        // Tính toán vị trí y theo parabol
        float parabolaT = t * 2 - 1; // biến đổi t từ [0, 1] thành [-1, 1]
        float parabolaY = (1 - parabolaT * parabolaT) * height; // giá trị parabol

        // Thêm giá trị y vào vị trí result
        result.y += parabolaY;

        return result;
    }
}
