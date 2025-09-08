using MenteSaudavelAPI._02.Services.Interfaces.Repositories;
using MenteSaudavelAPI._03.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MenteSaudavelAPI._02.Services.Repositories
{
    public class QuestionarioRepository : GenericRepository<Questionario>, IQuestionarioRepository
    {
        public QuestionarioRepository(DataBaseContext context) : base(context)
        {
        }

        public async Task<Questionario?> GetQuestionarioComRespostas(Guid questionarioId)
        {
            return await Find(questionario => questionario.Id == questionarioId)
                .Include(questionario => questionario.Respostas)
                .SingleOrDefaultAsync();
        }

        public async Task<List<Questionario>> GetUltimoQuestionarioRespondidoPorCadaUsuario()
        {
            return await GetAll()
                .GroupBy(q => q.Respondente.Id)
                .Select(g => g.OrderByDescending(q => q.DataEnvio).First())
                .ToListAsync();
        }

        public IQueryable<Questionario> GetQuestionariosByUsuarioId(Guid usuarioId)
        {
            return Find(questionario => questionario.Respondente.Id == usuarioId);
        }
    }
}