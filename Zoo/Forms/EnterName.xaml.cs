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
using System.Windows.Shapes;

namespace Zoo.Forms
{
    /// <summary>
    /// Interaction logic for EnterName.xaml
    /// </summary>
    public partial class EnterName : Window
    {
        public static string name;

        public EnterName()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Ввод имени
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btEnterName_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbEnterName.Text) == false)
            {
                SelectGame.actualGameController.User.SetName(tbEnterName.Text);

                Game game = new Game();
                game.Show();

                var windows = Application.Current.Windows;
                foreach (var item in windows)
                {
                    if (item.Equals(game) == false)
                    {
                        (item as Window).Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Имя не может быть пустым");
            }
        }
    }
}
