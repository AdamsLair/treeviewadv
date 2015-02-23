using System;
using System.Runtime.InteropServices;
using System.Windows.Automation.Provider;

namespace Aga.Controls
{
    public class NativeMethods
    {
        [DllImport("UIAutomationCore.dll", EntryPoint = "UiaHostProviderFromHwnd", CharSet = CharSet.Unicode)]
        public static extern int UiaHostProviderFromHwnd(IntPtr hwnd, [MarshalAs(UnmanagedType.Interface)] out IRawElementProviderSimple provider);

        [DllImport("UIAutomationCore.dll", EntryPoint = "UiaReturnRawElementProvider", CharSet = CharSet.Unicode)]
        public static extern IntPtr UiaReturnRawElementProvider(IntPtr hwnd, IntPtr wParam, IntPtr lParam, IRawElementProviderSimple el);

        [DllImport("UIAutomationCore.dll", EntryPoint = "UiaRaiseAutomationEvent", CharSet = CharSet.Unicode)]
        public static extern int UiaRaiseAutomationEvent(IRawElementProviderSimple el, int eventId);

        [DllImport("UIAutomationCore.dll", EntryPoint = "UiaRaiseAutomationPropertyChangedEvent", CharSet = CharSet.Unicode)]
        public static extern int UiaRaiseAutomationPropertyChangedEvent(IRawElementProviderSimple el, int propertyId, object oldValue, object newValue);
    }

    // Useful constants
    // Duplicated from UIAutomationClient.idl
    public class UiaConstants
    {
        public const int UIA_InvokePatternId = 10000;
        public const int UIA_SelectionPatternId = 10001;
        public const int UIA_ValuePatternId = 10002;
        public const int UIA_RangeValuePatternId = 10003;
        public const int UIA_ScrollPatternId = 10004;
        public const int UIA_ExpandCollapsePatternId = 10005;
        public const int UIA_GridPatternId = 10006;
        public const int UIA_GridItemPatternId = 10007;
        public const int UIA_MultipleViewPatternId = 10008;
        public const int UIA_WindowPatternId = 10009;
        public const int UIA_SelectionItemPatternId = 10010;
        public const int UIA_DockPatternId = 10011;
        public const int UIA_TablePatternId = 10012;
        public const int UIA_TableItemPatternId = 10013;
        public const int UIA_TextPatternId = 10014;
        public const int UIA_TogglePatternId = 10015;
        public const int UIA_TransformPatternId = 10016;
        public const int UIA_ScrollItemPatternId = 10017;
        public const int UIA_LegacyIAccessiblePatternId = 10018;
        public const int UIA_ItemContainerPatternId = 10019;
        public const int UIA_VirtualizedItemPatternId = 10020;
        public const int UIA_SynchronizedInputPatternId = 10021;

        public const int UIA_ToolTipOpenedEventId = 20000;
        public const int UIA_ToolTipClosedEventId = 20001;
        public const int UIA_StructureChangedEventId = 20002;
        public const int UIA_MenuOpenedEventId = 20003;
        public const int UIA_AutomationPropertyChangedEventId = 20004;
        public const int UIA_AutomationFocusChangedEventId = 20005;
        public const int UIA_AsyncContentLoadedEventId = 20006;
        public const int UIA_MenuClosedEventId = 20007;
        public const int UIA_LayoutInvalidatedEventId = 20008;
        public const int UIA_Invoke_InvokedEventId = 20009;
        public const int UIA_SelectionItem_ElementAddedToSelectionEventId = 20010;
        public const int UIA_SelectionItem_ElementRemovedFromSelectionEventId = 20011;
        public const int UIA_SelectionItem_ElementSelectedEventId = 20012;
        public const int UIA_Selection_InvalidatedEventId = 20013;
        public const int UIA_Text_TextSelectionChangedEventId = 20014;
        public const int UIA_Text_TextChangedEventId = 20015;
        public const int UIA_Window_WindowOpenedEventId = 20016;
        public const int UIA_Window_WindowClosedEventId = 20017;
        public const int UIA_MenuModeStartEventId = 20018;
        public const int UIA_MenuModeEndEventId = 20019;
        public const int UIA_InputReachedTargetEventId = 20020;
        public const int UIA_InputReachedOtherElementEventId = 20021;
        public const int UIA_InputDiscardedEventId = 20022;

