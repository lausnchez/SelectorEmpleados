using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace empleadosNoSeQue
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private StreamWriter sw;
        private StreamReader sr;
        private String nombreFichero = "usuarios.txt";
        private Cifrado cypher = new Cifrado();

        public Login()
        {
            InitializeComponent();
        }

        private void btn_registro_Click(object sender, RoutedEventArgs e)
        {

            if (comprobarCampos())
            {
                if (!buscarUsuario(txt_usuario.Text, txt_clave.Text))
                {
                    sw = new StreamWriter(nombreFichero, true);
                    String usuario = cypher.cifrar(txt_usuario.Text);
                    String clave = cypher.cifrar(txt_clave.Text);
                    sw.WriteLine(usuario);
                    sw.WriteLine(clave);
                    sw.Close();
                    MessageBox.Show("Se ha agregado el usuario " + txt_usuario.Text + " con éxito");
                    txt_usuario.Text = "";
                    txt_clave.Text = "";
                }
                else MessageBox.Show("Usuario no válido");
            }
            else MessageBox.Show("Faltan campos por rellenar");
        }

        private void btn_entrar_Click(object sender, RoutedEventArgs e)
        {
            if (comprobarCampos())
            {
                sr = new StreamReader(nombreFichero, true);
                String linea = "";
                String usuario = "";
                String clave = "";
                int counter = 0;

                while ((linea = sr.ReadLine()) != null)
                {
                    // Almacenar datos descifrados
                    if (counter % 2 == 0)
                    {
                        usuario = cypher.descrifrar(linea);
                    }
                    else if (counter % 2 == 1)
                    {
                        clave = cypher.descrifrar(linea);
                        // Si coinciden
                        if (comprobarCoincidencia(usuario, clave))
                        {
                            MainWindow nuevaVentana = new MainWindow(usuario);
                            nuevaVentana.Show();
                            this.Close();
                        }
                        else
                        {
                            usuario = "";
                            clave = "";
                        }
                    }
                    counter++;
                }
            }
            else MessageBox.Show("Faltan campos por rellenar");
        }

        /**
         * encuentra si un usuario ya ha sido ingresado para evitar coincidencias 
         */
        private Boolean buscarUsuario(String usuario, String clave)
        {
            String linea = "";
            String res = "";
            int counter = 0;
            StreamReader sr = new StreamReader(nombreFichero, true);

            while ((linea = sr.ReadLine()) != null)
            {
                // Almacenar datos descifrados
                if (counter % 2 == 0)
                {
                    res = cypher.descrifrar(linea);
                    if (res.Equals(usuario)){
                        return true;
                    }
                }
                counter++;
            }
            return false;
        }

        private Boolean comprobarCoincidencia(String usuario, String clave)
        {
            if (txt_usuario.Text.Equals(usuario) && txt_clave.Text.Equals(clave))
            {
                return true;
            }else return false;
        }

        private Boolean comprobarCampos()
        {
            Boolean valido = true;
            if (txt_clave.Text.Trim().Length == 0)
            {
                valido = false;
            }
            if (txt_usuario.Text.Trim().Length  == 0)
            {
                valido = false;
            }
            return valido;
        }
    }
}
