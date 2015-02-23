using System;
using System.Windows;
using System.Windows.Automation.Provider;

namespace Aga.Controls.BaseProviders
{
    /// <summary>
    /// A basic implementation of the Fragment provider.
    /// This adds the concept of a sub-window element to the Simple provider,
    /// and so includes work to report element aspects that the system
    /// would usually get for free from a window handle, like position and runtime ID.
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(true)]
    public abstract class BaseFragmentProvider : BaseSimpleProvider, IRawElementProviderFragment
    {
        protected BaseFragmentProvider(IRawElementProviderFragment parent, IRawElementProviderFragmentRoot fragmentRoot)
        {
            this._parent = parent;
            this.fragmentRoot = fragmentRoot;
        }

        #region IRawElementProviderFragment Members

        public abstract int[] GetRuntimeId();
        IRawElementProviderSimple[] IRawElementProviderFragment.GetEmbeddedFragmentRoots()
        {
            return GetEmbeddedFragmentRoots();
        }

        public abstract Rect BoundingRectangle { get; }

        private IRawElementProviderFragmentRoot[] GetEmbeddedFragmentRoots()
        {
            return null;
        }

        public virtual void SetFocus()
        {
        }

        public IRawElementProviderFragmentRoot FragmentRoot
        {
            get { return fragmentRoot; }
        }

        public IRawElementProviderFragment Navigate(NavigateDirection direction)
        {
            switch (direction)
            {
                case NavigateDirection.Parent: return _parent;
                case NavigateDirection.FirstChild: return GetFirstChild();
                case NavigateDirection.LastChild: return GetLastChild();
                case NavigateDirection.NextSibling: return GetNextSibling();
                case NavigateDirection.PreviousSibling: return GetPreviousSibling();
            }
            return null;
        }

        #endregion

        #region Override points

        /// <summary>
        /// Return the first child of this fragment.
        /// </summary>
        /// <returns></returns>
        protected virtual IRawElementProviderFragment GetFirstChild()
        {
            return null;
        }

        /// <summary>
        /// Return the last child of this fragment.
        /// </summary>
        /// <returns></returns>
        protected virtual IRawElementProviderFragment GetLastChild()
        {
            return null;
        }

        /// <summary>
        /// Return the next sibling of this fragment.
        /// </summary>
        /// <returns></returns>
        protected virtual IRawElementProviderFragment GetNextSibling()
        {
            return null;
        }

        /// <summary>
        /// Return the previous sibling of this fragment.
        /// </summary>
        /// <returns></returns>
        protected virtual IRawElementProviderFragment GetPreviousSibling()
        {
            return null;
        }
        #endregion

        #region Protected fields

        private readonly IRawElementProviderFragment _parent;
        protected IRawElementProviderFragmentRoot fragmentRoot;

        #endregion

        public override ProviderOptions ProviderOptions
        {
            get
            {
                return (ProviderOptions)((int)(ProviderOptions.ServerSideProvider |
                                               ProviderOptions.UseComThreading));
            }
        }

        public override IRawElementProviderSimple HostRawElementProvider
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
    }
}