        public const int UIA_RuntimeIdPropertyId = 30000;
        public const int UIA_BoundingRectanglePropertyId = 30001;
        public const int UIA_ProcessIdPropertyId = 30002;
        public const int UIA_ControlTypePropertyId = 30003;
        public const int UIA_LocalizedControlTypePropertyId = 30004;
        public const int UIA_NamePropertyId = 30005;
        public const int UIA_AcceleratorKeyPropertyId = 30006;
        public const int UIA_AccessKeyPropertyId = 30007;
        public const int UIA_HasKeyboardFocusPropertyId = 30008;
        public const int UIA_IsKeyboardFocusablePropertyId = 30009;
        public const int UIA_IsEnabledPropertyId = 30010;
        public const int UIA_AutomationIdPropertyId = 30011;
        public const int UIA_ClassNamePropertyId = 30012;
        public const int UIA_HelpTextPropertyId = 30013;
        public const int UIA_ClickablePointPropertyId = 30014;
        public const int UIA_CulturePropertyId = 30015;
        public const int UIA_IsControlElementPropertyId = 30016;
        public const int UIA_IsContentElementPropertyId = 30017;
        public const int UIA_LabeledByPropertyId = 30018;
        public const int UIA_IsPasswordPropertyId = 30019;
        public const int UIA_NativeWindowHandlePropertyId = 30020;
        public const int UIA_ItemTypePropertyId = 30021;
        public const int UIA_IsOffscreenPropertyId = 30022;
        public const int UIA_OrientationPropertyId = 30023;
        public const int UIA_FrameworkIdPropertyId = 30024;
        public const int UIA_IsRequiredForFormPropertyId = 30025;
        public const int UIA_ItemStatusPropertyId = 30026;
        public const int UIA_IsDockPatternAvailablePropertyId = 30027;
        public const int UIA_IsExpandCollapsePatternAvailablePropertyId = 30028;
        public const int UIA_IsGridItemPatternAvailablePropertyId = 30029;
        public const int UIA_IsGridPatternAvailablePropertyId = 30030;
        public const int UIA_IsInvokePatternAvailablePropertyId = 30031;
        public const int UIA_IsMultipleViewPatternAvailablePropertyId = 30032;
        public const int UIA_IsRangeValuePatternAvailablePropertyId = 30033;
        public const int UIA_IsScrollPatternAvailablePropertyId = 30034;
        public const int UIA_IsScrollItemPatternAvailablePropertyId = 30035;
        public const int UIA_IsSelectionItemPatternAvailablePropertyId = 30036;
        public const int UIA_IsSelectionPatternAvailablePropertyId = 30037;
        public const int UIA_IsTablePatternAvailablePropertyId = 30038;
        public const int UIA_IsTableItemPatternAvailablePropertyId = 30039;
        public const int UIA_IsTextPatternAvailablePropertyId = 30040;
        public const int UIA_IsTogglePatternAvailablePropertyId = 30041;
        public const int UIA_IsTransformPatternAvailablePropertyId = 30042;
        public const int UIA_IsValuePatternAvailablePropertyId = 30043;
        public const int UIA_IsWindowPatternAvailablePropertyId = 30044;
        public const int UIA_ValueValuePropertyId = 30045;
        public const int UIA_ValueIsReadOnlyPropertyId = 30046;
        public const int UIA_RangeValueValuePropertyId = 30047;
        public const int UIA_RangeValueIsReadOnlyPropertyId = 30048;
        public const int UIA_RangeValueMinimumPropertyId = 30049;
        public const int UIA_RangeValueMaximumPropertyId = 30050;
        public const int UIA_RangeValueLargeChangePropertyId = 30051;
        public const int UIA_RangeValueSmallChangePropertyId = 30052;
        public const int UIA_ScrollHorizontalScrollPercentPropertyId = 30053;
        public const int UIA_ScrollHorizontalViewSizePropertyId = 30054;
        public const int UIA_ScrollVerticalScrollPercentPropertyId = 30055;
        public const int UIA_ScrollVerticalViewSizePropertyId = 30056;
        public const int UIA_ScrollHorizontallyScrollablePropertyId = 30057;
        public const int UIA_ScrollVerticallyScrollablePropertyId = 30058;
        public const int UIA_SelectionSelectionPropertyId = 30059;
        public const int UIA_SelectionCanSelectMultiplePropertyId = 30060;
        public const int UIA_SelectionIsSelectionRequiredPropertyId = 30061;
        public const int UIA_GridRowCountPropertyId = 30062;
        public const int UIA_GridColumnCountPropertyId = 30063;
        public const int UIA_GridItemRowPropertyId = 30064;
        public const int UIA_GridItemColumnPropertyId = 30065;
        public const int UIA_GridItemRowSpanPropertyId = 30066;
        public const int UIA_GridItemColumnSpanPropertyId = 30067;
        public const int UIA_GridItemContainingGridPropertyId = 30068;
        public const int UIA_DockDockPositionPropertyId = 30069;
        public const int UIA_ExpandCollapseExpandCollapseStatePropertyId = 30070;
        public const int UIA_MultipleViewCurrentViewPropertyId = 30071;
        public const int UIA_MultipleViewSupportedViewsPropertyId = 30072;
        public const int UIA_WindowCanMaximizePropertyId = 30073;
        public const int UIA_WindowCanMinimizePropertyId = 30074;
        public const int UIA_WindowWindowVisualStatePropertyId = 30075;
        public const int UIA_WindowWindowInteractionStatePropertyId = 30076;
        public const int UIA_WindowIsModalPropertyId = 30077;
        public const int UIA_WindowIsTopmostPropertyId = 30078;
        public const int UIA_SelectionItemIsSelectedPropertyId = 30079;
        public const int UIA_SelectionItemSelectionContainerPropertyId = 30080;
        public const int UIA_TableRowHeadersPropertyId = 30081;
        public const int UIA_TableColumnHeadersPropertyId = 30082;
        public const int UIA_TableRowOrColumnMajorPropertyId = 30083;
        public const int UIA_TableItemRowHeaderItemsPropertyId = 30084;
        public const int UIA_TableItemColumnHeaderItemsPropertyId = 30085;
        public const int UIA_ToggleToggleStatePropertyId = 30086;
        public const int UIA_TransformCanMovePropertyId = 30087;
        public const int UIA_TransformCanResizePropertyId = 30088;
        public const int UIA_TransformCanRotatePropertyId = 30089;
        public const int UIA_IsLegacyIAccessiblePatternAvailablePropertyId = 30090;
        public const int UIA_LegacyIAccessibleChildIdPropertyId = 30091;
        public const int UIA_LegacyIAccessibleNamePropertyId = 30092;
        public const int UIA_LegacyIAccessibleValuePropertyId = 30093;
        public const int UIA_LegacyIAccessibleDescriptionPropertyId = 30094;
        public const int UIA_LegacyIAccessibleRolePropertyId = 30095;
        public const int UIA_LegacyIAccessibleStatePropertyId = 30096;
        public const int UIA_LegacyIAccessibleHelpPropertyId = 30097;
        public const int UIA_LegacyIAccessibleKeyboardShortcutPropertyId = 30098;
        public const int UIA_LegacyIAccessibleSelectionPropertyId = 30099;
        public const int UIA_LegacyIAccessibleDefaultActionPropertyId = 30100;
        public const int UIA_AriaRolePropertyId = 30101;
        public const int UIA_AriaPropertiesPropertyId = 30102;
        public const int UIA_IsDataValidForFormPropertyId = 30103;
        public const int UIA_ControllerForPropertyId = 30104;
        public const int UIA_DescribedByPropertyId = 30105;
        public const int UIA_FlowsToPropertyId = 30106;
        public const int UIA_ProviderDescriptionPropertyId = 30107;
        public const int UIA_IsItemContainerPatternAvailablePropertyId = 30108;
        public const int UIA_IsVirtualizedItemPatternAvailablePropertyId = 30109;
        public const int UIA_IsSynchronizedInputPatternAvailablePropertyId = 30110;

