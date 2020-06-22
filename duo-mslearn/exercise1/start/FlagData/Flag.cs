using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FlagData
{
    /// <summary>
    /// This model object represents a single flag
    /// </summary>
    public class Flag : INotifyPropertyChanged
    {
        private DateTime _dateAdopted;
        private string _country;
        private bool _includesShield;
        private string _description;
        private Uri _moreInformationUrl;
        private string _imageUrl;

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Name of the country that this flag belongs to
        /// </summary>
        public string Country
        {
            get { return _country; }
            set { ChangePropertyValue(ref _country, value); }
        }
        
        /// <summary>
        /// Image URL for the flag (from resources)
        /// </summary>
        public string ImageUrl
        {
            get { return _imageUrl; }
            set { ChangePropertyValue(ref _imageUrl, value); }
        }

        /// <summary>
        /// The date this flag was adopted
        /// </summary>
        public DateTime DateAdopted
        {
            get { return _dateAdopted; }
            set
            {
                if (_dateAdopted != value)
                {
                    _dateAdopted = value;
                    // Can pass the property name as a string,
                    // -or- let the compiler do it because of the
                    // CallerMemberNameAttribute on the RaisePropertyChanged method.
                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Whether the flag includes an image/shield as part of the design
        /// </summary>
        public bool IncludesShield
        {
            get { return _includesShield; }
            set { ChangePropertyValue(ref _includesShield, value); }
        }
        
        /// <summary>
        /// Some trivia or commentary about the flag
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { ChangePropertyValue(ref _description, value); }
        }
        
        /// <summary>
        /// A URL for more information
        /// </summary>
        public Uri MoreInformationUrl
        {
            get { return _moreInformationUrl; }
            set { ChangePropertyValue(ref _moreInformationUrl, value); }
        }

        /// <summary>
        /// Helper method to test a field value against a new value,
        /// do the assignment if they are different, and then raise the
        /// property change notification.
        /// </summary>
        /// <typeparam name="T">Type being changed</typeparam>
        /// <param name="field">Field</param>
        /// <param name="value">New value</param>
        /// <param name="propertyName">Property name</param>
        /// <returns>True if the value was changed.</returns>
        private bool ChangePropertyValue<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            if (!Equals(field, value))
            {
                field = value;
                RaisePropertyChanged(propertyName);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Raises the INotifyPropertyChanged event.
        /// </summary>
        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// ToString implementation.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Country;
        }
    }
}
