using MenteSaudavelAPI._02.Services.Interfaces.Services;
using MenteSaudavelAPI._03.Data.Entities;
using MenteSaudavelAPI._04.Infrastructure.Dto;
using MenteSaudavelAPI._04.Infrastructure.Enums;
using MenteSaudavelAPI._04.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace MenteSaudavelAPI._02.Services.Services
{
    public class QuestionarioService : IQuestionarioService
    {
        private readonly IUnitOfWork _unitOfWork;

        public QuestionarioService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<QuestionarioTO> CriarQuestionario(QuestionarioTO questionarioTO)
        {
            Usuario? respondente = await _unitOfWork.UsuarioRepository.GetById(questionarioTO.RespondenteId);

            Questionario questionario = new Questionario(respondente);

            CadastrarRespostas(ref questionario, questionarioTO.ListaRespostas);

            questionario.CalcularPontuacao();
            questionario.DefinirEstratificacao();

            _unitOfWork.QuestionarioRepository.Add(questionario);

            await _unitOfWork.SaveChangesAsync();

            questionarioTO.Pontuacao = questionario.Pontuacao;
            questionarioTO.Estratificacao = questionario.Estratificacao;

            return questionarioTO;
        }

        private void CadastrarRespostas(ref Questionario questionario, List<RespostaTO> listaRespostas)
        {
            foreach (RespostaTO respostaTO in listaRespostas)
            {
                Resposta resposta = new Resposta(questionario, respostaTO);

                questionario.AdicionarResposta(resposta);
            }
        }

        public async Task<QuestionarioTO> GetQuestionario(Guid questionarioId)
        {
            Questionario? questionario = await _unitOfWork.QuestionarioRepository.GetQuestionarioComRespostas(questionarioId);

            if (questionario is null)
            {
                throw new ArgumentException("Questionário não encontrado.");
            }

            return questionario.ToDto();
        }

        public async Task<QuestionarioTO> GetUltimoQuestionarioRespondidoByUsuarioId(Guid usuarioId)
        {
            IQueryable<Questionario> queryQuestionario = _unitOfWork.QuestionarioRepository.GetQuestionariosByUsuarioId(usuarioId);

            Questionario? ultimoQuestionarioRespondido = await queryQuestionario
                 .OrderByDescending(questionario => questionario.DataEnvio)
                 .FirstOrDefaultAsync();

            if (ultimoQuestionarioRespondido is null)
            {
                throw new ArgumentException("Usuário ainda não respondeu nenhum questionário.");
            }

            return ultimoQuestionarioRespondido.ToDto();
        }

        public async Task<List<QuestionarioTO>> GetQuestionariosByUsuarioId(DashboardRequestTO requestTO)
        {
            IQueryable<Questionario> queryQuestionario = _unitOfWork.QuestionarioRepository.GetQuestionariosByUsuarioId(requestTO.UsuarioId.Value);

            bool deveFiltrar =
                requestTO.DataInicio.HasValue ||
                requestTO.DataFim.HasValue;

            if (deveFiltrar)
            {
                queryQuestionario = FiltrarPorData(queryQuestionario, requestTO.DataInicio, requestTO.DataFim);
            }

            List<QuestionarioTO> listaQuestionariosRespondidos = await queryQuestionario
                 .OrderByDescending(questionario => questionario.DataEnvio)
                 .Select(questionario => questionario.ToDto())
                 .ToListAsync();

            return listaQuestionariosRespondidos;
        }

        public async Task<Dictionary<string, int>> GetQtdeUsuariosPorEstratificacao(DashboardTO dashboardTO)
        {
            List<Questionario> listaQuestionarios;

            bool deveFiltrar =
                dashboardTO.DataInicio.HasValue ||
                dashboardTO.DataFim.HasValue ||
                dashboardTO.Genero is not null ||
                dashboardTO.Idade.HasValue && dashboardTO.Idade > 0;

            if (deveFiltrar)
            {
                listaQuestionarios = await GetListaQuestionariosFiltrada(dashboardTO);
            }
            else
            {
                listaQuestionarios = await _unitOfWork.QuestionarioRepository.GetUltimoQuestionarioRespondidoPorCadaUsuario();
            }

            return GetQtdeUsuariosPorEstratificacao(listaQuestionarios);
        }

        private async Task<List<Questionario>> GetListaQuestionariosFiltrada(DashboardTO dashboardTO)
        {
            IQueryable<Questionario> queryQuestionarios = _unitOfWork.QuestionarioRepository.GetAll();

            queryQuestionarios = FiltrarPorData(queryQuestionarios, dashboardTO.DataInicio, dashboardTO.DataFim);

            if (dashboardTO.Genero is not null)
            {
                queryQuestionarios = queryQuestionarios.Where(questionario => questionario.Respondente.Genero.Valor == dashboardTO.Genero.Valor);
            }

            if (dashboardTO.Idade.HasValue)
            {
                var hoje = DateTime.Today;
                var idade = dashboardTO.Idade.Value;

                var dataNascimentoMin = DateOnly.FromDateTime(hoje.AddYears(-idade - 1).AddDays(1));
                var dataNascimentoMax = DateOnly.FromDateTime(hoje.AddYears(-idade));

                queryQuestionarios = queryQuestionarios.Where(questionario =>
                    questionario.Respondente.DataNascimento >= dataNascimentoMin &&
                    questionario.Respondente.DataNascimento <= dataNascimentoMax);
            }

            return await queryQuestionarios.ToListAsync();
        }

        private IQueryable<Questionario> FiltrarPorData(IQueryable<Questionario> queryQuestionarios, DateTime? dataInicio, DateTime? dataFim)
        {
            if (dataInicio.HasValue)
            {
                queryQuestionarios = queryQuestionarios.Where(questionario => questionario.DataEnvio >= dataInicio);
            }

            if (dataFim.HasValue)
            {
                DateTime diaPosterior = dataFim.Value.AddDays(1);

                queryQuestionarios = queryQuestionarios.Where(questionario => questionario.DataEnvio < diaPosterior);
            }

            return queryQuestionarios;
        }

        private Dictionary<string, int> GetQtdeUsuariosPorEstratificacao(List<Questionario> listaQuestionario)
        {
            Dictionary<string, int> qtdeUsuariosPorEstratificacao = Enum.GetValues<EnumEstratificacao>()
                .ToDictionary(e => e.GetDisplayName(), e => 0);

            foreach (var questionario in listaQuestionario)
            {
                qtdeUsuariosPorEstratificacao[questionario.Estratificacao.Descricao] += 1;
            }

            return qtdeUsuariosPorEstratificacao;
        }
    }
}