
namespace Domain.Seedwork.DataTypes
{
    using System;

    public struct TaskCommand
    {
        private string _value;

        public string Value
        {
            get
            {
                return _value ?? throw new ArgumentNullException(nameof(Value));
            }
            
        }

        public TaskCommand(string command)
        {
            _value = command;
        }
    }
}
