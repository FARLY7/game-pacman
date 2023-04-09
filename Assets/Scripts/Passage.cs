using UnityEngine;

public class Passage : MonoBehaviour
{
    [SerializeField] private Transform _connection;

    private void OnTriggerEnter2D(Collider2D other)
    {
        /* Move whatever collided with it to the connection */
        other.gameObject.transform.position = _connection.position;
    }
}
