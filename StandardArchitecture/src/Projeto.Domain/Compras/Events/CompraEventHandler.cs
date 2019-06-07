using Architecture.Domain.Core.Events;
using System;

namespace Project.Domain.Compras.Events
{
    public class CompraEventHandler :
         IHandler<CompraRegistradoEvent>,
         IHandler<CompraAtualizadoEvent>,
         IHandler<CompraExcluidoEvent>
    {
        public void Handle(CompraRegistradoEvent message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Compra Registrado com sucesso");
        }

        public void Handle(CompraAtualizadoEvent message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Compra Atualizada com sucesso");
        }

        public void Handle(CompraExcluidoEvent message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Compra Excluída com sucesso");
        }
    }
}
