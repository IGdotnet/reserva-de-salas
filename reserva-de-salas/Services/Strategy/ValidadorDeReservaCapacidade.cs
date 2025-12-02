using reserva_de_salas.Interfaces;
using reserva_de_salas.Models;

namespace reserva_de_salas.Services.Strategy
{
    public class ValidadorDeReservaCapacidade : IValidadorDeReservaStrategy
    {
        private readonly ISalaRepository _salasRepository;

        public ValidadorDeReservaCapacidade(ISalaRepository salasRepository)
        {
            _salasRepository = salasRepository;
        }
        public async Task<bool> Validar(Reserva reserva)
        {
            var sala = await _salasRepository.GetByIdAsync(reserva.SalaId);
            
            return reserva.NumeroDePessoas <= sala.Capacidade;
        }
    }
}
