using MenteSaudavelAPI._02.Services.Interfaces.Repositories;
using MenteSaudavelAPI._02.Services.Repositories;

namespace MenteSaudavelAPI._02.Services
{
    #region INTERFACE
    public interface IUnitOfWork
    {
        public IUsuarioRepository UsuarioRepository { get; }

        public IQuestionarioRepository QuestionarioRepository { get; }

        public IRespostaRepository RespostaRepository { get; }

        Task SaveChangesAsync();
    }
    #endregion

    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        #region PROPRIEDADES
        private readonly DataBaseContext _context;
        private bool _disposed = false;
        private IUsuarioRepository _usuarioRepository;
        private IQuestionarioRepository _questionarioRepository;
        private IRespostaRepository _respostaRepository;
        #endregion

        #region CONSTRUTOR
        public UnitOfWork(DataBaseContext context)
        {
            _context = context;
        }
        #endregion

        #region METODOS
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _context.Dispose();
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public IUsuarioRepository UsuarioRepository
        {
            get
            {
                if (_usuarioRepository == null)
                {
                    _usuarioRepository = new UsuarioRepository(_context);
                }

                return _usuarioRepository;
            }
        }

        public IQuestionarioRepository QuestionarioRepository
        {
            get
            {
                if (_questionarioRepository == null)
                {
                    _questionarioRepository = new QuestionarioRepository(_context);
                }

                return _questionarioRepository;
            }
        }

        public IRespostaRepository RespostaRepository
        {
            get
            {
                if (_respostaRepository == null)
                {
                    _respostaRepository = new RespostaRepository(_context);
                }

                return _respostaRepository;
            }
        }
        #endregion
    }
}