using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace empleadosNoSeQue
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        String nombreFichero = "empleados.txt";
        StreamWriter sw = null;
        StreamReader sr = null;
        List<Empleado> listaEmpleados = new List<Empleado>();

        public MainWindow(String usuario)
        {
            InitializeComponent();
            lbl_nombreUsuario.Content = usuario;
        }

        public class Empleado
        {
            public int ID {  get; set; }
            public String Nombre {  get; set; }
            public String Apellidos {  get; set; }
            public String Telefono {  get; set; }

            public Empleado(int id, String nombre, String apellidos, String tlf)
            {
                this.ID = id;
                this.Nombre = nombre;
                this.Apellidos = apellidos;
                this.Telefono = tlf;
            }
        }

        private void btn_leerTodos_Click(object sender, RoutedEventArgs e)
        {
            sr = new StreamReader(nombreFichero);
            String linea = "";
            int counter = 0;
            int id = 0;
            String nombre = "";
            String apellidos = "";
            String tlf = "";

            while ((linea = sr.ReadLine()) != null)
            {
                // ID
                if (counter%4 == 0)
                {
                    id = int.Parse(linea);
                }
                // NOMBRE
                else if (counter%4 == 1)
                {
                    nombre = linea;
                }
                // APELLIDOS
                else if (counter%4 == 2)
                {
                    apellidos = linea;
                }
                // TELEFONO
                else if (counter%4 == 3)
                {
                    tlf = linea;
                    listaEmpleados.Add(new Empleado(id, nombre, apellidos, tlf));
                    // Reset Strings
                    id = 0;
                    nombre = "";
                    apellidos = "";
                    tlf = "";
                }
                counter++;
            }
            sr.Close();
        }

        private void btn_limpiar_Click(object sender, RoutedEventArgs e)
        {
            //OpenFileDialog
        }

        private void btn_guardar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
