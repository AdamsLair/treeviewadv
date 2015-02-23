using System;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Automation.Provider;
using Aga.Controls.BaseProviders;
using Aga.Controls.Tree;

namespace Aga.Controls.Providers
{
    public class TreeColumnProvider : BaseFragmentProvider, IRawElementProviderFragmentRoot, IValueProvider
    {
        #region Fields

        private readonly TreeViewAdv _control;
        private readonly int _idxColumn;
        private readonly TreeColumn _column;

        #endregion

        public TreeColumnProvider(TreeViewAdv control, IRawElementProviderFragmentRoot root, int idxColumn)
            : base(root, root)
        {
            _control = control;
            _idxColumn = idxColumn;
            _column = _control.Columns[idxColumn];

            var strName = _column.Header;
            var automationPropertyId = Regex.Replace(strName, @"[^0-9a-zA-Z]+", "");

            AddStaticProperty(UiaConstants.UIA_NamePropertyId, strName);
            AddStaticProperty(UiaConstants.UIA_ControlTypePropertyId, UiaConstants.UIA_CustomControlTypeId);
            AddStaticProperty(UiaConstants.UIA_LocalizedControlTypePropertyId, "TreeNodeColumn");
            AddStaticProperty(UiaConstants.UIA_ProviderDescriptionPropertyId, "Treenode in the tree, FTW!");
            AddStaticProperty(UiaConstants.UIA_AutomationIdPropertyId, "TreeNode.Column_" + automationPropertyId);
            AddStaticProperty(UiaConstants.UIA_IsKeyboardFocusablePropertyId, false);
            AddStaticProperty(UiaConstants.UIA_IsControlElementPropertyId, true);
            AddStaticProperty(UiaConstants.UIA_IsContentElementPropertyId, false);
        }

        /// <summary>
        /// Returns provider options
        /// </summary>
        public override ProviderOptions ProviderOptions
        {
            get
            {
                return (ProviderOptions) ((int) (ProviderOptions.ServerSideProvider |
                                                 ProviderOptions.UseComThreading));
            }
        }

        /// <summary>
        /// Gets Pattern provider from <paramref name="patternId" />
        /// </summary>
        /// <param name="patternId">PatternId</param>
        /// <returns>self</returns>
        public override object GetPatternProvider(int patternId)
        {
            if (patternId == UiaConstants.UIA_ValuePatternId)
            {
                return this;
            }

            return base.GetPatternProvider(patternId);
        }

        /// <summary>
        /// Create a runtime ID. The runtime id should be unique on the entire desktop, 
        /// for the lifetime of the fragment.
        /// </summary>
        /// <returns></returns>
        public override int[] GetRuntimeId()
        {
            var runtimeId = new int[2];

            runtimeId[0] = UiaConstants.AppendRuntimeId;
            runtimeId[1] = _idxColumn;

            return runtimeId;
        }

        /// <summary>
        /// Get the bounding rect by consulting the control.
        /// </summary>
        public override Rect BoundingRectangle
        {
            get
            {
                var left = _control.Columns.Take(_idxColumn).Sum(c => c.Width);
                var screenRect =
                    _control.RectangleToScreen(new Rectangle(left, 0, _column.Width, _control.ColumnHeaderHeight));
                var margin = _control.Margin;
                return new Rect(screenRect.Left - margin.Left, screenRect.Top, screenRect.Width, screenRect.Height);
            }
        }

        /// <summary>
        /// Return first child.
        /// </summary>
        /// <returns>First direct child</returns>
        protected override IRawElementProviderFragment GetFirstChild()
        {
            return null;
        }

        /// <summary>
        /// Returns last child.
        /// </summary>
        /// <returns>last child node</returns>
        protected override IRawElementProviderFragment GetLastChild()
        {
            return null;
        }

        /// <summary>
        /// Return next nodeat the same level of this node.
        /// </summary>
        /// <returns>Nest sibling node</returns>
        protected override IRawElementProviderFragment GetNextSibling()
        {
            if (_idxColumn < _control.Columns.Count - 1)
            {
                return new TreeColumnProvider(_control, this, _idxColumn + 1);
            }

            return null;
        }

        /// <summary>
        /// Gets the preceeding node at the same level.
        /// </summary>
        /// <returns>Preceeding node.</returns>
        protected override IRawElementProviderFragment GetPreviousSibling()
        {
            if (_idxColumn > 0)
            {
                return new TreeColumnProvider(_control, this, _idxColumn - 1);
            }

            return null;
        }

        /// <summary>
        /// Value of the node
        /// </summary>
        public string Value
        {
            get { return _column.Header; }
        }

        /// <summary>
        /// Header is always readonly
        /// </summary>
        bool IValueProvider.IsReadOnly
        {
            get { return true; }
        }

        /// <summary>
        /// sets the value of the node
        /// </summary>
        /// <param name="value">value to set</param>
        public void SetValue(string value)
        {
            throw new InvalidOperationException("Header value is read-only.");
        }


        /// <summary>
        /// Returns element provider from clicked point in the application.
        /// </summary>
        /// <param name="x">x-coordinate</param>
        /// <param name="y">y-coordinate</param>
        /// <returns>Provider to do some magic</returns>
        public IRawElementProviderFragment ElementProviderFromPoint(double x, double y)
        {
            var node = _control.GetNodeAt(new System.Drawing.Point((int) x, (int) y));
            return new TreeColumnProvider(_control, this, node.Index);
        }

        /// <summary>
        /// Gets the element with current keyboard focus.
        /// </summary>
        /// <returns>focused node</returns>
        public IRawElementProviderFragment GetFocus()
        {
            if (_control.Columns != null && _control.Columns.Any(n => n.IsVisible))
            {
                return (IRawElementProviderFragment) _control.Columns[_idxColumn];
            }
            return null;
        }


        /// <summary>
        /// Get raw element provider from window handle.
        /// </summary>
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