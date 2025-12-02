using reserva_de_salas.Models;

namespace reserva_de_salas.Interfaces
{
    public interface IReservaRepository
    {
        Task<IEnumerable<Reserva>> GetAllAsync();
        Task<Reserva> GetByIdAsync(long id);
        Task AddAsync(Reserva reserva);
        void Update(Reserva reserva);
        void Delete(Reserva reserva);
        Task SaveChangesAsync();
        Task<List<Reserva>> FindBySalaIdAndDataAsync(long salaId, DateTime data);

    }
}
