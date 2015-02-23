using System;
using System.Windows;
using System.Windows.Automation.Provider;
using Aga.Controls.BaseProviders;
using Aga.Controls.Tree;

namespace Aga.Controls.Providers
{
    public class TreeViewAdvProvider : BaseFragmentRootProvider
    {
        public TreeViewAdvProvider(TreeViewAdv treeViewAdv)
        {
            _treeViewAdv = treeViewAdv;

            // Populate static properties
            //
            AddStaticProperty(UiaConstants.UIA_ControlTypePropertyId, UiaConstants.UIA_CustomControlTypeId);
            AddStaticProperty(UiaConstants.UIA_LocalizedControlTypePropertyId, "TreeView");
            AddStaticProperty(UiaConstants.UIA_ProviderDescriptionPropertyId, "Automation for TreeViewAdv");
            AddStaticProperty(UiaConstants.UIA_HelpTextPropertyId, "Accessible through Windows UI Automation.");

            // Used the Panel name for the good Automation ID of the Chart.
            AddStaticProperty(UiaConstants.UIA_AutomationIdPropertyId, _treeViewAdv.Name);

            AddStaticProperty(UiaConstants.UIA_IsKeyboardFocusablePropertyId, true);
            AddStaticProperty(UiaConstants.UIA_IsControlElementPropertyId, true);
            AddStaticProperty(UiaConstants.UIA_IsContentElementPropertyId, true);

            // Some properties are provided for me already by HWND provider
            // NativeWindowHandle, ProcessId, FrameworkId, IsEnabled, HasKeyboardFocus
        }

        public override ProviderOptions ProviderOptions
        {
            get
            {
                return (ProviderOptions) ((int) (ProviderOptions.ServerSideProvider |
                                                 ProviderOptions.UseComThreading));
            }
        }

        protected override IntPtr GetWindowHandle()
        {
            // Return our window handle, since the main Chart is a root provider.
            return _treeViewAdv.Handle;
        }

        protected override string GetName()
        {
            // This could be a static property, but here it demonstrates a method-provided property.
            // (A shipping app would localize this text.).

            return _treeViewAdv.Name;
        }

        public override Rect BoundingRectangle
        {
            get { return new Rect(_treeViewAdv.Top, _treeViewAdv.Left, _treeViewAdv.Width, _treeViewAdv.Height); }
        }

        protected override IRawElementProviderFragment GetFirstChild()
        {
            // Return the first child, which is the first bar in the chart.
            if (_treeViewAdv.RowCount > 0)
            {
                return new TreeNodeAdvProvider(_treeViewAdv, this, 0);
            }

            return null;
        }

        protected override IRawElementProviderFragment GetLastChild()
        {
            // Return the last child, which is the last bar in the chart.
            if (_treeViewAdv.RowCount > 0)
            {
                return new TreeNodeAdvProvider(_treeViewAdv, this, _treeViewAdv.RowCount - 1);
            }

            return null;
        }

        // Check to see if the passed point is a hit on one of our descendants.
        public override IRawElementProviderFragment ElementProviderFromPoint(double x, double y)
        {
            // Convert screen point to client point.
            System.Drawing.Point clientPoint = _treeViewAdv.PointToClient(new System.Drawing.Point((int) x, (int) y));


            var node = _treeViewAdv.GetNodeAt(clientPoint);
            if (node != null)
            {
                var treeNodeAdvProvider = new TreeNodeAdvProvider(_treeViewAdv, this, node.Row);

                return treeNodeAdvProvider;
            }

            return null;
        }

        #region Fields

        private readonly TreeViewAdv _treeViewAdv;

        #endregion
    }
}