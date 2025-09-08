using MenteSaudavelAPI._02.Services.Interfaces.Repositories;
using MenteSaudavelAPI._03.Data.Entities;

namespace MenteSaudavelAPI._02.Services.Repositories
{
    public class RespostaRepository : GenericRepository<Resposta>, IRespostaRepository
    {
        public RespostaRepository(DataBaseContext context) : base(context)
        {
        }
    }
}