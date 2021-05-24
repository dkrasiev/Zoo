using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Zoo
{
    [Serializable]
    public class GameController
    {
        public List<Animal> animals = new();
        public ObservableCollection<Animal> boughtAnimals = new();
        public ObservableCollection<Item> Items = new();
        public User User { get; private set; }

        /// <summary>
        /// Стартовые значения игры
        /// </summary>
        public GameController()
        {
            User = new User();

            animals.Add(new Animal("Лиса", 10, 2));
            animals.Add(new Animal("Пингвин", 40, 5));
            animals.Add(new Animal("Обезьяна", 200, 20));
            animals.Add(new Animal("Бурый медведь", 650, 60));
            animals.Add(new Animal("Белый медведь", 2500, 200));
            animals.Add(new Animal("Жираф", 10000, 500));
            animals.Add(new Animal("Слон", 40000, 1000));
            animals.Add(new Animal("Большая панда", 100000, 2500));

            Items.Add(new HealItem("Прибраться в зоопарке", 10, 10));
            Items.Add(new HealItem("Уход за животными", 50, 50));
            Items.Add(new HealItem("Капитальный ремонт", 100, 100));
        }

        /// <summary>
        /// Возвращает следующее животное 
        /// </summary>
        /// <returns>Animal</returns>
        public Animal NextAnimal()
        {
            return animals[boughtAnimals.Count];
        }

        /// <summary>
        /// Приобретает животное
        /// </summary>
        /// <param name="index"></param>
        public void BuyAnimal(int index)
        {
            if (User.Money < boughtAnimals[index].Cost)
            {
                System.Windows.MessageBox.Show("Недостаточно средств");
            }
            else
            {
                boughtAnimals[index].Buy(User);
            }
        }

        /// <summary>
        /// Приобретает предмет
        /// </summary>
        /// <param name="index"></param>
        public void BuyItem(int index)
        {
            if (User.Money < Items[index].Cost)
            {
                System.Windows.MessageBox.Show("Недостаточно средств");
            }
            else
            {
                Items[index].Buy(User);
            }
        }
        
        /// <summary>
        /// Возвращает значение прибыли
        /// </summary>
        /// <returns></returns>
        public double Income()
        {
            double income = 0;
            foreach (var item in boughtAnimals)
            {
                income += item.Earning * item.Count;
            }
            return income;
        }

        public override string ToString()
        {
            return $"Имя: {User.Name}\nДеньги: {User.Money}\nПрибыль за день: {Income()}";
        }
    }
}
