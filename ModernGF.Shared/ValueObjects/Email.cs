using ModernGF.Validator;
using ModernGF.Validator.Validation;

namespace ModernGF.Shared.ValueObjects
{
    public class Email : Notifiable
    {
        protected Email() { }
        public Email(string endereco)
        {
            Endereco = endereco;

            AddNotifications(new ValidationContract()
                .Requires()
                .IsEmail(Endereco, "Email", "O E-mail é inválido")
            );
        }

        public string Endereco { get; private set; }

        public override string ToString()
        {
            return Endereco;
        }
    }
}
