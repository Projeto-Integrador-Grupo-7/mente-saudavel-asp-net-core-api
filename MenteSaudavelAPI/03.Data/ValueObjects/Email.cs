namespace MenteSaudavelAPI._03.Data.ValueObjects
{
    public class Email
    {
        public string Endereco { get; private set; }

        internal Email() { }

        public Email(string endereco)
        {
            Endereco = endereco;
        }
    }
}
