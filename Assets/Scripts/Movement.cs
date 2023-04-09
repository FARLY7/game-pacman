using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    public Rigidbody2D rigidBody {get; private set; }

    [SerializeField] private float _speed = 8.0f;
    [SerializeField] private float _speedMultiplier = 1.0f;

    [SerializeField] private Vector2 _initialDirection;

    [SerializeField] private LayerMask _obstacleLayer;

    public Vector2 direction { get; private set; }
    public Vector2 nextDirection { get; private set; }
    public Vector3 startingPosition { get; private set; }

    private void Awake()
    {
        this.rigidBody = GetComponent<Rigidbody2D>();
        this.startingPosition = this.transform.position;
    }

    private void Start()
    {
        ResetState();
        this.rigidBody.velocity = _initialDirection * _speed;
    }

    private void Update()
    {
        if(nextDirection != Vector2.zero) {
            SetDirection(this.nextDirection);
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = this.rigidBody.position;
        Vector2 translation = this .direction * _speed * _speedMultiplier * Time.fixedDeltaTime;
        this.rigidBody.MovePosition(position + translation);
    }

    public void ResetState()
    {
        _speedMultiplier = 1.0f;
        this.direction = _initialDirection;
        this.nextDirection = Vector2.zero;
        this.transform.position = this.startingPosition;
        this.rigidBody.isKinematic = false;
        this.enabled = true;
    }

    public void SetDirection(Vector2 direction, bool force = false)
    {
        if(force || !Occupied(direction)) {
            this.direction = direction;
            this.nextDirection = Vector2.zero;
        } else {
            this.nextDirection = direction;
        }
    }

    private bool Occupied(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(
                                this.rigidBody.position, 
                                Vector2.one * 0.75f,
                                0.0f,
                                direction,
                                1.5f,
                                _obstacleLayer);

        return hit.collider != null;
    }
}
