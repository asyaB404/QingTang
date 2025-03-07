using UnityEngine;
#if UNITY_EDITOR
using System.Data;
using UnityEditor;
#endif

namespace QTConfig
{
	[System.Serializable]
	public class DialogListInfoClass
	{
		public int id;
		public int roleId;
		public string dialog;
		public int face;
		public RoldAnimType animType;

#if UNITY_EDITOR
		public void Init(DataRow row)
		{
			id = int.Parse(row[0].ToString());
			roleId = int.Parse(row[1].ToString());
			dialog = row[2].ToString();
			face = int.Parse(row[3].ToString());
			animType = (RoldAnimType)System.Enum.Parse(typeof(RoldAnimType), row[4].ToString());
		}
#endif
	}
}
