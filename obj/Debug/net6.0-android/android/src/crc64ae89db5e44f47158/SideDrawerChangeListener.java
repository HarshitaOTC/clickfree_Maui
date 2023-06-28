package crc64ae89db5e44f47158;


public class SideDrawerChangeListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.telerik.android.primitives.widget.sidedrawer.DrawerChangeListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onDrawerClosed:(Lcom/telerik/android/primitives/widget/sidedrawer/RadSideDrawer;)V:GetOnDrawerClosed_Lcom_telerik_android_primitives_widget_sidedrawer_RadSideDrawer_Handler:Com.Telerik.Android.Primitives.Widget.Sidedrawer.IDrawerChangeListenerInvoker, Telerik.Android.Primitives\n" +
			"n_onDrawerClosing:(Lcom/telerik/android/primitives/widget/sidedrawer/RadSideDrawer;)Z:GetOnDrawerClosing_Lcom_telerik_android_primitives_widget_sidedrawer_RadSideDrawer_Handler:Com.Telerik.Android.Primitives.Widget.Sidedrawer.IDrawerChangeListenerInvoker, Telerik.Android.Primitives\n" +
			"n_onDrawerOpened:(Lcom/telerik/android/primitives/widget/sidedrawer/RadSideDrawer;)V:GetOnDrawerOpened_Lcom_telerik_android_primitives_widget_sidedrawer_RadSideDrawer_Handler:Com.Telerik.Android.Primitives.Widget.Sidedrawer.IDrawerChangeListenerInvoker, Telerik.Android.Primitives\n" +
			"n_onDrawerOpening:(Lcom/telerik/android/primitives/widget/sidedrawer/RadSideDrawer;)Z:GetOnDrawerOpening_Lcom_telerik_android_primitives_widget_sidedrawer_RadSideDrawer_Handler:Com.Telerik.Android.Primitives.Widget.Sidedrawer.IDrawerChangeListenerInvoker, Telerik.Android.Primitives\n" +
			"";
		mono.android.Runtime.register ("Telerik.Maui.Handlers.SideDrawerChangeListener, Telerik.Maui.Core", SideDrawerChangeListener.class, __md_methods);
	}


	public SideDrawerChangeListener ()
	{
		super ();
		if (getClass () == SideDrawerChangeListener.class)
			mono.android.TypeManager.Activate ("Telerik.Maui.Handlers.SideDrawerChangeListener, Telerik.Maui.Core", "", this, new java.lang.Object[] {  });
	}


	public void onDrawerClosed (com.telerik.android.primitives.widget.sidedrawer.RadSideDrawer p0)
	{
		n_onDrawerClosed (p0);
	}

	private native void n_onDrawerClosed (com.telerik.android.primitives.widget.sidedrawer.RadSideDrawer p0);


	public boolean onDrawerClosing (com.telerik.android.primitives.widget.sidedrawer.RadSideDrawer p0)
	{
		return n_onDrawerClosing (p0);
	}

	private native boolean n_onDrawerClosing (com.telerik.android.primitives.widget.sidedrawer.RadSideDrawer p0);


	public void onDrawerOpened (com.telerik.android.primitives.widget.sidedrawer.RadSideDrawer p0)
	{
		n_onDrawerOpened (p0);
	}

	private native void n_onDrawerOpened (com.telerik.android.primitives.widget.sidedrawer.RadSideDrawer p0);


	public boolean onDrawerOpening (com.telerik.android.primitives.widget.sidedrawer.RadSideDrawer p0)
	{
		return n_onDrawerOpening (p0);
	}

	private native boolean n_onDrawerOpening (com.telerik.android.primitives.widget.sidedrawer.RadSideDrawer p0);

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
