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
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Zoo.Forms
{
    /// <summary>
    /// Interaction logic for SelectGame.xaml
    /// </summary>
    public partial class SelectGame : Window
    {
        /// <summary>
        /// Бинарный форматер
        /// </summary>
        public static BinaryFormatter binaryFormatter = new BinaryFormatter();

        /// <summary>
        /// Данные о сохранениях
        /// </summary>
        public static List<GameController> gameControllers = new List<GameController>
        {
            { null },
            { null },
            { null }
        };

        /// <summary>
        /// Используемый в данный момент контроллер
        /// </summary>
        public static GameController actualGameController;

        public SelectGame()
        {
            InitializeComponent();

            try
            {
                Deserializating();
            }
            catch
            {
                Serializating();
            }
        }

        private void btSlot0_Click(object sender, RoutedEventArgs e)
        {
            ButtonsStart(0);
        }

        private void btSlot1_Click(object sender, RoutedEventArgs e)
        {
            ButtonsStart(1);
        }

        private void btSlot2_Click(object sender, RoutedEventArgs e)
        {
            ButtonsStart(2);
        }

        private void btDel0_Click(object sender, RoutedEventArgs e)
        {
            DeleteSave(0);
        }

        private void btDel1_Click(object sender, RoutedEventArgs e)
        {
            DeleteSave(1);
        }

        private void btDel2_Click(object sender, RoutedEventArgs e)
        {
            DeleteSave(2);
        }

        private void btInfo0_Click(object sender, RoutedEventArgs e)
        {
            Info(0);
        }

        private void btInfo1_Click(object sender, RoutedEventArgs e)
        {
            Info(1);
        }

        private void btInfo2_Click(object sender, RoutedEventArgs e)
        {
            Info(2);
        }

        /// <summary>
        /// Сохранение данные
        /// </summary>
        public static void Serializating()
        {
            using (FileStream fs = new FileStream("Data.dat", FileMode.OpenOrCreate))
            {
                binaryFormatter.Serialize(fs, gameControllers);
            }
        }

        /// <summary>
        /// Чтение данных
        /// </summary>
        public static void Deserializating()
        {
            using (FileStream fs = new FileStream("Data.dat", FileMode.OpenOrCreate))
            {
                gameControllers = (List<GameController>)binaryFormatter.Deserialize(fs);
            }
        }

        /// <summary>
        /// Загружает сохраненную игру или создает новую
        /// </summary>
        /// <param name="index"></param>
        private void ButtonsStart(int index)
        {
            if (gameControllers[index] == null || gameControllers[index].User.Name == null)
            {
                gameControllers[index] = new GameController();
                actualGameController = gameControllers[index];
                EnterName enterName = new EnterName();
                enterName.ShowDialog();
            }
            else
            {
                actualGameController = gameControllers[index];
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
                Close();
            }
        }

        /// <summary>
        /// Удаляет сохранение
        /// </summary>
        /// <param name="index"></param>
        private void DeleteSave(int index)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Удалить сохранение?", "Подтверждение удаления", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                gameControllers[index] = null;
                Serializating();
            }
        }

        /// <summary>
        /// Выводит окно с информацией о сохранении
        /// </summary>
        /// <param name="index"></param>
        private void Info(int index)
        {
            if (gameControllers[index] == null)
            {
                MessageBox.Show("Сохранение отстутсвует");
            }
            else
            {
                MessageBox.Show(gameControllers[index].ToString(), "Данные сохранения");
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }
    }
}
