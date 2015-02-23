using System;
using System.Collections.Generic;
using System.Drawing;
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

        // Return a unique runtime ID to distinguish this from other elements.
        // This is required to implement.  It is usually best to return an array
        // starting with the AppendRuntimeId so that it will be joined to the
        // fragment root's runtime -- then you just need to add a unique suffix.
        public abstract int[] GetRuntimeId();
        IRawElementProviderSimple[] IRawElementProviderFragment.GetEmbeddedFragmentRoots()
        {
            return GetEmbeddedFragmentRoots();
        }

        // Return the bounding rectangle of the fragment.
        // This is required to implement.
        public abstract Rect BoundingRectangle { get; }

        // Return any fragment roots embedded within this fragment - uncommon
        // unless this is a fragment hosting another full HWND.
        private IRawElementProviderFragmentRoot[] GetEmbeddedFragmentRoots()
        {
            return null;
        }

        // Set focus to this fragment, if it is keyboard focusable.
        public virtual void SetFocus()
        {
        }

        // Return the fragment root: the fragment that is tied to the window handle itself.
        // Don't override, since the constructor requires the fragment root already.
        public IRawElementProviderFragmentRoot FragmentRoot
        {
            get { return fragmentRoot; }
        }

        // Routing function for going to neighboring elements.  We implemented
        // this to delegate to other virtual functions, so don't override it.
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

        // Return the first child of this fragment.
        protected virtual IRawElementProviderFragment GetFirstChild()
        {
            return null;
        }

        // Return the last child of this fragment.
        protected virtual IRawElementProviderFragment GetLastChild()
        {
            return null;
        }

        // Return the next sibling of this fragment.
        protected virtual IRawElementProviderFragment GetNextSibling()
        {
            return null;
        }

        // Return the previous sibling of this fragment.
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
