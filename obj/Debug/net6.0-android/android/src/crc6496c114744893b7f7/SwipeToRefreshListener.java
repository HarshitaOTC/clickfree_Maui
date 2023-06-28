package crc6496c114744893b7f7;


public class SwipeToRefreshListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.telerik.widget.list.SwipeRefreshBehavior.SwipeRefreshListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onRefreshRequested:()V:GetOnRefreshRequestedHandler:Com.Telerik.Widget.List.SwipeRefreshBehavior/ISwipeRefreshListenerInvoker, Telerik.Android.List\n" +
			"";
		mono.android.Runtime.register ("Telerik.Maui.Controls.Compatibility.DataControlsRenderer.Android.SwipeToRefreshListener, Telerik.Maui.Controls.Compatibility", SwipeToRefreshListener.class, __md_methods);
	}


	public SwipeToRefreshListener ()
	{
		super ();
		if (getClass () == SwipeToRefreshListener.class)
			mono.android.TypeManager.Activate ("Telerik.Maui.Controls.Compatibility.DataControlsRenderer.Android.SwipeToRefreshListener, Telerik.Maui.Controls.Compatibility", "", this, new java.lang.Object[] {  });
	}


	public void onRefreshRequested ()
	{
		n_onRefreshRequested ();
	}

	private native void n_onRefreshRequested ();

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
