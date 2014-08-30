﻿using System;
using System.Collections.Generic;

namespace KerbalKonstructs
{
	class StaticDatabase
	{
		//Groups are stored by name within the body name
		private Dictionary<string, Dictionary<string, StaticGroup>> groupList = new Dictionary<string,Dictionary<string,StaticGroup>>();
		private List<StaticGroup> activeGroups = new List<StaticGroup>();

		public Boolean addStatic(StaticObject obj)
		{
			String bodyName = obj.parentBody.bodyName;
			String groupName = obj.groupName;

			if (!groupList.ContainsKey(bodyName))
				groupList.Add(bodyName, new Dictionary<string, StaticGroup>());

			if (!groupList[bodyName].ContainsKey(groupName))
			{
				StaticGroup group = new StaticGroup(bodyName, groupName);
				//Ungrouped objects get individually cached. New acts the same as Ungrouped but stores unsaved statics instead.
				if (obj.groupName == "Ungrouped" || obj.groupName == "New")
					group.alwaysActive = true;
				groupList[bodyName].Add(groupName, group);
			}

			groupList[obj.parentBody.bodyName][obj.groupName].addStatic(obj);

			return activeGroups.Contains(groupList[obj.parentBody.bodyName][obj.groupName]);
		}
	}
}
