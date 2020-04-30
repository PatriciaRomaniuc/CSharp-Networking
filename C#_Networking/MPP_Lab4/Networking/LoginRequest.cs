using Model;
using System;
namespace Networking
{
	[Serializable]
    public class LoginRequest : Request
	{
		private Model.User user;

		public LoginRequest(User user)
		{
			this.user = user;
		}

		public virtual User User
		{
			get
			{
				return user;
			}
		}
	}

}