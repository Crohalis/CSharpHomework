using UnityEngine;
using System.Collections;

public static class AttackAnimation
{
    public static IEnumerator Move(
        Transform objectToMove,
        Vector3 startPosition,
        Vector3 endPosition,
        float duration)
    {
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            objectToMove.position = Vector3.Lerp(
                startPosition,
                endPosition,
                timer / duration
            );

            yield return null;
        }

        objectToMove.position = endPosition;
    }
}