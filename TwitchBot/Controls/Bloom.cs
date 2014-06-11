using System.Drawing;

namespace TwitchBot.Controls
{
    internal class Bloom
    {
        private string _name;
        private Color _value;

        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }

        public Color Value
        {
            get
            {
                return this._value;
            }
            set
            {
                this._value = value;
            }
        }

        public Bloom()
        {
        }

        public Bloom(string name, Color value)
        {
            this._name = name;
            this._value = value;
        }
    }
}