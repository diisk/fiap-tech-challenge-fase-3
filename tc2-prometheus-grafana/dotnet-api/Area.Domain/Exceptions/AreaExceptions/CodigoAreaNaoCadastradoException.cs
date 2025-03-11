using Domain.Exceptions;
using System.Net;

namespace Area.Domain.Exceptions.AreaExceptions
{
    public class CodigoAreaNaoCadastradoException : ApiException
    {
        public CodigoAreaNaoCadastradoException() : base(HttpStatusCode.NotFound, "Esse código de área não está cadastrado ou não existe.")
        {
        }
    }
}
