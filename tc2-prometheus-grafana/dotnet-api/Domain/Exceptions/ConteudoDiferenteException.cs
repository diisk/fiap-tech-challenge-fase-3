using System.Net;

namespace Domain.Exceptions
{
    public class ConteudoDiferenteException : ApiException
    {
        public ConteudoDiferenteException() : base(HttpStatusCode.BadRequest, "Conteudo do corpo está diferente do esperado.") { }

    }
}
