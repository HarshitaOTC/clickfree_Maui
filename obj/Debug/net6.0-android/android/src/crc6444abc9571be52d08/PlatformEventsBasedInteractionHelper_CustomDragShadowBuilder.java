package crc6444abc9571be52d08;


public class PlatformEventsBasedInteractionHelper_CustomDragShadowBuilder
	extends android.view.View.DragShadowBuilder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onProvideShadowMetrics:(Landroid/graphics/Point;Landroid/graphics/Point;)V:GetOnProvideShadowMetrics_Landroid_graphics_Point_Landroid_graphics_Point_Handler\n" +
			"";
		mono.android.Runtime.register ("Telerik.Maui.Controls.PlatformEventsBasedInteractionHelper+CustomDragShadowBuilder, Telerik.Maui.Controls", PlatformEventsBasedInteractionHelper_CustomDragShadowBuilder.class, __md_methods);
	}


	public PlatformEventsBasedInteractionHelper_CustomDragShadowBuilder ()
	{
		super ();
		if (getClass () == PlatformEventsBasedInteractionHelper_CustomDragShadowBuilder.class)
			mono.android.TypeManager.Activate ("Telerik.Maui.Controls.PlatformEventsBasedInteractionHelper+CustomDragShadowBuilder, Telerik.Maui.Controls", "", this, new java.lang.Object[] {  });
	}


	public PlatformEventsBasedInteractionHelper_CustomDragShadowBuilder (android.view.View p0)
	{
		super (p0);
		if (getClass () == PlatformEventsBasedInteractionHelper_CustomDragShadowBuilder.class)
			mono.android.TypeManager.Activate ("Telerik.Maui.Controls.PlatformEventsBasedInteractionHelper+CustomDragShadowBuilder, Telerik.Maui.Controls", "Android.Views.View, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public void onProvideShadowMetrics (android.graphics.Point p0, android.graphics.Point p1)
	{
		n_onProvideShadowMetrics (p0, p1);
	}

	private native void n_onProvideShadowMetrics (android.graphics.Point p0, android.graphics.Point p1);

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
