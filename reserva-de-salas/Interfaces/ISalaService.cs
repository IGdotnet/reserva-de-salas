using reserva_de_salas.Models;

namespace reserva_de_salas.Interfaces
{
    public interface ISalaService
    {
        Task<IEnumerable<Sala>> GetAllSalasAsync();
        Task<Sala> GetSalaByIdAsync(long id);
        Task<Sala> SaveSalaAsync(Sala sala);
        Task DeleteSalaAsync(long id);
    }
}
