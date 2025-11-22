using System;

namespace Colas_Queues
{
    // --- CLASES ---
    public class Cliente
    {
        public int Orden { get; set; }
        public string Nombre { get; set; }
    }
    public class Paquete
    {
        public string Codigo { get; set; }
        public double Peso { get; set; }
    }

    // --- NODOS ---
    public class NodoCliente
    {
        public Cliente Dato { get; set; }
        public NodoCliente Siguiente { get; set; }
    }
    public class NodoPaquete
    {
        public Paquete Dato { get; set; }
        public NodoPaquete Siguiente { get; set; }
    }

    // --- GESTORES (COLAS) ---
    public class ColaRestaurante
    {
        private NodoCliente frente, final;
        public int Cantidad { get; set; } = 0;

        public void Enqueue(int ord, string nom)
        {
            NodoCliente nuevo = new NodoCliente { Dato = new Cliente { Orden = ord, Nombre = nom } };
            if (frente == null) { frente = final = nuevo; }
            else { final.Siguiente = nuevo; final = nuevo; }
            Cantidad++;
            Console.WriteLine($"[Fila] Llegó: {nom}");
        }
        public void Dequeue()
        {
            if (frente != null)
            {
                Console.WriteLine($"[Atención] Sirviendo a: {frente.Dato.Nombre}");
                frente = frente.Siguiente;
                Cantidad--;
                if (frente == null) final = null;
            }
        }
        public void Peek()
        {
            if (frente != null) Console.WriteLine($"   >> Siguiente turno: {frente.Dato.Nombre}");
        }
    }

    public class ColaBodega
    {
        private NodoPaquete frente, final;
        public void Enqueue(string cod, double peso)
        {
            NodoPaquete nuevo = new NodoPaquete { Dato = new Paquete { Codigo = cod, Peso = peso } };
            if (frente == null) { frente = final = nuevo; }
            else { final.Siguiente = nuevo; final = nuevo; }
            Console.WriteLine($"[Bodega] Recibido: {cod}");
        }
        public void Dequeue()
        {
            if (frente != null)
            {
                Console.WriteLine($"[Despacho] Salió camión con: {frente.Dato.Codigo}");
                frente = frente.Siguiente;
                if (frente == null) final = null;
            }
        }
        public void Peek()
        {
            if (frente != null) Console.WriteLine($"   >> Próximo a salir: {frente.Dato.Codigo}");
        }
        public double SumarPesos()
        {
            double total = 0;
            NodoPaquete aux = frente;
            while (aux != null) { total += aux.Dato.Peso; aux = aux.Siguiente; }
            return total;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== EJERCICIO 1: RESTAURANTE ===");
            ColaRestaurante cr = new ColaRestaurante();
            for (int i = 1; i <= 9; i++) cr.Enqueue(100 + i, $"Cliente {i}");

            Console.WriteLine("\n--- Atendiendo ---");
            for (int i = 0; i < 4; i++) cr.Dequeue();

            cr.Peek();
            Console.WriteLine($"En espera: {cr.Cantidad}");

            Console.WriteLine("\n\n=== EJERCICIO 2: BODEGA ===");
            ColaBodega cb = new ColaBodega();
            cb.Enqueue("P-001", 10.5); cb.Enqueue("P-002", 2.0);
            cb.Enqueue("P-003", 5.0); cb.Enqueue("P-004", 1.5);
            cb.Enqueue("P-005", 8.0); cb.Enqueue("P-006", 3.5);
            cb.Enqueue("P-007", 12.0);

            Console.WriteLine("\n--- Procesando ---");
            for (int i = 0; i < 3; i++) cb.Dequeue();

            cb.Peek();
            Console.WriteLine($"Peso total pendiente: {cb.SumarPesos()} Kg");
            Console.ReadKey();
        }
    }
}