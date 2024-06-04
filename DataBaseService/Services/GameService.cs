using DataBaseService.Db.Entity;
using DataBaseService.Db.Repository;
using Grpc.Core;

namespace DataBaseService.Services
{
      public class GameService(UserRepository userRepository) : DataBaseService.GameService.GameServiceBase
      {
      
            public override Task<UserReply> SendUserInfo(UserRequest request, ServerCallContext context) 
            {
                  UnityEngine.Debug.Log(request.Login);
                  UnityEngine.Debug.Log(request.Password);
                  UnityEngine.Debug.Log(request.SchoolClass);
                  UnityEngine.Debug.Log(request.Score);
                  
                  var userData = new UserData(request.Login, request.Password, request.SchoolClass, request.Score);
                  userRepository.SaveToDb(userData);

                  return Task.FromResult(new UserReply() { Message = "Все ок " }); 
            }
      }
}

