package crc6496c114744893b7f7;


public class ExtendedListViewHolder
	extends com.telerik.widget.list.ListViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Telerik.Maui.Controls.Compatibility.DataControlsRenderer.Android.ExtendedListViewHolder, Telerik.Maui.Controls.Compatibility", ExtendedListViewHolder.class, __md_methods);
	}


	public ExtendedListViewHolder (android.view.View p0)
	{
		super (p0);
		if (getClass () == ExtendedListViewHolder.class)
			mono.android.TypeManager.Activate ("Telerik.Maui.Controls.Compatibility.DataControlsRenderer.Android.ExtendedListViewHolder, Telerik.Maui.Controls.Compatibility", "Android.Views.View, Mono.Android", this, new java.lang.Object[] { p0 });
	}

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
