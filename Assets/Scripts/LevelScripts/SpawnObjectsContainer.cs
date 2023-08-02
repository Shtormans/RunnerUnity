using System.Collections.Generic;

public class SpawnObjectsContainer
{
    private Queue<SpawnPlatformController> _platforms;

    public int Count => _platforms.Count;

    public SpawnObjectsContainer()
    {
        _platforms = new Queue<SpawnPlatformController>();
    }

    public void Add(SpawnPlatformController value)
    {
        _platforms.Enqueue(value);
    }

    public void RemoveFirst()
    {
        var platform = _platforms.Dequeue();

        if (platform != null)
        {
            platform.DestroyPlatform();
        }
    }

    public SpawnPlatformController GetFirst()
    {
        return _platforms.Peek();
    }

    public void RemoveFirstNulls()
    {
        while (_platforms.Peek() == null)
        {
            _platforms.Dequeue();
        }
    }
}
