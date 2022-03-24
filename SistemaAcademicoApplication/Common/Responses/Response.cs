using System.Collections.ObjectModel;

namespace SistemaAcademicoApplication.Common.Responses
{
    public interface IResponse
    {
        IEnumerable<string> Errors { get; }
        void AddError(string message);
    }

    public class Response<T> : IResponse
    {
        private readonly IList<string> _messages = new List<string>();

        public IEnumerable<string> Errors { get; }
        public T Result { get; }

        public Response()
        {
            Errors = new ReadOnlyCollection<string>(_messages);
        }

        public Response(T result) : this()
        {
            Result = result;
        }

        public void AddError(string message)
        {
            _messages.Add(message);
        }
    }
}
