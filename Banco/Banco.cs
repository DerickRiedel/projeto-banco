using System.Collections.Generic;
using System.Linq;
namespace AppBanco.Banco
{
    public class Banco
    {
        public int valorDisponivel{get; private set;} // Total de Verba no banco (Dinheiro)
        public List<Cedula> cedulasDisponiveis{get; private set;} // Cédulas disponíveis para retirar

        public Banco(){
            cedulasDisponiveis = new List<Cedula>(); // Construtor do Banco
        }

        public void Atualizar(){ // Atualizar o valor do dinheiro total ({valorDisponível}) do banco
            valorDisponivel = 0;
            foreach(var item in cedulasDisponiveis){
                valorDisponivel += item.Valor; // Para cada cédula no banco, pega o valor da cédula e adiciona no valorTotal
            }
        }

        public int[] ListarCedulas(){ // Listar a quantidade de cada tipo de cédula
            var valores = new int[5];
            foreach(var item in cedulasDisponiveis){
                if(item.Valor == 2)
                    valores[0] += 1;
                else if(item.Valor == 5)
                    valores[1] += 1;
                else if(item.Valor == 10)
                    valores[2] += 1;
                else if(item.Valor == 20)
                    valores[3] += 1;
                else if(item.Valor == 50)
                    valores[4] += 1;
            }
            return valores;
        }

        public void Depositar(Cedula cedula){ // Adicionar cédula no banco
            cedulasDisponiveis.Add(cedula);
        }

        public int Retirar(int valor){ // Retirar cédulas que contenham esse valor
            var result = cedulasDisponiveis.Where(x => x.Valor == valor).FirstOrDefault(); // Achar cédulas desse valor
            if(result != null){
                cedulasDisponiveis.Remove(result); // Remover a cédula do banco
                return valor; // Retornar o valor da cédula à ser retirado
            }else{
                return 0; // Se não, retornar um número que indica um erro (Cédula não disponível)
            }
        }
    }
}