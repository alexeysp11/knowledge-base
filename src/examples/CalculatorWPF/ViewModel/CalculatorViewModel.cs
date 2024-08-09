using System;
using System.Diagnostics;
using System.Windows;
using System.ComponentModel;
using System.Windows.Input;
using Concepts.Examples.CalculatorWPF.Models;

namespace Concepts.Examples.CalculatorWPF.ViewModels
{
    public class CalculatorViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private int _number1;
        public int Number1 
        { 
            get { return _number1; }
            set 
            { 
                _number1 = value;
                OnPropertyChanged("Number3"); // View notifies that sum is changed
            }
        }

        private int _number2;
        public int Number2 
        { 
            get { return _number2; }
            set 
            { 
                _number2 = value; 
                OnPropertyChanged("Number3"); 
            } 
        }

        public int Number3 
        { 
            get
            {
                var calcModel = new CalculationModel();
                return CalculationModel.GetSumOf(Number1, Number2);
                // return calcModel.CalculateResult(Number1, Number2); 
            }
        }
    }
}