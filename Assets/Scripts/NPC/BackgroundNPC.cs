using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundNPC : MonoBehaviour
{
    private Rigidbody2D rb;
    
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void MoveToNode(Transform[] nodes)
    {
        StartCoroutine(MoveToNodeCoroutine(nodes));
    }

    private IEnumerator MoveToNodeCoroutine(Transform[] nodes)
    {
        int node = UnityEngine.Random.Range(0, nodes.Length);
        float speed = 7f;
        float waitTime = UnityEngine.Random.Range(0.1f, 5f);

        yield return new WaitForSeconds(waitTime);

        Vector2 target = nodes[node].position;

        while (Vector2.Distance(rb.position, target) > 0.1f)
        {
            Vector2 direction = (target - rb.position).normalized;
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
    }
}
