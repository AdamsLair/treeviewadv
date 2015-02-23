using System;
using System.Linq;
using System.Windows;
using System.Windows.Automation.Provider;
using Aga.Controls.BaseProviders;
using Aga.Controls.Tree;

namespace Aga.Controls.Providers
{
    public class TreeModelProvider : BaseFragmentProvider, IValueProvider
    {
        // We know the Bar's parent is always the fragment root in this sample.
        public TreeModelProvider(TreeViewAdv control, IRawElementProviderFragmentRoot root, int idxRow)
            : base((IRawElementProviderFragment)root /* parent */, root /* fragmentRoot */)
        {
            this.control = control;
            _idxRow = idxRow;
            _node = _node = control.RowMap.ElementAt(idxRow);

            var strName = control.RowMap.ElementAt(idxRow).Tag.ToString(); ;

            // Populate static properties
            //
            // In a production app, Name should be localized
            AddStaticProperty(UiaConstants.UIA_NamePropertyId, strName);
            AddStaticProperty(UiaConstants.UIA_ControlTypePropertyId, UiaConstants.UIA_CustomControlTypeId);

            // In a production app, LocalizedControlType should be localized
            AddStaticProperty(UiaConstants.UIA_LocalizedControlTypePropertyId, "Chart Bar");
            AddStaticProperty(UiaConstants.UIA_ProviderDescriptionPropertyId, "UIASamples: Chart Bar Provider");

            // The automation id should be unique amongst the fragments siblings, and consistent between sessions.
            AddStaticProperty(UiaConstants.UIA_AutomationIdPropertyId, "ChartBar" + strName);

            AddStaticProperty(UiaConstants.UIA_IsKeyboardFocusablePropertyId, false);
            AddStaticProperty(UiaConstants.UIA_IsControlElementPropertyId, true);
            AddStaticProperty(UiaConstants.UIA_IsContentElementPropertyId, false);
        }

        public override ProviderOptions ProviderOptions
        {
            // Request COM threading style - all calls on main thread
            get
            {
                return (ProviderOptions)((int)(ProviderOptions.ServerSideProvider |
                                               ProviderOptions.UseComThreading));
            }
        }

        public override object GetPatternProvider(int patternId)
        {
            if (patternId == UiaConstants.UIA_ValuePatternId)
            {
                return this;
            }

            return base.GetPatternProvider(patternId);
        }

        // Create a runtime ID. The runtime id should be unique on the entire desktop, for the lifetime of the fragment.
        public override int[] GetRuntimeId()
        {
            int[] runtimeId = new int[2];

            runtimeId[0] = UiaConstants.AppendRuntimeId;
            runtimeId[1] = _node.Index;

            return runtimeId;
        }

        // Get the bounding rect by consulting the control.
        // Get the bounding rect by consulting the control.
        public override Rect BoundingRectangle
        {
            get
            {
                // Bounding rects must be in screen coordinates
                var screenRect = control.RectangleToScreen(control.GetNodeBounds(_node));

                var result = new Rect(screenRect.Left, screenRect.Top, screenRect.Width, screenRect.Height);

                return result;
            }
        }

        protected override IRawElementProviderFragment GetFirstChild()
        {
            // Return our first child, which is the first section in the bar.
            if (_node.Children.Count > 0)
            {
                return new TreeModelProvider(control, new TreeViewAdvProvider(control), 0);
            }

            return null;
        }

        protected override IRawElementProviderFragment GetLastChild()
        {
            // Return our last child, which is the last section in the bar.
            if (_node.Children.Count > 0)
            {
                return new TreeModelProvider(control, new TreeViewAdvProvider(control), _node.Children.Count - 1);
            }

            return null;
        }

        protected override IRawElementProviderFragment GetNextSibling()
        {
            // Return the fragment for the next bar in the chart.
            if (_idxRow < _node.Children.Count - 1)
            {
                return new TreeModelProvider(control, new TreeViewAdvProvider(control), _idxRow + 1);
            }

            return null;
        }

        protected override IRawElementProviderFragment GetPreviousSibling()
        {
            // Return the fragment for the previous bar in the chart.
            if (_idxRow > 0)
            {
                return new TreeModelProvider(control, new TreeViewAdvProvider(control), _idxRow - 1);
            }

            return null;
        }

        // IValuePattern members.
        public bool IsReadOnly
        {
            get { return _node.IsHidden; }
        }

        public string Value
        {
            get { return _node.Tag.ToString(); }
        }

        public void SetValue(string value)
        {
            throw new InvalidOperationException("Chart bar is read-only.");
        }

        #region Fields

        private TreeViewAdv control;
        private int _idxRow;
        private TreeNodeAdv _node;
        private TreeNodeAdv _root;

        #endregion
    }
}
