﻿using ZM.Domain.Common;
using ZM.Domain.Users;

namespace ZM.Domain.ChatGroups;

/// <summary>
/// Группа.
/// </summary>
public class ChatGroup : Chat
{
	private ChatGroup() { }

	public ChatGroup(string name, Guid creatorId, ICollection<User> members)
	{
		Name = name;
		CreatorId = creatorId;
		Members = members;
	}

	/// <summary>
	/// Название.
	/// </summary>
	public string Name { get; protected set; }

	/// <summary>
	/// Идентификатор создателя.
	/// </summary>
	public Guid CreatorId { get; protected set; }

	/// <summary>
	/// Создатель.
	/// </summary>
	public User Creator { get; protected set; }

	/// <summary>
	/// Сообщения.
	/// </summary>
	public ICollection<ChatGroupMessage> Messages { get; protected set; }
}