        public const int UIA_AnimationStyleAttributeId = 40000;
        public const int UIA_BackgroundColorAttributeId = 40001;
        public const int UIA_BulletStyleAttributeId = 40002;
        public const int UIA_CapStyleAttributeId = 40003;
        public const int UIA_CultureAttributeId = 40004;
        public const int UIA_FontNameAttributeId = 40005;
        public const int UIA_FontSizeAttributeId = 40006;
        public const int UIA_FontWeightAttributeId = 40007;
        public const int UIA_ForegroundColorAttributeId = 40008;
        public const int UIA_HorizontalTextAlignmentAttributeId = 40009;
        public const int UIA_IndentationFirstLineAttributeId = 40010;
        public const int UIA_IndentationLeadingAttributeId = 40011;
        public const int UIA_IndentationTrailingAttributeId = 40012;
        public const int UIA_IsHiddenAttributeId = 40013;
        public const int UIA_IsItalicAttributeId = 40014;
        public const int UIA_IsReadOnlyAttributeId = 40015;
        public const int UIA_IsSubscriptAttributeId = 40016;
        public const int UIA_IsSuperscriptAttributeId = 40017;
        public const int UIA_MarginBottomAttributeId = 40018;
        public const int UIA_MarginLeadingAttributeId = 40019;
        public const int UIA_MarginTopAttributeId = 40020;
        public const int UIA_MarginTrailingAttributeId = 40021;
        public const int UIA_OutlineStylesAttributeId = 40022;
        public const int UIA_OverlineColorAttributeId = 40023;
        public const int UIA_OverlineStyleAttributeId = 40024;
        public const int UIA_StrikethroughColorAttributeId = 40025;
        public const int UIA_StrikethroughStyleAttributeId = 40026;
        public const int UIA_TabsAttributeId = 40027;
        public const int UIA_TextFlowDirectionsAttributeId = 40028;
        public const int UIA_UnderlineColorAttributeId = 40029;
        public const int UIA_UnderlineStyleAttributeId = 40030;

