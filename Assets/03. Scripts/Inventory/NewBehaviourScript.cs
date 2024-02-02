using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public int num = 0;
    public Stack<float> stack = new Stack<float>();
    public Queue<float> queue = new Queue<float>();

    private void Start()
    {
        num = 4;

        stack.Push(10);
        stack.Push(5);
        stack.Push(8);
        stack.Push(12);
        stack.Push(10);
        stack.Push(14);

        queue.Enqueue(0.5f);
        queue.Enqueue(0.3f);
        queue.Enqueue(0.15f);
        queue.Enqueue(0.05f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Active();
    }

    void Active()
    {
        int count = num;
        float result = 0;
        float valueSum = 0;

        foreach(float value in queue)
        {
            result += stack.Pop() * value;

            count--;

            if (count == 0)
                break;
        }

        Debug.Log(result);

        count = num;
        foreach (float value in queue)
        {
            valueSum += value;

            count--;

            if (count == 0)
                break;
        }

        Debug.Log(valueSum);

        Debug.Log(result / valueSum);
    }
}
