using reserva_de_salas.Interfaces;
using reserva_de_salas.Models;

namespace reserva_de_salas.Services.Strategy
{
    public class ValidadorDeReservaHorario : IValidadorDeReservaStrategy
    {
        private readonly IReservaRepository _reservaRepository;

        public ValidadorDeReservaHorario(IReservaRepository reservaRepository)
        {
            _reservaRepository = reservaRepository;
        }
        public async Task<bool> Validar(Reserva reserva)
        {
            //não permite datas passadas
            if (reserva.Date.Date < DateTime.Today || (reserva.Date.Date == DateTime.Today && reserva.HoraInicio < DateTime.Now.TimeOfDay))
            {
                return false;
            }

            //carregar reservas existentes para a mesma sala/data
            var existente = await _reservaRepository.FindBySalaIdAndDataAsync(reserva.SalaId, reserva.Date);

            //ignora a próxima reserva de edição
            existente = existente.Where(x => x.Id != reserva.Id).ToList();

            //verificar sobreposição simples
            bool overlap = existente.Any(x => reserva.HoraInicio < x.HoraFim && x.HoraInicio < reserva.HoraFim);

            return !overlap;
        }
    }
}
