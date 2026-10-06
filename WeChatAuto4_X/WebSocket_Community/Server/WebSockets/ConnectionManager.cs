using System.Net.WebSockets;
using System.Collections.Concurrent;

public class ConnectionManager
{
    private readonly ConcurrentDictionary<string, WebSocket> _sockets = new();

    public string Add(WebSocket socket)
    {
        var id = Guid.NewGuid().ToString();
        _sockets[id] = socket;
        return id;
    }

    public WebSocket? Get(string id)
        => _sockets.TryGetValue(id, out var ws) ? ws : null;

    public void Remove(string id)
    {
        _sockets.TryRemove(id, out _);
    }

    public IEnumerable<string> GetAllIds() => _sockets.Keys;
}