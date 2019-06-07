using FluentValidation;
using FluentValidation.Results;
using System;

namespace Architecture.Domain.Core.Model
{
    public abstract class Entity<T> : AbstractValidator<T> where T : Entity<T>
    {
        protected Entity()
        {
            ValidationResult = new ValidationResult();
        }

        public Guid Id { get; protected set; }

        public abstract bool EhValido();
        public ValidationResult ValidationResult { get; protected set; }

        // vai ser comparável com outra instância da entidade com o ID, e não no tipo
        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity<T>;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(Entity<T> a, Entity<T> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity<T> a, Entity<T> b)
        {
            return !(a == b);
        }

        // essa implementação é obrigatória quando fazemos override em Equals
        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 123) + Id.GetHashCode(); // 123 pode ser qualquer número
        }

        // interessante para pegar o nome da entidade
        public override string ToString()
        {
            return GetType().Name + "[Id = " + Id + "]";
        }
    }
}
