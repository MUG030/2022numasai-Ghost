using UnityEngine;
using UnityEngine.Events;

public class SimpleTrigger : MonoBehaviour
{
    public Rigidbody2D triggerBody; 
    public UnityEvent onTriggerEnter;

    private bool hasTriggered = false; // トリガーが発生したかどうかのフラグ
    public Collider2D selfCollider; // 自身の2Dコライダー

    void OnTriggerEnter2D(Collider2D other){
        //do not trigger if there's no trigger target object
        if (triggerBody == null || hasTriggered) return;

        //only trigger if the triggerBody matches
        var hitRb = other.attachedRigidbody;
        if (hitRb == triggerBody)
        {
            onTriggerEnter.Invoke();
            hasTriggered = true;

            if (selfCollider != null)
            {
                Destroy(selfCollider);
            }
        }
    }

}
