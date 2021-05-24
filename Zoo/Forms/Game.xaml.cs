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
using System.IO;

namespace Zoo.Forms
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : Window
    {
        /// <summary>
        /// Контроллер сохранения этого окна
        /// </summary>
        GameController gameController;
        public Game()
        {
            InitializeComponent();

            gameController = SelectGame.actualGameController;

            Closing += Game_Closing;

            ButtonsUpdate();
        }

        /// <summary>
        /// Покупает следующее животное
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btAddAnimal_Click(object sender, RoutedEventArgs e)
        {
            if (gameController.User.Money < gameController.NextAnimal().BaseCost)
            {
                NotEnoughMoney();
            }
            else
            {
                gameController.User.SpendMoney(gameController.NextAnimal().BaseCost);
                gameController.boughtAnimals.Add(gameController.NextAnimal());
            }

            ButtonsUpdate();
        }

        /// <summary>
        /// Покупает животное из списка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listAnimal_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (listAnimals.SelectedIndex >= 0)
            {
                gameController.BuyAnimal(listAnimals.SelectedIndex);
            }

            ButtonsUpdate();
        }

        /// <summary>
        /// Кнопка следующего дня
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btNextDay_Click(object sender, RoutedEventArgs e)
        {
            gameController.User.GetIncome(gameController.Income());
            gameController.User.GetHarm(5);

            if (gameController.User.Health <= 0)
            {
                MessageBox.Show("Вы проиграли");
                Close();
            }

            ButtonsUpdate();
        }

        /// <summary>
        /// Сохраняет игру
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            bool error = false;
            try
            {
                SelectGame.Serializating();
            }
            catch
            {
                error = true;
                MessageBox.Show("Ошибка сохранения");
            }
            if (error == false)
            {
                MessageBox.Show("Сохранение прошло успешно");
            }
        }

        /// <summary>
        /// Обновление надписей интерфейса
        /// </summary>
        private void ButtonsUpdate()
        {
            listAnimals.ItemsSource = null;
            listAnimals.ItemsSource = gameController.boughtAnimals;
            listItems.ItemsSource = null;
            listItems.ItemsSource = gameController.Items;
            tbName.Text = gameController.User.Name;
            tbHealth.Text = $"{gameController.User.Health} / {gameController.User.MaxHealth}" ;
            tbMoney.Text = Convert.ToString(gameController.User.Money);
            tbIncome.Text = Convert.ToString(gameController.Income());

            if (gameController.boughtAnimals.Count == gameController.animals.Count)
            {
                btAddCell.Content = "Все животные куплены";
                btAddCell.IsEnabled = false;
            }
            else
            {
                btAddCell.Content = $"Купить: {gameController.NextAnimal().Name}\nСтоимость: {gameController.NextAnimal().BaseCost}";
            }
        }

        /// <summary>
        /// Выполняется во время закрытия программы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Game_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        /// <summary>
        /// Выводит сообщение "Недостаточно средств"
        /// </summary>
        private static void NotEnoughMoney()
        {
            MessageBox.Show("Недостаточно средств");
        }

        /// <summary>
        /// Покупает предмет из списка по двойному клику
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listItems_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (listItems.SelectedIndex >= 0)
            {
                gameController.BuyItem(listItems.SelectedIndex);
            }

            ButtonsUpdate();
        }
    }
}
