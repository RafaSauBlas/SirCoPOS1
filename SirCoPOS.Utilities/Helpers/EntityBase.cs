using SirCoPOS.Common.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Utilities.Helpers
{
    public class EntityBase : INotifyPropertyChanged, Interfaces.IEntity, IDataErrorInfo, INotifyDataErrorInfo
    {
        private Dictionary<string, List<string>> _errors;
        private Dictionary<string, object> _values;
        private Dictionary<string, object> _originals;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        private bool TrackChanges { get; set; }
        public EntityBase()
            : this(true)
        {

        }
        public EntityBase(bool trackChanges)
        {
            this.TrackChanges = trackChanges;
            _values = new Dictionary<string, object>();
            _originals = new Dictionary<string, object>();
            _errors = new Dictionary<string, List<string>>();

            this.PropertyChanged += EntityBase_PropertyChanged;
        }

        private void EntityBase_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.HasErrors):
                    this.RaisePropertyChanged(this.Error);
                    break;
            }
        }

        public void RaisePropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void RaiseErrorsChanged(string propertyName)
        {
            this.ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
        public string this[string columnName]
        {
            get
            {
                if (_errors.ContainsKey(columnName))
                {
                    var res = String.Join(Environment.NewLine, _errors[columnName]);
                    return res;
                }
                return null;
            }
        }

        public string Error
        {
            get
            {
                if (_errors.Any())
                {
                    var list = new List<string>();
                    foreach (var i in _errors)
                    {
                        list.AddRange(i.Value);
                    }
                    var res = String.Join(Environment.NewLine, list);
                    return res;
                }
                return null;
            }
        }

        public bool HasErrors
        {
            get { return _errors.Any(); }
        }

        public bool HasChange(string propertyName)
        {
            return _originals.ContainsKey(propertyName);
        }
        public T GetOriginal<T>(string propertyName)
        {
            if (_originals.ContainsKey(propertyName))
                return (T)_originals[propertyName];
            if (_values.ContainsKey(propertyName))
                return (T)_values[propertyName];
            return default(T);
        }
        public void SetValue<T>(string propertyName, T newValue)
        {
            var currentValue = GetValue<T>(propertyName);
            if (currentValue.IsDefault())
            {
                if (newValue.IsDefault())
                    return;
            }
            else if (!newValue.IsDefault() && currentValue.IsEquals(newValue))
                return;

            if (TrackChanges && !_originals.ContainsKey(propertyName))
                _originals[propertyName] =
                    _values.ContainsKey(propertyName) ? _values[propertyName] : default(T);

            _values[propertyName] = newValue;

            var hasErrors = this.HasErrors;
            CheckRulesForProperty(propertyName, newValue);
            RaisePropertyChanged(propertyName);
            if (hasErrors != this.HasErrors)
                RaisePropertyChanged(nameof(this.HasErrors));
        }
        protected void Set<T>(string propertyName, ref T currentValue, T newValue)
        {
            if (currentValue.IsDefault())
            {
                if (newValue.IsDefault())
                    return;
            }
            else if (!newValue.IsDefault() && currentValue.IsEquals(newValue))
                return;

            currentValue = newValue;

            CheckRulesForProperty(propertyName, newValue);
            RaisePropertyChanged(propertyName);
        }
        public T GetValue<T>(string propertyName)
        {
            if (_values.ContainsKey(propertyName))
                return (T)_values[propertyName];
            return default(T);
        }
        public void CancelEdit()
        {
            if (this.IsDirty())
            {
                foreach (var item in _originals)
                {
                    _values[item.Key] = item.Value;
                    RaisePropertyChanged(item.Key);
                }

                CommitEdit();
            }
        }

        public void CommitEdit()
        {
            if (this.IsDirty())
                _originals.Clear();
        }

        public bool IsDirty()
        {
            if (_originals.Any())
                return true;
            return false;
        }
        private bool _validating = false;
        public bool IsValid()
        {
            if (!_validating)
            {
                lock (this)
                {
                    _validating = true;
                    var hasErrors = this.HasErrors;
                    CheckAllRules();
                    if (hasErrors != this.HasErrors)
                        RaisePropertyChanged(nameof(this.HasErrors));
                    _validating = false;
                }
            }
            return !this.HasErrors;
        }
        private void CheckRulesForProperty(string propertyName, object newValue)
        {
            if (_errors.ContainsKey(propertyName))
                _errors.Remove(propertyName);

            var vrs = new List<ValidationResult>();

            if (!Validator.TryValidateProperty(newValue,
                new ValidationContext(this, null, null) { MemberName = propertyName }, vrs))
            {
                AddValidationResults(vrs);
            }
        }
        private void CheckAllRules()
        {
            _errors.Clear();
            var vrs = new List<ValidationResult>();

            if (!Validator.TryValidateObject(this, new ValidationContext(this, null, null), vrs, true))
            {
                AddValidationResults(vrs);
            }
        }
        private void AddValidationResults(IEnumerable<ValidationResult> vrs)
        {
            foreach (var i in vrs)
            {
                if (i.MemberNames.Any())
                {
                    foreach (var m in i.MemberNames)
                    {
                        if (!_errors.ContainsKey(m))
                            _errors.Add(m, new List<string>());

                        _errors[m].Add(i.ErrorMessage);

                        RaiseErrorsChanged(m);
                        RaisePropertyChanged(m);
                    }
                }
                else
                {
                    var m = string.Empty;
                    if (!_errors.ContainsKey(m))
                        _errors.Add(m, new List<string>());

                    _errors[m].Add(i.ErrorMessage);

                    RaiseErrorsChanged(m);
                    RaisePropertyChanged(m);
                }
            }
        }

        public IEnumerable GetErrors(string propertyName)
        {
            if (_errors != null && _errors.ContainsKey(propertyName))
            {
                return _errors[propertyName];
            }
            return null;
        }

        protected bool IsInDesignMode
        {
            get {

                return GalaSoft.MvvmLight.ViewModelBase.IsInDesignModeStatic
                    || Utilities.Helpers.UnitTestDetector.IsInUnitTest; 
            }
        }
    }
}
