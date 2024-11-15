// ------------------------------------------------------------------------------
// <auto-generated>
//    Generated by avrogen, version 1.12.0+8c27801dc8d42ccc00997f25c0b8f45f8d4a233e
//    Changes to this file may cause incorrect behavior and will be lost if code
//    is regenerated
// </auto-generated>
// ------------------------------------------------------------------------------
namespace FoodDelivery.Kafka.Application.Messages.Orders
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	using global::Avro;
	using global::Avro.Specific;
	
	[global::System.CodeDom.Compiler.GeneratedCodeAttribute("avrogen", "1.12.0+8c27801dc8d42ccc00997f25c0b8f45f8d4a233e")]
	public partial class RemoveOrderItemMessage : global::Avro.Specific.ISpecificRecord
	{
		public static global::Avro.Schema _SCHEMA = global::Avro.Schema.Parse(@"{""type"":""record"",""name"":""RemoveOrderItemMessage"",""namespace"":""FoodDelivery.Kafka.Application.Messages.Orders"",""fields"":[{""name"":""OrderId"",""type"":""int""},{""name"":""ClientId"",""type"":""int""},{""name"":""OccurredOn"",""type"":""long""},{""name"":""MenuItemId"",""type"":""int""}]}");
		private int _orderId;
		private int _clientId;
		private long _occurredOn;
		private int _menuItemId;
		public virtual global::Avro.Schema Schema
		{
			get
			{
				return RemoveOrderItemMessage._SCHEMA;
			}
		}
		public int OrderId
		{
			get
			{
				return this._orderId;
			}
			set
			{
				this._orderId = value;
			}
		}
		public int ClientId
		{
			get
			{
				return this._clientId;
			}
			set
			{
				this._clientId = value;
			}
		}
		public long OccurredOn
		{
			get
			{
				return this._occurredOn;
			}
			set
			{
				this._occurredOn = value;
			}
		}
		public int MenuItemId
		{
			get
			{
				return this._menuItemId;
			}
			set
			{
				this._menuItemId = value;
			}
		}
		public virtual object Get(int fieldPos)
		{
			switch (fieldPos)
			{
			case 0: return this.OrderId;
			case 1: return this.ClientId;
			case 2: return this.OccurredOn;
			case 3: return this.MenuItemId;
			default: throw new global::Avro.AvroRuntimeException("Bad index " + fieldPos + " in Get()");
			};
		}
		public virtual void Put(int fieldPos, object fieldValue)
		{
			switch (fieldPos)
			{
			case 0: this.OrderId = (System.Int32)fieldValue; break;
			case 1: this.ClientId = (System.Int32)fieldValue; break;
			case 2: this.OccurredOn = (System.Int64)fieldValue; break;
			case 3: this.MenuItemId = (System.Int32)fieldValue; break;
			default: throw new global::Avro.AvroRuntimeException("Bad index " + fieldPos + " in Put()");
			};
		}
	}
}
