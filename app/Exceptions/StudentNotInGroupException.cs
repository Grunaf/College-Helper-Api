namespace app.Exceptions
{
    public class StudentNotInGroupException : Exception
    {
        public StudentNotInGroupException() { }

        public StudentNotInGroupException(string message) : base(message) { }
    }
}
