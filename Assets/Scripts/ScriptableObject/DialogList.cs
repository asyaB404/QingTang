using System.Collections.Generic;
using UnityEngine;

namespace QTConfig
{
	[CreateAssetMenu(fileName = "DialogList", menuName = "ScriptableObject/Dialog1")]
	public class DialogList : ExcelableScriptableObject
	{
		public List<DialogListInfoClass> list = new();

		public override void Init(object[] objects)
		{
			foreach (var obj in objects)
			{
				var obj1 = obj as DialogListInfoClass;
				list.Add(obj1);
			}
		}
	}
}
