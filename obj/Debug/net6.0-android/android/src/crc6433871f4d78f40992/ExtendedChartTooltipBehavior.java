package crc6433871f4d78f40992;


public class ExtendedChartTooltipBehavior
	extends com.telerik.widget.chart.visualization.behaviors.ChartTooltipBehavior
	implements
		mono.android.IGCUserPeer,
		android.view.ViewTreeObserver.OnScrollChangedListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onScrollChanged:()V:GetOnScrollChangedHandler:Android.Views.ViewTreeObserver/IOnScrollChangedListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("Telerik.Maui.Controls.Compatibility.ChartRenderer.Android.ExtendedChartTooltipBehavior, Telerik.Maui.Controls.Compatibility", ExtendedChartTooltipBehavior.class, __md_methods);
	}


	public ExtendedChartTooltipBehavior (android.content.Context p0)
	{
		super (p0);
		if (getClass () == ExtendedChartTooltipBehavior.class)
			mono.android.TypeManager.Activate ("Telerik.Maui.Controls.Compatibility.ChartRenderer.Android.ExtendedChartTooltipBehavior, Telerik.Maui.Controls.Compatibility", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public void onScrollChanged ()
	{
		n_onScrollChanged ();
	}

	private native void n_onScrollChanged ();

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
