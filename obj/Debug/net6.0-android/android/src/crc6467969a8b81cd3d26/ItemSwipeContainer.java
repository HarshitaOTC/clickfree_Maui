package crc6467969a8b81cd3d26;


public class ItemSwipeContainer
	extends crc6467969a8b81cd3d26.CellContainerBase
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Telerik.Maui.Controls.Compatibility.DataControlsRenderer.Android.ListView.ItemSwipeContainer, Telerik.Maui.Controls.Compatibility", ItemSwipeContainer.class, __md_methods);
	}


	public ItemSwipeContainer (android.content.Context p0)
	{
		super (p0);
		if (getClass () == ItemSwipeContainer.class)
			mono.android.TypeManager.Activate ("Telerik.Maui.Controls.Compatibility.DataControlsRenderer.Android.ListView.ItemSwipeContainer, Telerik.Maui.Controls.Compatibility", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public ItemSwipeContainer (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == ItemSwipeContainer.class)
			mono.android.TypeManager.Activate ("Telerik.Maui.Controls.Compatibility.DataControlsRenderer.Android.ListView.ItemSwipeContainer, Telerik.Maui.Controls.Compatibility", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public ItemSwipeContainer (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == ItemSwipeContainer.class)
			mono.android.TypeManager.Activate ("Telerik.Maui.Controls.Compatibility.DataControlsRenderer.Android.ListView.ItemSwipeContainer, Telerik.Maui.Controls.Compatibility", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, System.Private.CoreLib", this, new java.lang.Object[] { p0, p1, p2 });
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
