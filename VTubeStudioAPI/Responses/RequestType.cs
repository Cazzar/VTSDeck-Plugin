namespace VTubeStudioAPI.Responses;

public enum RequestType
{
    APIStateRequest,
    AuthenticationTokenRequest,
    AuthenticationRequest,
    StatisticsRequest,
    VTSFolderInfoRequest,
    CurrentModelRequest,
    AvailableModelsRequest,
    ModelLoadRequest,
    MoveModelRequest,
    HotkeysInCurrentModelRequest,
    HotkeyTriggerRequest,
    ArtMeshListRequest,
    ColorTintRequest,
    FaceFoundRequest,
    InputParameterListRequest,
    ParameterValueRequest,
    Live2DParameterListRequest,
    ParameterCreationRequest,
    ParameterDeletionRequest,
    InjectParameterDataRequest,
}
