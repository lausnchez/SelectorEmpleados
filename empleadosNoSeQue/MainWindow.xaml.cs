using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Policy;
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
            public String urlFoto {  get; set; }

            public Empleado(int id, String nombre, String apellidos, String tlf, String urlFoto)
            {
                this.ID = id;
                this.Nombre = nombre;
                this.Apellidos = apellidos;
                this.Telefono = tlf;
                this.urlFoto = urlFoto;
            }
        }

        private void btn_leerTodos_Click(object sender, RoutedEventArgs e)
        {
            actualizarSource();
            MessageBox.Show("Se ha agregado la lista de empleados");
        }

        private void btn_limpiar_Click(object sender, RoutedEventArgs e)
        {
            listBox_empleados.ItemsSource = null;
        }

        private void btn_guardar_Click(object sender, RoutedEventArgs e)
        {
            if (camposVacios()){           
                if (comprobarID(txt_id.Text))
                {
                    // Escribir en el fichero
                    sw = new StreamWriter(nombreFichero,true);

                    String urlImagen = img_empleado.Source.ToString().Substring(8);

                    sw.WriteLine(txt_id.Text);
                    sw.WriteLine(txt_nombre.Text);
                    sw.WriteLine(txt_apellidos.Text);
                    sw.WriteLine(txt_tlf.Text);
                    sw.WriteLine(urlImagen);

                    sw.Close();

                    // Agregar al listBox
                    listaEmpleados.Add(new Empleado(
                        int.Parse(txt_id.Text),
                        txt_nombre.Text,
                        txt_apellidos.Text,
                        txt_tlf.Text,
                        img_empleado.Source.ToString()));
                    MessageBox.Show("Se ha agregado el empleado con éxito");
                }
            }
        }

        private void btn_foto_Click(object sender, RoutedEventArgs e)
        {
            String ruta = "";
            var dialog = new Microsoft.Win32.OpenFileDialog();

            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                img_empleado.Source = new BitmapImage(new Uri(dialog.FileName));
            }
        }

        private void btn_eliminar_Click(object sender, RoutedEventArgs e)
        {
            if (listBox_empleados.SelectedItem != null)
            {
                listBox_empleados.ItemsSource = null;
                listaEmpleados.Remove((Empleado)listBox_empleados.SelectedItem);
                listBox_empleados.ItemsSource = listaEmpleados;
            }
            else MessageBox.Show("Debe seleccionar un empleado");
        }

        /**
         * Comprueba si los campos están vacíos. Devuelve false si hay algún campo sin rellenar
         */
        private Boolean camposVacios()
        {
            String error = "";
            Boolean valido = true;
            if (txt_id.Text.Trim().Length == 0)
            {
                valido = false;
                error += "\nEl campo \"ID\" está vacío";
            }
            if (txt_nombre.Text.Trim().Length == 0)
            {
                valido = false;
                error += "\nEl campo \"Nombre\" está vacío";
            }
            if (txt_apellidos.Text.Trim().Length == 0)
            {
                valido = false;
                error += "\nEl campo \"Apellidos\" está vacío";
            }
            if (txt_tlf.Text.Trim().Length == 0)
            {
                valido = false;
                error += "\nEl campo \"Teléfono\" está vacío";
            }
            if (valido == false)
            {
                MessageBox.Show(error);
            }
            return valido;
        }

        private Boolean comprobarID(String idIngresado)
        {
            Boolean valido = true;
            sr = new StreamReader(nombreFichero);
            String linea = "";
            int contador = 0;

            while ((linea = sr.ReadLine()) != null)
            {
                // LINEA DE ID
                if (contador%5 == 0)
                {
                    if (idIngresado.Equals(linea))
                    {
                        valido = false;
                    }
                    sr.ReadLine();  // Nombre
                    sr.ReadLine();  // Apellidos
                    sr.ReadLine();  // Telefono
                    sr.ReadLine();  // Imagen
                }
                contador+=4;
            }
            sr.Close();
            if (!valido)
            {
                MessageBox.Show("ID no válido");
            }
            return valido;
        }

        private void actualizarSource()
        {
            listBox_empleados.ItemsSource = null;
            listaEmpleados.Clear();
            sr = new StreamReader(nombreFichero);
            String linea = "";
            int counter = 0;
            int id = 0;
            String nombre = "";
            String apellidos = "";
            String tlf = "";
            String url = "";

            while ((linea = sr.ReadLine()) != null)
            {
                // ID
                if (counter % 5 == 0)
                {
                    id = int.Parse(linea);
                }
                // NOMBRE
                else if (counter % 5 == 1)
                {
                    nombre = linea;
                }
                // APELLIDOS
                else if (counter % 5 == 2)
                {
                    apellidos = linea;
                }
                // TELEFONO
                else if (counter % 5 == 3)
                {
                    tlf = linea;
                }
                else if (counter % 5 == 4)
                {
                    url = linea;
                    listaEmpleados.Add(new Empleado(id, nombre, apellidos, tlf, url));

                    // Reset Strings
                    id = 0;
                    nombre = "";
                    apellidos = "";
                    tlf = "";
                    url = "";
                }
                counter++;
            }
            sr.Close();
            listBox_empleados.ItemsSource = listaEmpleados;
        }
    }
}
