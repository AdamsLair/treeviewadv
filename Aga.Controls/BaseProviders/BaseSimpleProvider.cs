using System;
using System.Collections.Generic;
using System.Windows.Automation.Provider;

namespace Aga.Controls.BaseProviders
{
    [System.Runtime.InteropServices.ComVisible(true)]
    public abstract class BaseSimpleProvider : IRawElementProviderSimple
    {
        #region IRawElementProviderSimple Members

        public virtual object GetPatternProvider(int patternId)
        {
            return null;
        }

        public virtual object GetPropertyValue(int propertyId)
        {
            // Check the static props list first
            if (_staticProps.ContainsKey(propertyId))
            {
                return _staticProps[propertyId];
            }

            // Switching construct to go get the right property from a virtual method.
            if (propertyId == UiaConstants.UIA_NamePropertyId)
            {
                return GetName();
            }

            // Add further cases here to support more properties.
            // Do note that it may be more efficient to handle static properties
            // by adding them to the static props list instead of using methods.

            return null;
        }

        public virtual IRawElementProviderSimple HostRawElementProvider
        {
            get
            {
                var hwnd = GetWindowHandle();
                if (hwnd != IntPtr.Zero)
                {
                    IRawElementProviderSimple hostProvider;
                    NativeMethods.UiaHostProviderFromHwnd(GetWindowHandle(), out hostProvider);
                    return hostProvider;
                }

                return null;
            }
        }

        public virtual ProviderOptions ProviderOptions
        {
            get { return ProviderOptions.ServerSideProvider; }
        }

        #endregion

        #region Protected overrides

        // Get the window handle for a provider that is a full HWND
        protected virtual IntPtr GetWindowHandle()
        {
            return IntPtr.Zero;
        }

        // Get the localized name for this control
        protected virtual string GetName()
        {
            return null;
        }

        #endregion

        #region Other protected methods

        protected void AddStaticProperty(int propertyId, object value)
        {
            _staticProps.Add(propertyId, value);
        }

        #endregion

        #region Fields

        private readonly Dictionary<int, object> _staticProps = new Dictionary<int, object>();

        #endregion
    }
}