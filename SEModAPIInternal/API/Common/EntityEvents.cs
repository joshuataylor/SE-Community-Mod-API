﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SEModAPIInternal.API.Entity;

namespace SEModAPIInternal.API.Common
{
	public class EntityEventManager
	{
		public enum EntityEventType
		{
			OnPlayerJoined,
			OnPlayerLeft,
			OnBaseEntityMoved,
			OnCubeGridMoved,
			OnCubeGridCreated,
			OnCubeGridDeleted,
		}

		public struct EntityEvent
		{
			public EntityEventType type;
			public DateTime timestamp;
			public BaseObject entity;
		}

		#region "Attributes"

		private static EntityEventManager m_instance;
		private List<EntityEvent> m_entityEvents;
		private List<EntityEvent> m_entityEventsBuffer;
		private bool m_isResourceLocked;

		#endregion

		#region "Constructors and Initializers"

		protected EntityEventManager()
		{
			m_entityEvents = new List<EntityEvent>();
			m_entityEventsBuffer = new List<EntityEvent>();
			m_isResourceLocked = false;

			m_instance = this;

			Console.WriteLine("Finished loading EntityEventManager");
		}

		#endregion

		#region "Properties"

		public static EntityEventManager Instance
		{
			get
			{
				if (m_instance == null)
					m_instance = new EntityEventManager();

				return m_instance;
			}
		}

		public List<EntityEvent> EntityEvents
		{
			get
			{
				List<EntityEvent> copy = new List<EntityEvent>(m_entityEvents.ToArray());
				return copy;
			}
		}

		public bool ResourceLocked
		{
			get { return m_isResourceLocked; }
			set
			{
				if (value == false)
				{
					m_entityEvents.AddList(m_entityEventsBuffer);
				}

				m_isResourceLocked = value;
			}
		}

		#endregion

		#region "Methods"

		public void AddEvent(EntityEvent newEvent)
		{
			if (ResourceLocked)
				m_entityEventsBuffer.Add(newEvent);
			else
				m_entityEvents.Add(newEvent);
		}

		public void ClearEvents()
		{
			m_entityEvents.Clear();
		}

		#endregion
	}
}