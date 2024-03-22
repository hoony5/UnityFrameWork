using System.Collections.Generic;

public class CustomCoroutineInternalPool
{
    private readonly Stack<CustomCoroutineInternal> _pool = new Stack<CustomCoroutineInternal>();

    public CustomCoroutineInternal Get()
    {
        if (_pool.TryPop(out CustomCoroutineInternal item))
            return item;

        return new CustomCoroutineInternal() { index = -1 };
    }

    public void Return(CustomCoroutineInternal item)
    {
        var temp = item;
        temp.index = -1;
        temp.routine = null;
        temp.token = default;
        _pool.Push(item);
    }
}