using ModernGF.Validator.Validation;

namespace ModernGF.Shared.ValueObjects
{
    public class Endereco : ValueObject
    {
        protected Endereco() { }

        public Endereco(string cEP, string logradouro, string numero, string bairro, string cidade, string estado)
        {
            CEP = cEP;
            Logradouro = logradouro;
            Numero = numero;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;

            AddNotifications(new ValidationContract()
               .Requires()
               .HasMinLen(Logradouro, 3, "Endereco.Logradouro", "A rua deve conter pelo menos 3 caracteres")
           );
        }
        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Bairro { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }
        public string UF { get; private set; }
        public string CEP { get; private set; }
    }
}
