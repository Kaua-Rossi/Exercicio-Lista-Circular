/*
1. Com base no código da lista encadeada simples, construa a classe
da lista encadeada circular. Apresente um exemplo mostrando o valor
dos elementos do primeiro até o último elemento, e na sequencia,
após exibir o último, exibir novamente o primeiro e seguir
apresentando os demais elementos. Use um contador com um limite
para evitar loop infinito.
a. Entrada: Criar uma lista circular com os elementos A->B->C->D
b. Saída do método display()

2. Você vai simular uma fila circular de processos.
a. Cada item tem:
i. Nome do processo
ii. Tempo restante de processamento
b. Você deve criar uma classe que represente o processo.
Regras:
c. Você sempre executa o head por uma quantidade fixa de vezes (ex.: 3
unidades - Cada unidade simboliza um tempo de processamento).
d. Depois, ele vai para o final (rotação automática pela circularidade).
e. Se o tempo restante chegar a 0, ele é removido da lista.
f. O sistema termina quando a lista fica vazia.
*/

using System;

namespace ExercicioListaEncadeada
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("### Questão 01 ###");
			Question01();
			Console.WriteLine("\n### Questão 02 ###");
			Question02();
		}

		public static void Question01()
		{
			Node<String> node1 = new Node<String>("A");
			Node<String> node2 = new Node<String>("B");
			Node<String> node3 = new Node<String>("C");
			Node<String> node4 = new Node<String>("D");

			node1.Next = node2;
			node2.Next = node3;
			node3.Next = node4;
			node4.Next = node1;

			Display(node1);

			static void Display(Node<String> starterNode)
			{
				Node<String>? current = starterNode;
				byte i = 0;

				while (current != null && i < 10)
				{
					i++;
					Console.Write($"{current.Data}" + (i == 10 ? "" : " -> "));
					current = current.Next;
				}
			}
		}

		public static void Question02()
		{
			CircularList circularList = new CircularList();
			circularList.AddProcess("Process A", 20);
			circularList.AddProcess("Process B", 25);
			circularList.AddProcess("Process C", 10);
			circularList.AddProcess("Process D", 40);
			circularList.ShowProcessing(5);
		}
	}

	public class Node<T>
	{
		public T? Data { get; set; }
		public Node<T>? Next { get; set; }

		public Node(T data)
		{
			Data = data;
			Next = null;
		}
	}

	public class Process
	{
		public String Name { get; set; } = "";
		public int RemainingTime { get; set; }
		public Process? Next { get; set; }

		public Process(String name, int remainingTime)
		{
			Name = name;
			RemainingTime = remainingTime;
			Next = null;
		}
	}

	public class CircularList
	{
		public Process? Head;
		public int Length;

		public void AddProcess(String name, int time)
		{
			Process p = new Process(name, time);
			InsertAtEnd(p);
		}

		public void InsertAtEnd(Process newNode)
		{
			if (Head == null)
			{
				Head = newNode;
				newNode.Next = Head;
			}
			else
			{
				Process Current = Head;
				while (Current.Next != Head)
				{
					Current = Current.Next;
				}
				Current.Next = newNode;
				newNode.Next = Head;
			}
			Length++;
		}

		public void HeadRemove()
		{
			if (Head == null) return;


			if (Head.Next == Head)
			{
				Head = null;
			}
			else
			{
				Process? Current = Head;
				while (Current.Next != Head)
				{
					Current = Current.Next;
				}
				Current.Next = Head.Next;
				Head = Head.Next;
			}
			Length--;
		}

		public void ShowProcessing(int unities = 5)
		{
			if (Head == null)
			{
				Console.WriteLine("Lista vazia.");
			}
			while (Length > 0 && Head != null)
			{
				Process p = Head;

				Console.WriteLine($"\n--> Executando {p.Name}");

				p.RemainingTime -= unities;
				if (p.RemainingTime < 0) p.RemainingTime = 0;

				Console.WriteLine($"Tempo restante: {p.RemainingTime}");

				if (p.RemainingTime <= 0)
                {
                    Console.WriteLine($"{p.Name} finalizado e removido.");
                    HeadRemove(); 
                }
                else
                {
                    Console.WriteLine($"{p.Name} vai para o fim da fila.");
                    Process temp = Head;
				    HeadRemove();
				    InsertAtEnd(temp);
                }
			}
			Console.WriteLine("Todos os processos foram finalizados.");
		}
	}
}