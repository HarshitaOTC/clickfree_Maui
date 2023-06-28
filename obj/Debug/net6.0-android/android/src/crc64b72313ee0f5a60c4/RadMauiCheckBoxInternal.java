package crc64b72313ee0f5a60c4;


public class RadMauiCheckBoxInternal
	extends androidx.appcompat.widget.AppCompatCheckBox
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onInitializeAccessibilityNodeInfo:(Landroid/view/accessibility/AccessibilityNodeInfo;)V:GetOnInitializeAccessibilityNodeInfo_Landroid_view_accessibility_AccessibilityNodeInfo_Handler\n" +
			"n_onDraw:(Landroid/graphics/Canvas;)V:GetOnDraw_Landroid_graphics_Canvas_Handler\n" +
			"";
		mono.android.Runtime.register ("Telerik.Maui.Platform.RadMauiCheckBoxInternal, Telerik.Maui.Core", RadMauiCheckBoxInternal.class, __md_methods);
	}


	public RadMauiCheckBoxInternal (android.content.Context p0)
	{
		super (p0);
		if (getClass () == RadMauiCheckBoxInternal.class)
			mono.android.TypeManager.Activate ("Telerik.Maui.Platform.RadMauiCheckBoxInternal, Telerik.Maui.Core", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public RadMauiCheckBoxInternal (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == RadMauiCheckBoxInternal.class)
			mono.android.TypeManager.Activate ("Telerik.Maui.Platform.RadMauiCheckBoxInternal, Telerik.Maui.Core", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public RadMauiCheckBoxInternal (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == RadMauiCheckBoxInternal.class)
			mono.android.TypeManager.Activate ("Telerik.Maui.Platform.RadMauiCheckBoxInternal, Telerik.Maui.Core", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, System.Private.CoreLib", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public void onInitializeAccessibilityNodeInfo (android.view.accessibility.AccessibilityNodeInfo p0)
	{
		n_onInitializeAccessibilityNodeInfo (p0);
	}

	private native void n_onInitializeAccessibilityNodeInfo (android.view.accessibility.AccessibilityNodeInfo p0);


	public void onDraw (android.graphics.Canvas p0)
	{
		n_onDraw (p0);
	}

	private native void n_onDraw (android.graphics.Canvas p0);

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
