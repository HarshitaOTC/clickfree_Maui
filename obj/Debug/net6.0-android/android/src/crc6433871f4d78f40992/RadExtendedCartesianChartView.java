package crc6433871f4d78f40992;


public class RadExtendedCartesianChartView
	extends com.telerik.widget.chart.visualization.cartesianChart.RadCartesianChartView
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onMeasure:(II)V:GetOnMeasure_IIHandler\n" +
			"";
		mono.android.Runtime.register ("Telerik.Maui.Controls.Compatibility.ChartRenderer.Android.RadExtendedCartesianChartView, Telerik.Maui.Controls.Compatibility", RadExtendedCartesianChartView.class, __md_methods);
	}


	public RadExtendedCartesianChartView (android.content.Context p0)
	{
		super (p0);
		if (getClass () == RadExtendedCartesianChartView.class)
			mono.android.TypeManager.Activate ("Telerik.Maui.Controls.Compatibility.ChartRenderer.Android.RadExtendedCartesianChartView, Telerik.Maui.Controls.Compatibility", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public RadExtendedCartesianChartView (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == RadExtendedCartesianChartView.class)
			mono.android.TypeManager.Activate ("Telerik.Maui.Controls.Compatibility.ChartRenderer.Android.RadExtendedCartesianChartView, Telerik.Maui.Controls.Compatibility", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public void onMeasure (int p0, int p1)
	{
		n_onMeasure (p0, p1);
	}

	private native void n_onMeasure (int p0, int p1);

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
