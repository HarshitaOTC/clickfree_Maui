package crc643961f13c85a497f7;


public class Procedure_1
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.telerik.android.common.Procedure
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_apply:(Ljava/lang/Object;)V:GetApply_Ljava_lang_Object_Handler:Com.Telerik.Android.Common.IProcedureInvoker, Telerik.Android.Common\n" +
			"";
		mono.android.Runtime.register ("Telerik.Maui.Controls.Compatibility.Common.Android.Procedure`1, Telerik.Maui.Controls.Compatibility", Procedure_1.class, __md_methods);
	}


	public Procedure_1 ()
	{
		super ();
		if (getClass () == Procedure_1.class)
			mono.android.TypeManager.Activate ("Telerik.Maui.Controls.Compatibility.Common.Android.Procedure`1, Telerik.Maui.Controls.Compatibility", "", this, new java.lang.Object[] {  });
	}


	public void apply (java.lang.Object p0)
	{
		n_apply (p0);
	}

	private native void n_apply (java.lang.Object p0);

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
