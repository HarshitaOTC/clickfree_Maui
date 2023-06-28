package mono.com.telerik.widget.dataform.visualization;


public class EditorValueChangedListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.telerik.widget.dataform.visualization.EditorValueChangedListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onEditorValueChanged:(Lcom/telerik/widget/dataform/engine/EntityProperty;Ljava/lang/Object;)V:GetOnEditorValueChanged_Lcom_telerik_widget_dataform_engine_EntityProperty_Ljava_lang_Object_Handler:Com.Telerik.Widget.Dataform.Visualization.IEditorValueChangedListenerInvoker, Telerik.Android.Input\n" +
			"";
		mono.android.Runtime.register ("Com.Telerik.Widget.Dataform.Visualization.IEditorValueChangedListenerImplementor, Telerik.Android.Input", EditorValueChangedListenerImplementor.class, __md_methods);
	}


	public EditorValueChangedListenerImplementor ()
	{
		super ();
		if (getClass () == EditorValueChangedListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Telerik.Widget.Dataform.Visualization.IEditorValueChangedListenerImplementor, Telerik.Android.Input", "", this, new java.lang.Object[] {  });
	}


	public void onEditorValueChanged (com.telerik.widget.dataform.engine.EntityProperty p0, java.lang.Object p1)
	{
		n_onEditorValueChanged (p0, p1);
	}

	private native void n_onEditorValueChanged (com.telerik.widget.dataform.engine.EntityProperty p0, java.lang.Object p1);

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
