using ModernGF.Shared.Enums;
using ModernGF.Validator.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModernGF.Shared.ValueObjects
{
    public class Documento : ValueObject
    {
        protected Documento() { }
        public Documento(string number)
        {
            Numero = Numero;

            AddNotifications(new ValidationContract()
                .Requires()
                .IsTrue(Validate(), "Documento.Numero", "Documento inválido")
           );
        }

        public string Numero { get; private set; }
        [NotMapped]
        public EDocumentType Type { get; private set; }

        private bool Validate()
        {
            if (Type == EDocumentType.CNPJ && Numero.Length == 14)
                return true;

            if (Type == EDocumentType.CPF && Numero.Length == 11)
                return true;

            return false;
        }
    }
}
