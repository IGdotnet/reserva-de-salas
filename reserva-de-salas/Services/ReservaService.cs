using reserva_de_salas.Interfaces;
using reserva_de_salas.Models;
using reserva_de_salas.Services.Strategy;

namespace reserva_de_salas.Services
{
    public class ReservaService : IReservaService
    {
        private readonly IReservaRepository _repository;
        private IValidadorDeReservaStrategy _strategy;
        public ReservaService(IReservaRepository repository)
        {
            _repository = repository; 
        }
        public async Task DeleteAsync(long id)
        {
            var reserva = await _repository.GetByIdAsync(id);
            if (reserva == null)
            {
                throw new InvalidOperationException("Reserva não encontrada");
            }
            
            _repository.Delete(reserva);
            await _repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<Reserva>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Reserva> GetByIdAsync(long id)
        {
            var reserva = await _repository.GetByIdAsync(id);
            if (reserva == null)
            {
                throw new InvalidOperationException("Reserva não encontrada");
            }
            return reserva;
        }

        public async Task<Reserva> SaveAsync(Reserva reserva)
        {
            if(reserva.Id == 0)
            {
                await _repository.AddAsync(reserva);
            }
            else
            {
                _repository.Update(reserva);
            }

            await _repository.SaveChangesAsync();
            return reserva;
        }

        public void SetValidator(IValidadorDeReservaStrategy strategy)
        {
            _strategy = strategy;
        }

        public async Task<bool> ValidateAsync(Reserva reserva)
        {
            if (_strategy != null)
            {
                return await _strategy.Validar(reserva);
            }
            throw new InvalidOperationException("Estratégia de validação não definida");
        }
    }
}
