using System;
using System.Collections.Generic;

public class CollectibleProgress
{
    public string UserId { get; }
    private readonly Dictionary<string, Collectible> _collectibles = new();

    public CollectibleProgress(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
            throw new ArgumentException("UserId is required", nameof(userId));

        UserId = userId;
    }

    public void Register(Collectible collectible)
    {
        if (!_collectibles.ContainsKey(collectible.Id))
            _collectibles[collectible.Id] = collectible;
    }

    public bool TryCollect(string id, DateTime time)
    {
        if (!_collectibles.TryGetValue(id, out var collectible))
            throw new Exception($"Collectible {id} not registered.");

        if (collectible.IsCollected)
            return false;

        collectible.Collect(time);
        return true;
    }

    public bool IsCollected(string id)
    {
        return _collectibles.TryGetValue(id, out var collectible) && collectible.IsCollected;
    }

    public IReadOnlyCollection<Collectible> GetAll() => _collectibles.Values;
}