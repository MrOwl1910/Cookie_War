using UnityEngine;
public class FollowCamera : MonoBehaviour
{
    [SerializeField] Transform Traget;
    [SerializeField] float smoothspeed = 0.125f;
    private void FixedUpdate()
    {
        Vector2 desiredPosition = (Vector2)Traget.position;
        Vector2 smoothPosition = Vector3.Lerp((Vector2)transform.position, desiredPosition, smoothspeed);

        transform.position = new Vector3(smoothPosition.x, smoothPosition.y, -10);
    }
}
