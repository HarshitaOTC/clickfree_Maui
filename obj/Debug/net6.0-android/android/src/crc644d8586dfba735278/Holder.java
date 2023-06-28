package crc644d8586dfba735278;


public class Holder
	extends androidx.recyclerview.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Telerik.Maui.ItemsControl.Holder, Telerik.Maui.Core", Holder.class, __md_methods);
	}


	public Holder (android.view.View p0)
	{
		super (p0);
		if (getClass () == Holder.class)
			mono.android.TypeManager.Activate ("Telerik.Maui.ItemsControl.Holder, Telerik.Maui.Core", "Android.Views.View, Mono.Android", this, new java.lang.Object[] { p0 });
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
