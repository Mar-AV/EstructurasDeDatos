using System;

namespace Pilas_Stacks
{
    // --- CLASES DE DATOS ---
    public class Ventana
    {
        public string Nombre { get; set; }
        public string Usuario { get; set; }
        // Constructor simple
        public Ventana(string n, string u) { Nombre = n; Usuario = u; }
    }

    public class Movimiento
    {
        public string Accion { get; set; }
        public string Coordenada { get; set; }
    }

    // --- NODOS ---
    public class NodoVentana
    {
        public Ventana Dato { get; set; }
        public NodoVentana Siguiente { get; set; }
    }

    public class NodoJuego
    {
        public Movimiento Dato { get; set; }
        public NodoJuego Siguiente { get; set; }
    }

    // --- GESTORES (PILAS) ---
    public class PilaVentanas
    {
        private NodoVentana cima;
        public void Push(string nombre, string user)
        {
            NodoVentana nuevo = new NodoVentana();
            nuevo.Dato = new Ventana(nombre, user);
            nuevo.Siguiente = cima;
            cima = nuevo;
            Console.WriteLine($"[Ventana] Abierta: {nombre}");
        }
        public void Pop()
        {
            if (cima != null)
            {
                Console.WriteLine($"[Ventana] Cerrada: {cima.Dato.Nombre}");
                cima = cima.Siguiente;
            }
        }
        public void MostrarActiva()
        {
            if (cima != null) Console.WriteLine($"   >> Activa: {cima.Dato.Nombre}");
        }
    }

    public class PilaJuego
    {
        private NodoJuego cima;
        public int Contador { get; set; } = 0;

        public void Push(string acc, string coord)
        {
            NodoJuego nuevo = new NodoJuego();
            nuevo.Dato = new Movimiento { Accion = acc, Coordenada = coord };
            nuevo.Siguiente = cima;
            cima = nuevo;
            Contador++;
        }
        public void Pop()
        {
            if (cima != null)
            {
                Console.WriteLine($"[Undo] Deshaciendo: {cima.Dato.Accion}");
                cima = cima.Siguiente;
                Contador--;
            }
        }
        public void MostrarUltimo()
        {
            if (cima != null) Console.WriteLine($"   >> Estado actual: {cima.Dato.Accion} en {cima.Dato.Coordenada}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== EJERCICIO 1: HISTORIAL VENTANAS ===");
            PilaVentanas pv = new PilaVentanas();
            pv.Push("Login", "User1");
            pv.Push("Dashboard", "User1");
            pv.Push("Config", "User1");
            pv.Push("Ayuda", "User1");
            pv.Push("Reporte", "User1");
            pv.Push("Perfil", "User1"); // 6 ventanas

            Console.WriteLine("\n--- Usuario cierra ventanas ---");
            pv.Pop(); pv.MostrarActiva();
            pv.Pop(); pv.MostrarActiva();
            pv.Pop(); pv.MostrarActiva();

            Console.WriteLine("\n\n=== EJERCICIO 2: JUEGO (UNDO) ===");
            PilaJuego pj = new PilaJuego();
            pj.Push("Inicio", "0,0");
            pj.Push("Correr", "10,0");
            pj.Push("Saltar", "10,5");
            pj.Push("Atacar", "12,5");
            pj.Push("Caminar", "15,5");
            pj.Push("Saltar", "20,5");
            pj.Push("Caer", "20,0");
            pj.Push("Morir", "20,0"); // 8 movimientos

            Console.WriteLine("\n--- Deshaciendo jugadas (Ctrl+Z) ---");
            for (int i = 0; i < 4; i++)
            {
                pj.Pop();
                pj.MostrarUltimo();
            }
            Console.WriteLine($"Movimientos restantes en memoria: {pj.Contador}");
            Console.ReadKey();
        }
    }
}