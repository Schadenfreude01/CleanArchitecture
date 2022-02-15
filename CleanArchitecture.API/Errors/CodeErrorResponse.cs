namespace CleanArchitecture.API.Errors
{
    public class CodeErrorResponse
    {
        public CodeErrorResponse(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageStatusCode(statusCode);
        }

        public int StatusCode { get; private set; }
        public string? Message { get; private set; }

        private string GetDefaultMessageStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "El request enviado tiene errores",
                401 => "No tienes autorizacion para este recurso",
                404 => "No se encontro el recurso solicitado",
                500 => "Se produjeron errores en el servidor",
                _   => string.Empty
            };
        }
    }
}
