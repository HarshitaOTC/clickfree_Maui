package crc64b72313ee0f5a60c4;


public class RadMauiCheckBox
	extends com.microsoft.maui.MauiViewGroup
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_isEnabled:()Z:GetIsEnabledHandler\n" +
			"n_setEnabled:(Z)V:GetSetEnabled_ZHandler\n" +
			"n_onInitializeAccessibilityNodeInfo:(Landroid/view/accessibility/AccessibilityNodeInfo;)V:GetOnInitializeAccessibilityNodeInfo_Landroid_view_accessibility_AccessibilityNodeInfo_Handler\n" +
			"n_onLayout:(ZIIII)V:GetOnLayout_ZIIIIHandler\n" +
			"n_onMeasure:(II)V:GetOnMeasure_IIHandler\n" +
			"";
		mono.android.Runtime.register ("Telerik.Maui.Platform.RadMauiCheckBox, Telerik.Maui.Core", RadMauiCheckBox.class, __md_methods);
	}


	public RadMauiCheckBox (android.content.Context p0)
	{
		super (p0);
		if (getClass () == RadMauiCheckBox.class)
			mono.android.TypeManager.Activate ("Telerik.Maui.Platform.RadMauiCheckBox, Telerik.Maui.Core", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public RadMauiCheckBox (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == RadMauiCheckBox.class)
			mono.android.TypeManager.Activate ("Telerik.Maui.Platform.RadMauiCheckBox, Telerik.Maui.Core", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public RadMauiCheckBox (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == RadMauiCheckBox.class)
			mono.android.TypeManager.Activate ("Telerik.Maui.Platform.RadMauiCheckBox, Telerik.Maui.Core", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, System.Private.CoreLib", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public boolean isEnabled ()
	{
		return n_isEnabled ();
	}

	private native boolean n_isEnabled ();


	public void setEnabled (boolean p0)
	{
		n_setEnabled (p0);
	}

	private native void n_setEnabled (boolean p0);


	public void onInitializeAccessibilityNodeInfo (android.view.accessibility.AccessibilityNodeInfo p0)
	{
		n_onInitializeAccessibilityNodeInfo (p0);
	}

	private native void n_onInitializeAccessibilityNodeInfo (android.view.accessibility.AccessibilityNodeInfo p0);


	public void onLayout (boolean p0, int p1, int p2, int p3, int p4)
	{
		n_onLayout (p0, p1, p2, p3, p4);
	}

	private native void n_onLayout (boolean p0, int p1, int p2, int p3, int p4);


	public void onMeasure (int p0, int p1)
	{
		n_onMeasure (p0, p1);
	}

	private native void n_onMeasure (int p0, int p1);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
