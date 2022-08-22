using VTubeStudioAPI.Responses;

namespace VTubeStudioAPI;
internal class VtubeStudioMapping : TypeMapConverter<ApiResponse>
{
    public VtubeStudioMapping() : base(new ()
    {
        { "AuthenticationTokenResponse", typeof(ApiResponse<AuthenticateResponse>) },
        { "APIError", typeof(ApiResponse<ApiErrorResponse>) },
        { "AuthenticationResponse", typeof(ApiResponse<AuthenticationResponse>) },
        { "StatisticsResponse", typeof (ApiResponse<StatisticsResponse>) },
        { "CurrentModelResponse", typeof(ApiResponse<CurrentModelResponse>) },
        { "AvailableModelsResponse", typeof(ApiResponse<AvailableModelsResponse>) },
        { "ModelLoadResponse", typeof(ApiResponse<ModelLoadResponse>) },
        { "MoveModelResponse", typeof(ApiResponse<MoveModelResponse>) },
        { "HotkeysInCurrentModelResponse", typeof(ApiResponse<ModelHotkeysResponse>) },
        { "HotkeyTriggerResponse", typeof(ApiResponse<HotkeyTriggerResponse>) },
        //{ "ExpressionStateResponse", typeof(ApiResponse<ExpressionStateResponse>) },
        { "ExpressionActivationResponse", typeof(ApiResponse<ExpressionActivationResponse>) },
        //{ "ArtMeshListResponse", typeof(ApiResponse<ArtMeshListResponse>) },
        //{ "ColorTintResponse", typeof(ApiResponse<ColorTintResponse>) },
        //{ "ArtMeshListResponse", typeof(ApiResponse<ArtMeshListResponse>) },
        //{ "SceneColorOverlayInfoResponse", typeof(ApiResponse<SceneColorOverlayInfoResponse>) },
        //{ "FaceFoundResponse", typeof(ApiResponse<FaceFoundResponse>) },
        //{ "InputParameterListResponse", typeof(ApiResponse<InputParameterListResponse>) },
        //{ "ParameterValueResponse", typeof(ApiResponse<ParameterValueResponse>) },
        //{ "Live2DParameterListResponse", typeof(ApiResponse<Live2DParameterListResponse>) },
        //{ "ParameterCreationResponse", typeof(ApiResponse<ParameterCreationResponse>) },
        //{ "ParameterDeletionResponse", typeof(ApiResponse<ParameterDeletionResponse>) },
        { "InjectParameterDataResponse", typeof(ApiResponse<InjectParameterDataResponse>) },
        //{ "GetCurrentModelPhysicsResponse", typeof(ApiResponse<GetCurrentModelPhysicsResponse>) },
        { "SetCurrentModelPhysicsResponse", typeof(ApiResponse<SetCurrentModelPhysicsResponse>) },
        //{ "NDIConfigResponse", typeof(ApiResponse<NDIConfigResponse>) },
        //{ "ItemListResponse", typeof(ApiResponse<ItemListResponse>) },
        //{ "ItemLoadResponse", typeof(ApiResponse<ItemLoadResponse>) },
        //{ "ItemUnloadResponse", typeof(ApiResponse<ItemUnloadResponse>) },
        //{ "ItemAnimationControlResponse", typeof(ApiResponse<ItemAnimationControlResponse>) },
        //{ "ItemMoveResponse", typeof(ApiResponse<ItemMoveResponse>) },
    }, "messageType")
    {
    }
}
