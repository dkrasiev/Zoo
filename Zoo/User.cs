using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo
{
    [Serializable]
    public class User
    {
        public string Name { get; private set; }
        public double Money { get; private set; }
        public int Health { get; private set; }
        public int MaxHealth { get; private set; }

        public User()
        {
            Money = 10f;
            MaxHealth = 100;
            Health = MaxHealth;
        }

        /// <summary>
        /// Получение урона
        /// </summary>
        /// <param name="health"></param>
        public void GetHarm(int health)
        {
            Health -= health;
        }

        /// <summary>
        /// Пополнение здоровья
        /// </summary>
        /// <param name="health"></param>
        public void GetHealth(int health)
        {
            Health += health;
            if (Health > MaxHealth)
            {
                Health = MaxHealth;
            }
        }

        /// <summary>
        /// Изменение имени
        /// </summary>
        /// <param name="name"></param>
        public void SetName(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Получение денег
        /// </summary>
        /// <param name="income"></param>
        public void GetIncome(double income)
        {
            Money += income;
            Money = Math.Round(Money, 2);
        }

        /// <summary>
        /// Трата денег
        /// </summary>
        /// <param name="cost"></param>
        public void SpendMoney(double cost)
        {
            Money -= cost;
            Money = Math.Round(Money, 2);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
