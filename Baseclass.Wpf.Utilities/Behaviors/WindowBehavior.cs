namespace Baseclass.Wpf.Utilities.Behaviors
{
    using System.Windows;

    /// <summary>
    ///     Behavior which makes it possible to let the MainWindow flash for a specific number of times.
    /// </summary>
    public class WindowBehavior
    {
        #region Static Fields

        /// <summary>
        ///     Attached property which configures the number of times the window should flash when <see cref="FlashProperty" /> is
        ///     set to true.
        /// </summary>
        public static readonly DependencyProperty FlashCountProperty = DependencyProperty.RegisterAttached(
            "FlashCount", 
            typeof(uint), 
            typeof(WindowBehavior), 
            new PropertyMetadata((uint)2));

        /// <summary>
        ///     Attached property which lets the window flash for <see cref="FlashCountProperty" /> times.
        /// </summary>
        public static readonly DependencyProperty FlashProperty = DependencyProperty.RegisterAttached(
            "Flash", 
            typeof(bool), 
            typeof(WindowBehavior), 
            new PropertyMetadata(default(bool), FlashPropertyChanged));

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Gets the value of the <see cref="FlashProperty"/> for a specific <see cref="UIElement"/>.
        /// </summary>
        /// <param name="element">
        /// The <see cref="UIElement"/> from which to get the attached property value.
        /// </param>
        /// <returns>
        /// The value if the window has been set to flash.
        /// </returns>
        public static bool GetFlash(UIElement element)
        {
            return (bool)element.GetValue(FlashProperty);
        }

        /// <summary>
        /// Gets the value of the <see cref="FlashCountProperty"/> for a specific <see cref="UIElement"/>.
        /// </summary>
        /// <param name="element">
        /// The <see cref="UIElement"/> from which to get the attached property value.
        /// </param>
        /// <returns>
        /// The number of times the window should flash.
        /// </returns>
        public static uint GetFlashCount(UIElement element)
        {
            return (uint)element.GetValue(FlashCountProperty);
        }

        /// <summary>
        /// Lets the MainWindow flash for <see cref="FlashCountProperty"/> number of times.
        /// </summary>
        /// <param name="element">
        /// The <see cref="UIElement"/> from which to get the attached property value.
        /// </param>
        /// <param name="value">
        /// <c>true</c> if the window should flash for a moment.
        /// </param>
        public static void SetFlash(UIElement element, bool value)
        {
            element.SetValue(FlashProperty, value);
        }

        /// <summary>
        /// Sets the number of times the window should flash.
        /// </summary>
        /// <param name="element">
        /// The <see cref="UIElement"/> from which to get the attached property value.
        /// </param>
        /// <param name="value">
        /// Number of times the window should flash.
        /// </param>
        public static void SetFlashCount(UIElement element, uint value)
        {
            element.SetValue(FlashCountProperty, value);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Delegate which handles the changes of the <see cref="FlashProperty"/>.
        ///     Makes the window flash if set to true, stops it otherwise.
        /// </summary>
        /// <param name="dependencyObject">
        /// The element which has the property attached to it.
        /// </param>
        /// <param name="dependencyPropertyChangedEventArgs">
        /// Provides data for various property changed events.
        /// </param>
        private static void FlashPropertyChanged(
            DependencyObject dependencyObject, 
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            if ((bool)dependencyPropertyChangedEventArgs.NewValue)
            {
                uint flashCount = GetFlashCount((UIElement)dependencyObject);

                Application.Current.MainWindow.FlashWindow(flashCount);
            }
            else
            {
                Application.Current.MainWindow.StopFlashingWindow();
            }
        }

        #endregion
    }
}