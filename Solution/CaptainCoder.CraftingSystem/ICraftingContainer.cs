public interface ICraftingContainer<T>
{
    public string Name { get; }
    public int Rows { get; }
    public int Columns { get; }
    public HashSet<ICraftingCategory> Categories { get; }
    public HashSet<Position> InvalidPositions { get; }
    public IEnumerable<(Position, T)> Positions { get; }

    /// <summary>
    /// Attempts to add the specified <paramref name="item"/> into this <see cref="CraftingContainer"/>
    /// at the specified <paramref name="position"/>. 
    /// Returns true, if the item was added and false otherwise.
    /// </summary>
    public bool TryAddItem(Position position, T item);
    public bool TryMove(Position from, Position to);
    public bool TryRemove(Position position, out T removed);
    public void Clear();
    public bool TryItemAt(Position position, out T result);
    public T ItemAt(Position position);
    public bool HasItemAt(Position position);

    public interface IAdapter : ICraftingContainer<T>
    {
        public ICraftingContainer<T> CraftingContainer { get; }
        string ICraftingContainer<T>.Name => CraftingContainer.Name;
        int ICraftingContainer<T>.Rows => CraftingContainer.Rows;
        int ICraftingContainer<T>.Columns => CraftingContainer.Columns;
        HashSet<ICraftingCategory> ICraftingContainer<T>.Categories => CraftingContainer.Categories;
        HashSet<Position> ICraftingContainer<T>.InvalidPositions => CraftingContainer.InvalidPositions;
        IEnumerable<(Position, T)> ICraftingContainer<T>.Positions => CraftingContainer.Positions;
        bool ICraftingContainer<T>.TryAddItem(Position position, T item) => CraftingContainer.TryAddItem(position, item);
        bool ICraftingContainer<T>.TryMove(Position from, Position to) => CraftingContainer.TryMove(from, to);
        bool ICraftingContainer<T>.TryRemove(Position position, out T removed) => CraftingContainer.TryRemove(position, out removed);
        bool ICraftingContainer<T>.TryItemAt(Position position, out T result) => CraftingContainer.TryItemAt(position, out result);
        T ICraftingContainer<T>.ItemAt(Position position) => CraftingContainer.ItemAt(position);
        bool ICraftingContainer<T>.HasItemAt(Position position) => CraftingContainer.HasItemAt(position);
        void ICraftingContainer<T>.Clear() => CraftingContainer.Clear();
    }
}
