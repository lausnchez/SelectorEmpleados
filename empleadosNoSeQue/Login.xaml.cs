using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        private void btn_entrar_Click(object sender, RoutedEventArgs e)
        {
            sr = new StreamReader(nombreFichero, true);
            String linea = "";
            while ((linea = sr.ReadLine()) != null)
            {
                
            }
        }
    }
}
