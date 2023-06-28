package crc64ae89db5e44f47158;


public class RadScrollViewHandler_LaidOutListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.telerik.widget.primitives.panels.ScrollViewLaidOutListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onLaidOut:()V:GetOnLaidOutHandler:Com.Telerik.Widget.Primitives.Panels.IScrollViewLaidOutListenerInvoker, Telerik.Android.Primitives\n" +
			"";
		mono.android.Runtime.register ("Telerik.Maui.Handlers.RadScrollViewHandler+LaidOutListener, Telerik.Maui.Core", RadScrollViewHandler_LaidOutListener.class, __md_methods);
	}


	public RadScrollViewHandler_LaidOutListener ()
	{
		super ();
		if (getClass () == RadScrollViewHandler_LaidOutListener.class)
			mono.android.TypeManager.Activate ("Telerik.Maui.Handlers.RadScrollViewHandler+LaidOutListener, Telerik.Maui.Core", "", this, new java.lang.Object[] {  });
	}


	public void onLaidOut ()
	{
		n_onLaidOut ();
	}

	private native void n_onLaidOut ();

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
