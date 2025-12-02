using reserva_de_salas.Models;
using reserva_de_salas.Services.Strategy;

namespace reserva_de_salas.Interfaces
{
    public interface IReservaService
    {
        Task<IEnumerable<Reserva>> GetAllAsync();
        Task<Reserva> GetByIdAsync(long id);
        Task<Reserva> SaveAsync(Reserva reserva);
        Task DeleteAsync(long id);
        void SetValidator(IValidadorDeReservaStrategy validator);
        Task<bool> ValidateAsync(Reserva reserva);
    }
}
