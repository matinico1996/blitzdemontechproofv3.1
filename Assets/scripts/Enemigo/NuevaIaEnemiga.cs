using UnityEngine;

public class NuevaIaEnemiga : MonoBehaviour
{
    [Header("Detección y Movimiento")]
    public float radioBusqueda = 5f;
    public LayerMask capaJugador;
    public Transform transformJugador;
    public float enemySpeed = 3f;  // Velocidad fija de persecución

    [Header("Salto")]
    public float fuerzSalto = 6.5f;               // Impulso del salto
    public float jumpHeightThreshold = 2.5f;      // Altura máxima que puede detectar
    public float horizontalThreshold = 2.5f;      // Distancia horizontal máxima para saltar
    public LayerMask capaPlataformas;             // Layer de tus plataformas
    public float tiempoEntreSaltos = 1f;          // Cooldown entre saltos
    public float maxVelocidadY = 7f;              // Límite de velocidad vertical

    [Header("Detección de Obstáculos")]
    public LayerMask capaObstaculos;              // Layer de muros/objetos sólidos
    public float wallCheckDistance = 0.6f;        // Distancia de raycast horizontal

    private Rigidbody2D fisica;
    private float tiempoProximoSalto = 0f;

    void Start()
    {
        fisica = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // 1. Detectar al jugador en rango
        Collider2D jugadorCollider = Physics2D.OverlapCircle(transform.position, radioBusqueda, capaJugador);
        if (jugadorCollider == null)
        {
            // Sin jugador: detener X, dejar Y libre
            fisica.velocity = new Vector2(0, fisica.velocity.y);
            return;
        }

        // 2. Calcular vector hacia el jugador
        Vector2 diff = transformJugador.position - transform.position;
        float signX = Mathf.Sign(diff.x);

        // 3. Raycast horizontal para detectar paredes/obstáculos
        Vector2 origen = (Vector2)transform.position + Vector2.up * 0.2f;
        Vector2 dirHorizontal = Vector2.right * signX;
        RaycastHit2D pared = Physics2D.Raycast(origen, dirHorizontal, wallCheckDistance, capaObstaculos);
        Debug.DrawRay(origen, dirHorizontal * wallCheckDistance, pared ? Color.magenta : Color.gray);

        if (pared.collider != null)
        {
            // Si hay pared, frenar componente X
            fisica.velocity = new Vector2(0, fisica.velocity.y);

            // Intentar saltar sobre el obstáculo si está al alcance
            if (IsGrounded() && Time.time >= tiempoProximoSalto)
            {
                fisica.velocity = new Vector2(fisica.velocity.x, 0f);
                fisica.AddForce(Vector2.up * fuerzSalto, ForceMode2D.Impulse);
                tiempoProximoSalto = Time.time + tiempoEntreSaltos;
            }

            return; // saltamos o quedamos parados este frame
        }

        // 4. Movimiento horizontal constante
        fisica.velocity = new Vector2(signX * enemySpeed, fisica.velocity.y);

        // 5. Salto si el jugador está encima y cerca
        float dy = diff.y;
        float dx = Mathf.Abs(diff.x);
        if (dy > 0.5f
            && dx <= horizontalThreshold
            && Time.time >= tiempoProximoSalto
            && IsGrounded())
        {
            Vector2 rayOrigin = (Vector2)transform.position + Vector2.up * 0.5f;
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up, jumpHeightThreshold, capaPlataformas);
            Debug.DrawRay(rayOrigin, Vector2.up * jumpHeightThreshold, Color.green);

            if (hit.collider != null)
            {
                fisica.velocity = new Vector2(fisica.velocity.x, 0f);
                fisica.AddForce(Vector2.up * fuerzSalto, ForceMode2D.Impulse);
                tiempoProximoSalto = Time.time + tiempoEntreSaltos;
            }
        }

        // 6. Limitar velocidad vertical
        if (fisica.velocity.y > maxVelocidadY)
            fisica.velocity = new Vector2(fisica.velocity.x, maxVelocidadY);
    }

    private bool IsGrounded()
    {
        RaycastHit2D suelo = Physics2D.Raycast(transform.position, Vector2.down, 1f, capaPlataformas);
        Debug.DrawRay(transform.position, Vector2.down * 1f, Color.red);
        return suelo.collider != null;
    }

    private void OnDrawGizmosSelected()
    {
        // Rango de detección del jugador
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioBusqueda);

        // Zona de salto válida
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(
            transform.position + Vector3.up * (jumpHeightThreshold / 2),
            new Vector3(horizontalThreshold * 2, jumpHeightThreshold, 0.1f)
        );

        // Línea de detección de pared
        Vector3 origen = transform.position + Vector3.up * 0.2f;
        Vector3 destino = origen + Vector3.right * Mathf.Sign(transformJugador.position.x - transform.position.x) * wallCheckDistance;
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(origen, destino);
    }
}
