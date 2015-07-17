using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseTools.Model
{
	[Serializable]
	public class DataBaseSchema
	{
		public string Name { get; set; }
		public List<TableSchema> Tables { get; set; }
		public override string ToString()
		{
			return Name;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="dataBaseSchema"></param>
		/// <returns>结构一致时返回null，不一致时返回具体的差异信息</returns>
		public List<string> CompareSchema(DataBaseSchema dataBaseSchema)
		{
			List<string> result = new List<string>();
			if (dataBaseSchema != null)
			{
				if (this.Name != dataBaseSchema.Name)
				{
					result.Add("数据库名称不一致");
					return result;
				}

				if (this.Tables == null || dataBaseSchema.Tables == null)
				{
					result.Add("数据库无表结构");
					return result;
				}
				if ((this.Tables == null && dataBaseSchema.Tables == null) && Tables.Count != dataBaseSchema.Tables.Count)
				{
					result.Add("数据库表个数不一致");
					List<TableSchema> ts = (Tables.Count>dataBaseSchema.Tables.Count)?Tables:dataBaseSchema.Tables;
					List<TableSchema> lts = (Tables.Count<dataBaseSchema.Tables.Count)?Tables:dataBaseSchema.Tables;
					string database = (Tables.Count > dataBaseSchema.Tables.Count) ? "元数据库_" + this.Name : "目标数据库_" + dataBaseSchema.Name;
					foreach (var t in ts)
					{
						if (!lts.Contains(t))
						{
							result.Add(string.Format("{0}缺少表{1}", database, t.Name));
						}
						else
						{
							//var r = t.CompareSchema(lts);
						}
					}
					return result;
				}
				else
				{
					foreach (var t in this.Tables)
					{
						foreach (var tt in dataBaseSchema.Tables)
						{
							if (tt.Name == t.Name)
							{
								var r = tt.CompareSchema(t);
								if (r != null)
									return r;
							}
						}
					}
				}
					
				
				return null;

			}
			else {				
				result.Add("输入不合法！");
				return result;
			}
		}
	}

	[Serializable]
	public class TableSchema
	{
		public string Name { get; set; }
		public List<FieldSchema> Cloumes { get; set; }
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if ((obj.GetType().Equals(this.GetType())) == false)
			{
				return false;
			}
			TableSchema temp = null;
			temp = (TableSchema)obj;
			return this.Name.Equals(temp.Name);
		}
		
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="tableSchema"></param>
		/// <returns>返回null说明机构一致</returns>
		public List<string> CompareSchema(TableSchema tableSchema)
		{
			List<string> result = new List<string>();
			if (tableSchema == null)
			{
				result.Add("输入不合法！");
				return result;
			}

			if (this.Cloumes.Count != tableSchema.Cloumes.Count)
			{
				result.Add(string.Format("表{0}中字段数量不一致", this.Name));

				return result;
			}
			foreach (var f in this.Cloumes)
			{
				foreach (var tf in this.Cloumes)
				{
					if (f.Field == tf.Field)
					{
						var r = f.CompareSchema(tf);
						if (r != null)
							return string.Format("表{0}中字段{1}{2}",this.Name,f.Field,r);
					}
				}
			}
			return null;
		}

	}

	[Serializable]
	public class FieldSchema
	{
		public string Field { get; set; }
		public string Type { get; set; }
		public string Null { get; set; }
		public string Key { get; set; }
		public string Default { get; set; }
		public string Extra { get; set; }

		public string CompareSchema(FieldSchema fieldSchema)
		{
			if (Type != fieldSchema.Type)
				return "Type 不一致";
			if (Null != fieldSchema.Null)
				return "Null 不一致";
			if (Type != fieldSchema.Type)
				return "Type 不一致";
			if (Key != fieldSchema.Key)
				return "Key 不一致";
			if (Default != fieldSchema.Default)
				return "Default 不一致";
			if (Extra != fieldSchema.Extra)
				return "Extra 不一致";
			return null;
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if ((obj.GetType().Equals(this.GetType())) == false)
			{
				return false;
			}
			FieldSchema temp = null;
			temp = (FieldSchema)obj;
			return this.Field.Equals(temp.Field);
		}

		public override int GetHashCode()
		{
			return this.Field.GetHashCode();
		}
	}
}
