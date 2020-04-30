namespace Networking
{
    internal class ErrorResponse : Response
    {
        private string message;

        public ErrorResponse(string message)
        {
            this.message = message;
        }
        public string getMessage() { return message; }
    }
}