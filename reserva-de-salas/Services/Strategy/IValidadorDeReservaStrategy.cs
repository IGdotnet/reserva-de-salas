using reserva_de_salas.Models;

namespace reserva_de_salas.Services.Strategy
{
    public interface IValidadorDeReservaStrategy
    {
        Task<bool> Validar(Reserva reserva);
    }
}
