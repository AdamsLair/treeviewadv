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
            if (_staticProps.ContainsKey(propertyId))
            {
                return _staticProps[propertyId];
            }

            if (propertyId == UiaConstants.UIA_NamePropertyId)
            {
                return GetName();
            }

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

        protected virtual IntPtr GetWindowHandle()
        {
            return IntPtr.Zero;
        }

        protected virtual string GetName()
        {
            return null;
        }

        protected virtual IEnumerable<IRawElementProviderFragment> GetChildren()
        {
            return null;
        }

        protected virtual int GetChildCount()
        {
            return 0;
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