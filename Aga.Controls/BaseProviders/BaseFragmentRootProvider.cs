using System.Windows;
using System.Windows.Automation.Provider;

namespace Aga.Controls.BaseProviders
{
    /// <summary>
    /// A basic implementation of the FragmentRoot provider.  This adds on to Fragment
    /// the power to route certain queries to the sub-window elements in this tree,
    /// so it is usually implemented by the fragment that corresponds to the window handle.
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(true)]
    public abstract class BaseFragmentRootProvider : BaseFragmentProvider, IRawElementProviderFragmentRoot
    {
        public BaseFragmentRootProvider()
            : base(null /* parent */, null /* fragmentRoot */)
        {
            fragmentRoot = this;
        }

        #region IRawElementProviderFragmentRoot Members

        // Perform hit testing and testing and return the element that contains this point.
        // Point is given in screen coordinates.  Return null is the hit is on the fragment
        // root itself.
        public virtual IRawElementProviderFragment ElementProviderFromPoint(double x, double y)
        {
            return null;
        }

        // Return the fragment with keyboard focus, if there is one.
        public virtual IRawElementProviderFragment GetFocus()
        {
            return null;
        }

        #endregion

        // The fragment root usually has an HWND, so returning an empty bounding rect is OK.
        public override Rect BoundingRectangle
        {
            get { return new Rect(); }
        }

        // The fragment root usually has an HWND, so returning an empty runtime ID is OK.
        // The system will construct one for us.
        public override int[] GetRuntimeId()
        {
            return null;
        }
    }
}