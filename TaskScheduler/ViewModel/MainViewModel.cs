using GalaSoft.MvvmLight;

namespace TaskScheduler.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {

        }

        public override void Cleanup()
        {
            // Clean up if needed
            base.Cleanup();
        }
    }
}