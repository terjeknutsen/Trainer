namespace CoreDomain.Types
{
    public struct WorkoutType
    {
        readonly string description;
        public WorkoutType(string description)
        {
            this.description = description;
        }
        public override string ToString()
        {
            return description;
        }
    }
}
