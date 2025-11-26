using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    private const string Ground = "Ground";

    public bool IsGround { get; private set; }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(Ground))
            IsGround = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(Ground))
            IsGround = false;
    }
}
