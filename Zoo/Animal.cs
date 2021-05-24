using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo
{
    [Serializable]
    public class Animal
    {
        public string Name { get; private set; }
        public double BaseCost { get; private set; }
        public double Cost { get; private set; }
        public double Earning { get; private set; }
        public int Count { get; private set; }

        public Animal(string name, double baseCost, double earning)
        {
            Name = name;
            BaseCost = baseCost;
            Count = 1;
            Earning = earning;
            CostUpdate();
        }

        /// <summary>
        /// Покупка животного
        /// </summary>
        /// <param name="user"></param>
        public void Buy(User user)
        {
            user.SpendMoney(Cost);
            Count++;
            CostUpdate();
        }

        /// <summary>
        /// Обновление стоимости животного
        /// </summary>
        private void CostUpdate()
        {
            Cost = Math.Round(BaseCost * Math.Pow(1.15, Count), 2);
        }

        public override string ToString()
        {
            return $"{Name}({Count}) Доход: {Earning} Стоимость: {Cost}";
        }
    }
}
