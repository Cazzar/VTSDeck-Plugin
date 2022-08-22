namespace VTubeStudioAPI.Responses;

public enum ResponseType
{
    APIError,
    APIStateResponse,
    AuthenticationTokenResponse,
    AuthenticationResponse,
    StatisticsResponse,
    VTSFolderInfoResponse,
    CurrentModelResponse,
    AvailableModelsResponse,
    ModelLoadResponse,
    MoveModelResponse,
    HotkeysInCurrentModelResponse,
    HotkeyTriggerResponse,
    ArtMeshListResponse,
    ColorTintResponse,
    FaceFoundResponse,
    InputParameterListResponse,
    ParameterValueResponse,
    Live2DParameterListResponse,
    ParameterCreationResponse,
    ParameterDeletionResponse,
    InjectParameterDataResponse,
}
