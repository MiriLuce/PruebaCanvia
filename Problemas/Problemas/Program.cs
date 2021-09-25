using System;
using System.Linq;

namespace Problemas
{
    class Program
    {
        static void Main(string[] args)
        {
            char[,] ejemploUno = new char[1, 1];
            ejemploUno[0, 0] = 'N';

            char[,] ejemploDos = new char[4, 4];
            ejemploDos[0, 0] = 'N';
            ejemploDos[0, 1] = 'N';
            ejemploDos[0, 2] = 'Y';
            ejemploDos[0, 3] = 'N';
            ejemploDos[1, 0] = 'N';
            ejemploDos[1, 1] = 'N';
            ejemploDos[1, 2] = 'Y';
            ejemploDos[1, 3] = 'N';
            ejemploDos[2, 0] = 'N';
            ejemploDos[2, 1] = 'N';
            ejemploDos[2, 2] = 'N';
            ejemploDos[2, 3] = 'N';
            ejemploDos[3, 0] = 'N';
            ejemploDos[3, 1] = 'Y';
            ejemploDos[3, 2] = 'Y';
            ejemploDos[3, 3] = 'N';

            Console.WriteLine("Problemas Nro 1: Calculo de salarios");
            Console.WriteLine("Respuesta Uno: " + CalculoSalarios(ejemploUno));
            Console.WriteLine("Respuesta Dos: " + CalculoSalarios(ejemploDos));
            Console.WriteLine();

            Console.WriteLine("Problemas Nro 2: Buenos amigos");
            Console.WriteLine("Respuesta Uno: " + BuenosAmigos(8, 1, 15, 1, 10));
            Console.WriteLine("Respuesta Dos: " + BuenosAmigos(7, 1, 15, 1, 10));
            Console.WriteLine("Respuesta Tres: " + BuenosAmigos(11, 1, 15, 1, 10));
            Console.WriteLine("Respuesta Cuatro: " + BuenosAmigos(10000000000, 1, 100000000000, 1, 10));
        }

        /* PROBLEMA NRO 1 : CALCULO DE SALARIOS */

        static public int CalculoSalarios(char[,] matrizEmpleados)
        {
            int cantEmpleados = (int)Math.Sqrt(matrizEmpleados.Length);
            String[] listaEmpleados = new String[cantEmpleados];

            // Lista de subordinados para cada empleado
            for (int i = 0; i < cantEmpleados; i++)
            {
                String subordinados = "";
                for (int j = 0; j < cantEmpleados; j++)
                    if (matrizEmpleados[i, j] == 'Y')
                        subordinados += (subordinados.Length == 0 ? "" : ",") + j;
                listaEmpleados[i] = subordinados;
            }
            return listaEmpleados.Sum(subordinados =>
                // Halla salario para cada empleado y lo suma
                SalarioEmppleado(listaEmpleados, subordinados)
            );
        }

        static public int SalarioEmppleado(String[] listaEmpleados, String subordinados)
        {
            if (subordinados.Length == 0)
                return 1; // No tiene subordinados
            return subordinados.Split(",").Sum(empleado =>
                // Halla el salario de cada subordinados
                SalarioEmppleado(listaEmpleados, listaEmpleados[int.Parse(empleado)])
            );
        }

        /* PROBLEMA NRO 2 : BUENOS AMIGOS */

        static public int BuenosAmigos(Int64 distLibros, int cantMejoresAmi, Int64 distMejoresAmi, int cantCompa, Int64 distCompa)
        {
            Int64 distFaltante = distLibros;

            if (distFaltante < 0) return -1; //  El recorrido ofrecido no alcanza
            else if (distFaltante == 0) return 0; // Recorrido completo
            else if (cantMejoresAmi > 0)
            {
                // Recorrido de mejor amigo + Recorrido de los demás
                distFaltante = Recorrido(distFaltante, distMejoresAmi);
                int cantPersonas = BuenosAmigos(distFaltante, cantMejoresAmi - 1, distMejoresAmi, cantCompa, distCompa);
                return cantPersonas >= 0 ? 1 + cantPersonas : -1;
            }
            else if (cantCompa > 0)
            {
                // Recorrido de compañero + Recorrido de los demás
                distFaltante = Recorrido(distFaltante, distCompa);
                int cantPersonas = BuenosAmigos(distFaltante, cantMejoresAmi, distMejoresAmi, cantCompa - 1, distCompa);
                return cantPersonas >= 0 ? 1 + cantPersonas : -1;
            }
            else return -1; // No hay amigos
        }

        static public Int64 Recorrido(Int64 distInicial, Int64 distMaxima)
        {
            if (distInicial >= distMaxima)
                return -1; // El recorrido ofrecido no alcanza
            else if (distInicial * 2 <= distMaxima)
                return 0; // El recorrido ofrecido es suficiente para completar
            else
                return distInicial * 2 - distMaxima; // Lo que dejó por recorrer
        }
    }
}
