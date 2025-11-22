using System;

namespace Deque_DobleCola
{
    // --- CLASES Y NODO DOBLE ---
    public class NodoDoble
    {
        public string Dato { get; set; } // Usamos string para simplificar (nombre foto / placa auto)
        public string InfoExtra { get; set; } // Resolucion / Tipo auto
        public NodoDoble Siguiente { get; set; }
        public NodoDoble Anterior { get; set; }
    }

    // --- GESTOR DEQUE ---
    public class Deque
    {
        private NodoDoble frente, final;
        public int Cantidad { get; set; } = 0;

        // Insertar al Final
        public void PushBack(string dato, string info)
        {
            NodoDoble nuevo = new NodoDoble { Dato = dato, InfoExtra = info };
            if (final == null) { frente = final = nuevo; }
            else
            {
                final.Siguiente = nuevo;
                nuevo.Anterior = final;
                final = nuevo;
            }
            Cantidad++;
            Console.WriteLine($"[Final] Entró: {dato}");
        }

        // Insertar al Frente
        public void PushFront(string dato, string info)
        {
            NodoDoble nuevo = new NodoDoble { Dato = dato, InfoExtra = info };
            if (frente == null) { frente = final = nuevo; }
            else
            {
                nuevo.Siguiente = frente;
                frente.Anterior = nuevo;
                frente = nuevo;
            }
            Cantidad++;
            Console.WriteLine($"[Frente] Entró: {dato}");
        }

        // Sacar del Frente
        public void PopFront()
        {
            if (frente == null) return;
            Console.WriteLine($"[Sale Frente] {frente.Dato}");
            frente = frente.Siguiente;
            if (frente != null) frente.Anterior = null;
            else final = null;
            Cantidad--;
        }

        public void MostrarFrente()
        {
            if (frente != null) Console.WriteLine($"   >> Actual/Frente: {frente.Dato} ({frente.InfoExtra})");
        }

        public void MostrarTodo()
        {
            NodoDoble aux = frente;
            Console.Write("   >> Estado Deque: ");
            while (aux != null) { Console.Write($"[{aux.Dato}] "); aux = aux.Siguiente; }
            Console.WriteLine();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== EJERCICIO 1: EDITOR FOTOS ===");
            Deque fotos = new Deque();
            fotos.PushBack("Foto1.jpg", "HD");
            fotos.PushBack("Foto2.jpg", "HD");
            fotos.PushBack("Foto3.jpg", "HD");
            fotos.PushBack("Foto4.jpg", "HD");
            fotos.PushBack("Foto5.jpg", "HD");

            Console.WriteLine("\n--- Deslizar a la izquierda (Borrar frente) ---");
            fotos.PopFront(); fotos.MostrarFrente();
            fotos.PopFront(); fotos.MostrarFrente();

            Console.WriteLine("\n--- Insertar al inicio ---");
            fotos.PushFront("Editada.png", "4K");
            fotos.MostrarFrente();

            Console.WriteLine("\n\n=== EJERCICIO 2: TRAFICO REVERSIBLE ===");
            Deque carril = new Deque();
            carril.PushFront("Auto-1", "Norte");
            carril.PushBack("Moto-2", "Sur"); // Entra por el otro lado
            carril.PushFront("Camion-3", "Norte");
            carril.PushBack("Auto-4", "Sur");
            carril.PushBack("Auto-5", "Sur");

            Console.WriteLine("\n--- Salida de vehiculos (Por frente) ---");
            carril.PopFront();
            carril.PopFront();
            carril.PopFront();

            Console.WriteLine("\n--- Ingreso nuevos por atras ---");
            carril.PushBack("Moto-New1", "Sur");
            carril.PushBack("Moto-New2", "Sur");

            carril.MostrarTodo();
            Console.ReadKey();
        }
    }
}