using Grpc.Core;
using User.Module.Service.Interface;

namespace User.Module.Greeter
{
    public class UserServiceGrpcImple: UserServiceGrpc.UserServiceGrpcBase
    {
        private readonly IUserService _service;

        public UserServiceGrpcImple( IUserService service)
        {
            this._service = service;
        }

        public override async Task<AuthUserResponse> GetUserByIdForAuth(UserRequest request, ServerCallContext context) {

            var user = await this._service.GetById(request.Id);

            return new AuthUserResponse { 
                Id = user.Id,
                FirstName = user.First_name,
                LastName = user.Last_name,
                Age = user.Age,
                Username = user.Username,
                Email = user.Email,
                Password = user.Password,
                Roles = user.Roles,
                RefreshToken = user.RefreshToken
            };
        }

        public override async Task<AuthUserResponse> UpdateRefreshToken(RefreshTokenRequest request, ServerCallContext context) {

            var user = await this._service.UpdateToken(request.Id, request.RefreshToken);

            return new AuthUserResponse {
                Id = user.Id,
                FirstName = user.First_name,
                LastName = user.Last_name,
                Age = user.Age,
                Username = user.Username,
                Email = user.Email,
                Password = user.Password,
                Roles = user.Roles,
                RefreshToken = user.RefreshToken
            };
        }
    }
}
