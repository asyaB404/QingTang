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
		public string move;
		public int sceneId;
		public string sound;

#if UNITY_EDITOR
		public void Init(DataRow row)
		{
			id = int.Parse(row[0].ToString());
			roleId = int.Parse(row[1].ToString());
			dialog = row[2].ToString();
			face = int.Parse(row[3].ToString());
			move = row[4].ToString();
			sceneId = int.Parse(row[5].ToString());
			sound = row[6].ToString();
		}
#endif
	}
}
