using ModernGF.Validator;
using System;

namespace ModernGF.Shared.Entities
{
    public abstract class Entity<T> : Notifiable
    {
        public T Cod { get; protected set; }
        public DateTime DataCadastro { get; set; }
        public Nullable<DateTime> DataAlteracao { get; set; }
        public Nullable<DateTime> DataExcluido { get; set; }
    }
}
