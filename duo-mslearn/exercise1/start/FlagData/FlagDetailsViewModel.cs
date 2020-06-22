using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FlagData
{
    /// <summary>
    /// A class to hold our basic flag logic which flips between 
    /// flags and holds the current "state" of the view.
    /// </summary>
    public class FlagDetailsViewModel : INotifyPropertyChanged
    {
        FlagRepository repository;
        int currentFlag;

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// List of the countries
        /// </summary>
        public IList<string> Countries { get { return repository.Countries; } }

        /// <summary>
        /// List of flags
        /// </summary>
        public IList<Flag> Flags { get { return repository.Flags; } }

        /// <summary>
        /// Currently selected flag
        /// </summary>
        public Flag CurrentFlag
        {
            get
            {
                return repository.Flags[currentFlag];
            }

            set
            {
                int index = repository.Flags.IndexOf(value);
                if (index >= 0)
                {
                    currentFlag = index;
                    RaisePropertyChanged(nameof(CurrentFlag));
                }
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public FlagDetailsViewModel()
        {
            repository = new FlagRepository();
        }

        /// <summary>
        /// This moves to the next flag and wraps around the collection.
        /// </summary>
        public void MoveToNextFlag()
        {
            currentFlag++;
            if (currentFlag >= repository.Flags.Count)
                currentFlag = 0; // wrap

            // The CurrentFlag has changed.
            RaisePropertyChanged(nameof(CurrentFlag));
        }

        /// <summary>
        /// This moves to the previous flag and wraps around the collection.
        /// </summary>
        public void MoveToPreviousFlag()
        {
            currentFlag--;
            if (currentFlag < 0)
                currentFlag = repository.Flags.Count-1;

            // The CurrentFlag has changed.
            RaisePropertyChanged(nameof(CurrentFlag));
        }

        /// <summary>
        /// Raise the PropertyChanged notification.
        /// </summary>
        /// <param name="propertyName"></param>
        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
