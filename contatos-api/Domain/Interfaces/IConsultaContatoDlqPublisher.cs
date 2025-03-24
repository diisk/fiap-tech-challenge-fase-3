namespace Infrastructure.Events
{
    public interface IConsultaContatoDlqPublisher
    {
        Task PublicarConsultaVaziaAsync(int? codigoArea);
    }
}
