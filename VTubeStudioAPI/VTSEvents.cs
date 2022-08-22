using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using VTubeStudioAPI.Responses;
using WebSocketSharp;

namespace VTubeStudioAPI;
public static class VTSEvents
{
    public static event EventHandler<EventArgs>? SocketConnected;
    public static event EventHandler<CloseEventArgs>? SocketClosed;
    public static event EventHandler<ApiEventArgs<ApiError>>? OnApiError;
    public static event EventHandler<ApiEventArgs<ApiStateResponse>>? OnApiState;
    public static event EventHandler<ApiEventArgs<AuthenticationResponse>>? OnAuthenticationResponse;
    public static event EventHandler<ApiEventArgs<AvailableModelsResponse>>? OnAvailableModels;
    public static event EventHandler<ApiEventArgs<ModelLoadResponse>>? OnModelLoad;
    public static event EventHandler<ApiEventArgs<ModelHotkeysResponse>>? OnModelHotkeys;
    public static event EventHandler<ApiEventArgs<HotkeyTriggerResponse>>? OnHotkeyTriggered;
    public static event EventHandler<ApiEventArgs<CurrentModelResponse>>? OnCurrentModelInformation;
    public static event EventHandler<ApiEventArgs<MoveModelResponse>>? OnModelMove;

    internal static void TriggerSocketConnected(object sender) => SocketConnected?.Invoke(sender, new ());
    internal static void TriggerSocketClosed(object sender, CloseEventArgs args) => SocketClosed?.Invoke(sender, args);
    internal static void TriggerOnApiError(object sender, ApiEventArgs<ApiError> args) => OnApiError?.Invoke(sender, args);
    internal static void TriggerOnApiState(object sender, ApiEventArgs<ApiStateResponse> args) => OnApiState?.Invoke(sender, args);
    internal static void TriggerOnAuthenticationResponse(object sender, ApiEventArgs<AuthenticationResponse> args) => OnAuthenticationResponse?.Invoke(sender, args);
    internal static void TriggerOnAvailableModels(object sender, ApiEventArgs<AvailableModelsResponse> args) => OnAvailableModels?.Invoke(sender, args);
    internal static void TriggerOnModelLoad(object sender, ApiEventArgs<ModelLoadResponse> args) => OnModelLoad?.Invoke(sender, args);
    internal static void TriggerOnModelHotkeys(object sender, ApiEventArgs<ModelHotkeysResponse> args) => OnModelHotkeys?.Invoke(sender, args);
    internal static void TriggerOnHotkeyTriggered(object sender, ApiEventArgs<HotkeyTriggerResponse> args) => OnHotkeyTriggered?.Invoke(sender, args);
    internal static void TriggerOnCurrentModelInformation(object sender, ApiEventArgs<CurrentModelResponse> args) => OnCurrentModelInformation?.Invoke(sender, args);
    internal static void TriggerOnModelMove(object sender, ApiEventArgs<MoveModelResponse> args) => OnModelMove?.Invoke(sender, args);
    //internal static void Trigger(object sender, ApiEventArgs<> args) => ?.Invoke(sender, args);
}
