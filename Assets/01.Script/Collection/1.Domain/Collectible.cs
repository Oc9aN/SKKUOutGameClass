using System;

public class Collectible
{
    public string Id { get; }
    public bool IsCollected { get; private set; }
    public DateTime? CollectedAt { get; private set; }

    public Collectible(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentException("Collectible ID is required");

        Id = id;
        IsCollected = false;
    }

    public void Collect(DateTime collectedTime)
    {
        if (IsCollected)
            throw new InvalidOperationException($"Collectible {Id} already collected.");

        IsCollected = true;
        CollectedAt = collectedTime;
    }
}