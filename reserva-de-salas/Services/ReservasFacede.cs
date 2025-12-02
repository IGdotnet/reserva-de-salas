using reserva_de_salas.Interfaces;
using reserva_de_salas.Models;
using reserva_de_salas.Services.Strategy;

namespace reserva_de_salas.Services
{
    public class ReservasFacede
    {
        private readonly IReservaService _reservaService;
        private readonly IUsuarioService _usuarioService;
        private readonly ISalaService _salaService;
        private readonly IValidadorDeReservaStrategy _validadorHorario;
        private readonly IValidadorDeReservaStrategy _validadorCapacidade;

        public ReservasFacede(IReservaService reservaService, IUsuarioService usuarioService, ISalaService salaService, ValidadorDeReservaHorario validadorHorario, ValidadorDeReservaCapacidade validadorCapacidade)
        {
            _reservaService = reservaService;
            _usuarioService = usuarioService;
            _salaService = salaService;
            _validadorHorario = validadorHorario;
            _validadorCapacidade = validadorCapacidade;
        }

        public async Task<List<Reserva>> ListarReservasAsync()
        {
            var reservas = await _reservaService.GetAllAsync();
            return reservas.ToList();   
        }

        public async Task<List<Usuario>> ListarUsuarioAsync()
        {
            var usuarios = await _usuarioService.GetAllAsync();
            return usuarios.ToList();
        }

        public async Task<List<Sala>> ListarSalaAsync()
        {
            var salas = await _salaService.GetAllSalasAsync();
            return salas.ToList();
        }

        public async Task<Dictionary<string, long>> GetIndicadoresAsync()
        {
            var totalSalas = (await _salaService.GetAllSalasAsync()).Count();
            var totalUsuarios = (await _usuarioService.GetAllAsync()).Count();
            var totalReservas = (await _reservaService.GetAllAsync()).Count();

            return new Dictionary<string, long>
            {
                ["totalSalas"] = totalSalas,
                ["totalUsuarios"] = totalUsuarios,
                ["totalReservas"] = totalReservas
            };
        }

        public async Task<string> ReservarAsync(Reserva reserva)
        {
            //Valida duração mínima
            if (reserva.HoraFim <= reserva.HoraInicio)
            {
                return "A hora de fim deve ser posterior à hora de início.";
            }

            //Data no passado
            if (reserva.Date.Date < DateTime.Today)
            {
                return "A data de reserva não pode ser anterior a data atual.";
            }

            //Usuário e sala existem
            if (await _usuarioService.GetByIdAsync(reserva.UsuarioId) is null)
            {
                return "Usuário não encontrado.";
            }

            if (await _salaService.GetSalaByIdAsync(reserva.SalaId) is null)
            {
                return "Sala não encontrada.";
            }

            //Conflito de horário
            _reservaService.SetValidator(_validadorHorario);
            if (!await _reservaService.ValidateAsync(reserva))
            {
                return "Horário indisponível.";
            }

            //Capacidade
            _reservaService.SetValidator(_validadorCapacidade);
            if (!await _reservaService.ValidateAsync(reserva))
            {
                return "O número de pessoas excede a capacidade da sala.";
            }

            //Salvar
            await _reservaService.SaveAsync(reserva);
            return "Reserva realizada com sucesso.";
        }

        public async Task<string> AtualizarAsync(Reserva model)
        {
            try
            {
                //Busca a instância do banco
                var reserva = await _reservaService.GetByIdAsync(model.Id);

                //Atualiza apenas os campos permitidos
                reserva.UsuarioId = model.UsuarioId;
                reserva.SalaId = model.SalaId;
                reserva.Date = model.Date;
                reserva.HoraInicio = model.HoraInicio;
                reserva.HoraFim = model.HoraFim;
                reserva.NumeroDePessoas = model.NumeroDePessoas;

                //Reusa seu mesmo método de validação e salvamento
                return await ReservarAsync(reserva);
            }
            catch (Exception ex)
            {
                return "Erro: " + ex.Message;
            }
        }

        public async Task DeleteAsync(long id)
        {
            await _reservaService.DeleteAsync(id);
        }

        public async Task<Reserva> GetIdAsync(long id)
        {
            return await _reservaService.GetByIdAsync(id);
        }
    }
}
