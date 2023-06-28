package crc6433871f4d78f40992;


public class LegendRenderer
	extends crc643961f13c85a497f7.AndroidRendererBase_2
	implements
		mono.android.IGCUserPeer,
		com.telerik.android.common.CollectionChangeListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_collectionChanged:(Lcom/telerik/android/common/CollectionChangedEvent;)V:GetCollectionChanged_Lcom_telerik_android_common_CollectionChangedEvent_Handler:Com.Telerik.Android.Common.ICollectionChangeListenerInvoker, Telerik.Android.Common\n" +
			"";
		mono.android.Runtime.register ("Telerik.Maui.Controls.Compatibility.ChartRenderer.Android.LegendRenderer, Telerik.Maui.Controls.Compatibility", LegendRenderer.class, __md_methods);
	}


	public LegendRenderer (android.content.Context p0)
	{
		super (p0);
		if (getClass () == LegendRenderer.class)
			mono.android.TypeManager.Activate ("Telerik.Maui.Controls.Compatibility.ChartRenderer.Android.LegendRenderer, Telerik.Maui.Controls.Compatibility", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public LegendRenderer (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == LegendRenderer.class)
			mono.android.TypeManager.Activate ("Telerik.Maui.Controls.Compatibility.ChartRenderer.Android.LegendRenderer, Telerik.Maui.Controls.Compatibility", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public LegendRenderer (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == LegendRenderer.class)
			mono.android.TypeManager.Activate ("Telerik.Maui.Controls.Compatibility.ChartRenderer.Android.LegendRenderer, Telerik.Maui.Controls.Compatibility", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, System.Private.CoreLib", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public LegendRenderer (android.content.Context p0, android.util.AttributeSet p1, int p2, int p3)
	{
		super (p0, p1, p2, p3);
		if (getClass () == LegendRenderer.class)
			mono.android.TypeManager.Activate ("Telerik.Maui.Controls.Compatibility.ChartRenderer.Android.LegendRenderer, Telerik.Maui.Controls.Compatibility", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, System.Private.CoreLib:System.Int32, System.Private.CoreLib", this, new java.lang.Object[] { p0, p1, p2, p3 });
	}


	public void collectionChanged (com.telerik.android.common.CollectionChangedEvent p0)
	{
		n_collectionChanged (p0);
	}

	private native void n_collectionChanged (com.telerik.android.common.CollectionChangedEvent p0);

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
