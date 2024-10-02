using System.Collections;
using UnityEngine;

public class DownAttackMonster : MonoBehaviour
{
    [SerializeField] float attackDistance = 1f;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] float fallSpeed = 5f;

    private bool isAttacking = false;
    private Vector3 initPosition;

    private void Start()
    {
        initPosition = transform.position;
    }

    private void Update()
    {
        if (IsPlayerBelow() && !isAttacking)
        {
            StartCoroutine(MonsterAttack());
        }
    }

    private bool IsPlayerBelow()
    {
        Collider2D player = Physics2D.OverlapBox(transform.position - new Vector3(0, attackDistance, 0), new Vector2(1f, attackDistance), 0f, playerLayer);
        return player != null;
    }

    private IEnumerator MonsterAttack()
    {
        isAttacking = true;

        Vector3 targetPosition = initPosition - new Vector3(0, attackDistance, 0);
        while (transform.position.y > targetPosition.y)
        {
            transform.position += Vector3.down * fallSpeed * Time.deltaTime;
            yield return null;
        }

        while (transform.position.y < initPosition.y)
        {
            transform.position += Vector3.up * fallSpeed * Time.deltaTime;
            yield return null;
        }

        isAttacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2"))
        {
            GameManager.Instance.GameOver();
        }
    }

    private void DrawGizmo()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position - new Vector3(0, attackDistance, 0), new Vector2(1f, attackDistance));
    }
}
