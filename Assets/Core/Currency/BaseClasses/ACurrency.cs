using System;
using SafeKepper;

namespace Core.Currency.BaseClasses
{
    public abstract class ACurrency<TType> where TType : Enum
    {
        public abstract TType Type { get; }
        public abstract float BaseValue { get; }
        
        public event Action<float> OnAmountChange;

        private float _amount;
        public float Amount
        {
            get => _amount;
            set
            {
                if (value < 0)
                    _amount = 0;
                else 
                    _amount = value;
                
                OnAmountChange?.Invoke(_amount);
                Saver.SetFloat(SaveKey, _amount);
            }
        }

        private string SaveKey => Type.ToString();

        protected ACurrency()
        {
            Initialize();
        }

        protected virtual void Initialize()
        {
            Amount = Saver.GetFloat(SaveKey, BaseValue);
        }

        public bool HasValue(float decrease)
        {
            return Amount - decrease >= 0;
        }
    }
}
