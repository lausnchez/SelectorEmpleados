using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace empleadosNoSeQue
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(String usuario)
        {
            InitializeComponent();
            lbl_nombreUsuario.Content = usuario;
        }

        private void btn_leerTodos_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btn_limpiar_Click(object sender, RoutedEventArgs e)
        {
            //OpenFileDialog
        }
    }
}
