using Architecture.Domain.Core.Model;
using FluentValidation;
using Project.Domain.Compras;
using System;
using System.Collections.Generic;

namespace Project.Domain.Clientes
{
    public class Cliente : Entity<Cliente>
    {
        public Cliente(Guid id, string nome, string cpf, string email)
        {
            Id = id;
            Nome = nome;
            CPF = cpf;
            Email = email;
        }

        public string Nome { get; private set; }
        public string CPF { get; private set; }
        public string Email { get; private set; }

        // EF Propriedade de Navegação
        public virtual ICollection<Compra> Compras { get; private set; }

        // Construtor para o EF
        protected Cliente() { }

        public override bool EhValido()
        {
            Validar();
            return ValidationResult.IsValid;
        }

        #region Validações
        private void Validar()
        {
            ValidarNome();
            ValidarCpf();
            ValidarEmail();
        }

        private void ValidarNome()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O nome precisa ser fornecido.")
                .Length(2, 100).WithMessage("O nome precisa ter entre 2 e 100 caracteres.");
        }

        private void ValidarCpf()
        {
            RuleFor(c => c.CPF)
                .NotEmpty().WithMessage("O cpf precisa ser fornecido.")
                .Length(11).WithMessage("O cpf precisa ter 11 caracteres.");
        }

        private void ValidarEmail()
        {
            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("O email precisa ser fornecido.")
                .Length(7, 100).WithMessage("O email precisa ter entre 7 e 100 caracteres.");
        }
        #endregion
    }
}
