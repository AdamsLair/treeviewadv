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
        protected BaseFragmentRootProvider()
            : base(null, null)
        {
            fragmentRoot = this;
        }

        #region IRawElementProviderFragmentRoot Members

        /// <summary>
        /// Perform hit testing and testing and return the element that contains this point.
        /// Point is given in screen coordinates.  Return null is the hit is on the fragment
        /// root itself.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public virtual IRawElementProviderFragment ElementProviderFromPoint(double x, double y)
        {
            return null;
        }

        public virtual IRawElementProviderFragment GetFocus()
        {
            return null;
        }

        #endregion

        public override Rect BoundingRectangle
        {
            get { return new Rect(); }
        }

        public override int[] GetRuntimeId()
        {
            return null;
        }
    }
}