        public const int UIA_ButtonControlTypeId = 50000;
        public const int UIA_CalendarControlTypeId = 50001;
        public const int UIA_CheckBoxControlTypeId = 50002;
        public const int UIA_ComboBoxControlTypeId = 50003;
        public const int UIA_EditControlTypeId = 50004;
        public const int UIA_HyperlinkControlTypeId = 50005;
        public const int UIA_ImageControlTypeId = 50006;
        public const int UIA_ListItemControlTypeId = 50007;
        public const int UIA_ListControlTypeId = 50008;
        public const int UIA_MenuControlTypeId = 50009;
        public const int UIA_MenuBarControlTypeId = 50010;
        public const int UIA_MenuItemControlTypeId = 50011;
        public const int UIA_ProgressBarControlTypeId = 50012;
        public const int UIA_RadioButtonControlTypeId = 50013;
        public const int UIA_ScrollBarControlTypeId = 50014;
        public const int UIA_SliderControlTypeId = 50015;
        public const int UIA_SpinnerControlTypeId = 50016;
        public const int UIA_StatusBarControlTypeId = 50017;
        public const int UIA_TabControlTypeId = 50018;
        public const int UIA_TabItemControlTypeId = 50019;
        public const int UIA_TextControlTypeId = 50020;
        public const int UIA_ToolBarControlTypeId = 50021;
        public const int UIA_ToolTipControlTypeId = 50022;
        public const int UIA_TreeControlTypeId = 50023;
        public const int UIA_TreeItemControlTypeId = 50024;
        public const int UIA_CustomControlTypeId = 50025;
        public const int UIA_GroupControlTypeId = 50026;
        public const int UIA_ThumbControlTypeId = 50027;
        public const int UIA_DataGridControlTypeId = 50028;
        public const int UIA_DataItemControlTypeId = 50029;
        public const int UIA_DocumentControlTypeId = 50030;
        public const int UIA_SplitButtonControlTypeId = 50031;
        public const int UIA_WindowControlTypeId = 50032;
        public const int UIA_PaneControlTypeId = 50033;
        public const int UIA_HeaderControlTypeId = 50034;
        public const int UIA_HeaderItemControlTypeId = 50035;
        public const int UIA_TableControlTypeId = 50036;
        public const int UIA_TitleBarControlTypeId = 50037;
        public const int UIA_SeparatorControlTypeId = 50038;

        public const int AppendRuntimeId = 3;
    }
}
