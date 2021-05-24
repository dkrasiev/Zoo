using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo
{
    [Serializable]
    public abstract class Item
    {
        public string Name { get; private set; }
        public double Cost { get; private set; }

        public Item(string name, double cost)
        {
            Name = name;
            Cost = cost;
        }

        /// <summary>
        /// Покупка предмета 
        /// </summary>
        /// <param name="user"></param>
        public virtual void Buy(User user)
        {
            user.SpendMoney(Cost);
            Use(user);
        }

        /// <summary>
        /// Использование предмета
        /// </summary>
        /// <param name="user"></param>
        public virtual void Use(User user)
        {

        }

        public override string ToString()
        {
            return Name;
        }
    }

    /// <summary>
    /// Лечащие предметы
    /// </summary>
    [Serializable]
    public class HealItem : Item
    {
        /// <summary>
        /// Сила, с которой лечит предмет
        /// </summary>
        public int HealPower { get; private set; }

        public HealItem(string name, double cost, int power) : base(name, cost)
        {
            HealPower = power;
        }

        public override void Buy(User user)
        {
            base.Buy(user);
        }

        public override void Use(User user)
        {
            user.GetHealth(HealPower);
        }

        public override string ToString()
        {
            return $"{Name}(+{HealPower}) Стоимость: {Cost}";
        }
    }
}
