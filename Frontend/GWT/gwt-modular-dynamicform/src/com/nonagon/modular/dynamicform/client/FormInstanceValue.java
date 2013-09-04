package com.nonagon.modular.dynamicform.client;

import com.google.gwt.core.client.JavaScriptObject;

public class FormInstanceValue extends JavaScriptObject {
	
	public static FormInstanceValue create() {
		
		FormInstanceValue instance = ((FormInstanceValue)JavaScriptObject.createObject());
		instance.init();
		
		return instance;
	}
	
	protected final native void init()/*-{
		
		this.__type = "Nonagon.Modular.DynamicForm.FormInstanceValue, Nonagon.Modular.DynamicForm";
	}-*/;

	protected FormInstanceValue() {}

	public final native int getId()/*-{ return this.Id; }-*/;

	public final native int getFormInstanceId()/*-{ return this.FormInstanceId; }-*/;
	public final native void setFormInstanceId(int value)/*-{ this.FormInstanceId = value; }-*/;

	public final native int getFieldId()/*-{ return this.FieldId; }-*/;
	public final native void setFieldId(int value)/*-{ this.FieldId = value; }-*/;
	
	public final native String getFieldCode()/*-{ return this.FieldCode; }-*/;
	public final native void setFieldCode(String value)/*-{ this.FieldCode = value; }-*/;
	
	public final native String getFieldValue()/*-{ return this.Value; }-*/;
	public final native void setFieldValue(String value)/*-{ this.Value = value; }-*/;
}
