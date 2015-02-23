using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Automation.Provider;
using Aga.Controls.BaseProviders;
using Aga.Controls.Tree;

namespace Aga.Controls.Providers
{
    public class TreeNodeAdvProvider : BaseFragmentProvider, IRawElementProviderFragmentRoot, IValueProvider
    {
        #region Fields

        private readonly TreeViewAdv _control;
        private readonly int _idxRow;
        private readonly TreeNodeAdv _node;

        #endregion

        public TreeNodeAdvProvider(TreeViewAdv control, IRawElementProviderFragmentRoot root, int idxRow)
            : base(root, root)
        {
            _control = control;
            _idxRow = idxRow;
            _node = control.RowMap.ElementAt(idxRow);

            var strName = _node.Tag.ToString();
            var automationPropertyId = Regex.Replace(strName, @"[^0-9a-zA-Z]+", "");

            AddStaticProperty(UiaConstants.UIA_NamePropertyId, strName);
            AddStaticProperty(UiaConstants.UIA_ControlTypePropertyId, UiaConstants.UIA_CustomControlTypeId);
            AddStaticProperty(UiaConstants.UIA_LocalizedControlTypePropertyId, "TreeNode");
            AddStaticProperty(UiaConstants.UIA_ProviderDescriptionPropertyId, "Treenode in the tree, FTW!");
            AddStaticProperty(UiaConstants.UIA_AutomationIdPropertyId, "TreeNode_" + automationPropertyId);
            AddStaticProperty(UiaConstants.UIA_IsKeyboardFocusablePropertyId, false);
            AddStaticProperty(UiaConstants.UIA_IsControlElementPropertyId, true);
            AddStaticProperty(UiaConstants.UIA_IsContentElementPropertyId, false);
        }

        /// <summary>
        /// Returns provider options
        /// </summary>
        public override ProviderOptions ProviderOptions
        {
            // Request COM threading style - all calls on main thread
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
            runtimeId[1] = _idxRow;

            return runtimeId;
        }

        /// <summary>
        /// Get the bounding rect by consulting the control.
        /// </summary>
        public override Rect BoundingRectangle
        {
            get
            {
                // Bounding rects must be in screen coordinates
                var screenRect = _control.RectangleToScreen(_control.GetNodeBounds(_node));

                var margin = _control.Margin;
                var offsetTop = _control.ColumnHeaderHeight;
                var offsetLeft = margin.Left/2;

                return new Rect(screenRect.Left + offsetLeft, screenRect.Top + offsetTop, screenRect.Width,
                    screenRect.Height);
            }
        }

        /// <summary>
        /// Return first child.
        /// </summary>
        /// <returns>First direct child</returns>
        protected override IRawElementProviderFragment GetFirstChild()
        {
            if (_node.Children.Count > 0)
            {
                return new TreeNodeAdvProvider(_control, this, 0);
            }

            return null;
        }

        /// <summary>
        /// Returns last child.
        /// </summary>
        /// <returns>last child node</returns>
        protected override IRawElementProviderFragment GetLastChild()
        {
            if (_node.Children.Count > 0)
            {
                return new TreeNodeAdvProvider(_control, this, _node.Children.Count - 1);
            }

            return null;
        }

        /// <summary>
        /// Return next nodeat the same level of this node.
        /// </summary>
        /// <returns>Nest sibling node</returns>
        protected override IRawElementProviderFragment GetNextSibling()
        {
            if (_idxRow < _node.Children.Count - 1)
            {
                return new TreeNodeAdvProvider(_control, this, _idxRow + 1);
            }

            return null;
        }

        /// <summary>
        /// Gets the preceeding node at the same level.
        /// </summary>
        /// <returns>Preceeding node.</returns>
        protected override IRawElementProviderFragment GetPreviousSibling()
        {
            if (_idxRow > 0)
            {
                return new TreeNodeAdvProvider(_control, this, _idxRow - 1);
            }

            return null;
        }

        /// <summary>
        /// Value of the node
        /// </summary>
        public string Value
        {
            get { return _node.Tag.ToString(); }
        }
        
        /// <summary>
        /// Returns if node is readonly
        /// </summary>
        bool IValueProvider.IsReadOnly
        {
            get { return _node.IsHidden; }
        }

        /// <summary>
        /// sets the value of the node
        /// </summary>
        /// <param name="value">value to set</param>
        public void SetValue(string value)
        {
            throw new InvalidOperationException("Node value is read-only.");
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
            return new TreeNodeAdvProvider(_control, this, node.Row);
        }

        /// <summary>
        /// Gets the element with current keyboard focus.
        /// </summary>
        /// <returns>focused node</returns>
        public IRawElementProviderFragment GetFocus()
        {
            if (_control.RowMap != null && _control.RowMap.Any(n => n.IsSelected))
            {
                return (IRawElementProviderFragment) _control.RowMap.Where(n => n.IsSelected);
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

        /// <summary>
        /// Gets all child nodes non-recursive
        /// </summary>
        /// <returns>IEnumerable of child nodes</returns>
        protected override IEnumerable<IRawElementProviderFragment> GetChildren()
        {
            if (_node.Children.Count > 0)
            {
                foreach (var child in _node.Children)
                {
                    yield return new TreeNodeAdvProvider(_control, this, child.Row);
                }
            }
        }
        
        /// <summary>
        /// Gets all child nodes non-recursive
        /// </summary>
        /// <returns>IEnumerable of child nodes</returns>
        public IEnumerable<IRawElementProviderFragment> Children
        {
            get { return GetChildren();}
        }

        /// <summary>
        /// Get count of Childnodes.
        /// </summary>
        /// <returns>Childcount</returns>
        protected override int GetChildCount()
        {
            return _node.Children.Any() ? _node.Children.Count : 0;
        }

        /// <summary>
        /// Get count of Childnodes.
        /// </summary>
        /// <returns>Childcount</returns>
        public int ChildCount { get { return GetChildCount();} }

        /// <summary>
        /// Get ChildItems.
        /// </summary>
        protected IEnumerable<IRawElementProviderFragment> Items
        {
            get { return GetChildren(); }
        }
    }
}