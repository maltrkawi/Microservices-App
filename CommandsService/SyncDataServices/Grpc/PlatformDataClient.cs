using AutoMapper;
using CommandsService.Models;
using Grpc.Net.Client;
using PlatformService;

namespace CommandsService.SyncDataServices.Grpc
{
    public class PlatformDataClient : IPlatformDataClient
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public PlatformDataClient(IConfiguration config, IMapper mapper)
        {
            _config = config;
            _mapper = mapper;
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            Console.WriteLine($"--> Calling gRPC Service: {_config["GrpcPlatform"]}");
            var channel = GrpcChannel.ForAddress(_config["GrpcPlatform"]);
            var client = new GrpcPlatform.GrpcPlatformClient(channel);
            var request = new GetAllRequests();

            try
            {
                var response = client.GetAllPlatforms(request);
                return _mapper.Map<IEnumerable<Platform>>(response.Platform);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not call gRPC server: {ex.Message}");
                return null;
            }
        }
    }
}
