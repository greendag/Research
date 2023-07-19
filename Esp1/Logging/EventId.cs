namespace Logging
{
    /// <summary>Event Id structure</summary>
    public readonly struct EventId
    {
        #region Properties

        /// <summary>The ID</summary>
        public int Id { get; }

        /// <summary>The name, null is none</summary>
        public string Name { get; }

        #endregion

        #region Initialization

        /// <summary>Creates an EventId</summary>
        /// <param name="id">The ID number</param>
        /// <param name="name">The associated name</param>
        public EventId(int id, string name = null)
        {
            Id = id;
            Name = name;
        }

        #endregion

        #region Public Methods

        /// <summary>Implicitly convert int to EventId</summary>
        /// <param name="i"></param>
        public static implicit operator EventId(int i) => new(i);

        /// <summary>Equal operator</summary>
        /// <param name="left">EventId left to compare</param>
        /// <param name="right">EventId right to compare</param>
        /// <returns>True if equal</returns>
        public static bool operator ==(EventId left, EventId right) => left.Equals(right);

        /// <summary>non equal operator</summary>
        /// <param name="left">EventId left to compare</param>
        /// <param name="right">EventId right to compare</param>
        /// <returns>True if not equal</returns>
        public static bool operator !=(EventId left, EventId right) => !left.Equals(right);

        /// <summary>
        /// Check if this EventId have the same Id as the other one
        /// </summary>
        /// <param name="other">The EventId to compare</param>
        /// <returns>True if Id is equal</returns>
        public bool Equals(EventId other) => Id == other.Id;

        /// <summary>
        /// Check if  this EventId is the same object as the other one
        /// </summary>
        /// <param name="obj">The EventId to compare</param>
        /// <returns>True if equal</returns>
        public override bool Equals(object obj) => obj is EventId other && Equals(other);

        /// <summary>Get the hash code</summary>
        /// <returns>ID is the hash code</returns>
        public override int GetHashCode() => Id;

        /// <summary>Convert to string</summary>
        /// <returns>The name or ID if name is null</returns>
        public override string ToString() => Name ?? Id.ToString();

        #endregion
    }
}
