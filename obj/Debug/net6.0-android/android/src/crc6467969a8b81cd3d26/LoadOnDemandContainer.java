package crc6467969a8b81cd3d26;


public class LoadOnDemandContainer
	extends crc6498b7b6d262a005e3.RadMauiViewContainer
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Telerik.Maui.Controls.Compatibility.DataControlsRenderer.Android.ListView.LoadOnDemandContainer, Telerik.Maui.Controls.Compatibility", LoadOnDemandContainer.class, __md_methods);
	}


	public LoadOnDemandContainer (android.content.Context p0)
	{
		super (p0);
		if (getClass () == LoadOnDemandContainer.class)
			mono.android.TypeManager.Activate ("Telerik.Maui.Controls.Compatibility.DataControlsRenderer.Android.ListView.LoadOnDemandContainer, Telerik.Maui.Controls.Compatibility", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public LoadOnDemandContainer (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == LoadOnDemandContainer.class)
			mono.android.TypeManager.Activate ("Telerik.Maui.Controls.Compatibility.DataControlsRenderer.Android.ListView.LoadOnDemandContainer, Telerik.Maui.Controls.Compatibility", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public LoadOnDemandContainer (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == LoadOnDemandContainer.class)
			mono.android.TypeManager.Activate ("Telerik.Maui.Controls.Compatibility.DataControlsRenderer.Android.ListView.LoadOnDemandContainer, Telerik.Maui.Controls.Compatibility", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, System.Private.CoreLib", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public LoadOnDemandContainer (android.content.Context p0, android.util.AttributeSet p1, int p2, int p3)
	{
		super (p0, p1, p2, p3);
		if (getClass () == LoadOnDemandContainer.class)
			mono.android.TypeManager.Activate ("Telerik.Maui.Controls.Compatibility.DataControlsRenderer.Android.ListView.LoadOnDemandContainer, Telerik.Maui.Controls.Compatibility", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, System.Private.CoreLib:System.Int32, System.Private.CoreLib", this, new java.lang.Object[] { p0, p1, p2, p3 });
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
