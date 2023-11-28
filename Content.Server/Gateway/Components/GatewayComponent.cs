using Content.Server.Gateway.Systems;
using Robust.Shared.Audio;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom;

namespace Content.Server.Gateway.Components;

/// <summary>
/// Controlling gateway that links to other gateway destinations on the server.
/// </summary>
[RegisterComponent, Access(typeof(GatewaySystem))]
public sealed partial class GatewayComponent : Component
{
    /// <summary>
    /// Whether this destination is shown in the gateway ui.
    /// If you are making a gateway for an admeme set this once you are ready for players to select it.
    /// </summary>
    [DataField, ViewVariables(VVAccess.ReadWrite)]
    public bool Enabled;

    /// <summary>
    /// Can the gateway be interacted with? If false then only settable via admins / mappers.
    /// </summary>
    [DataField, ViewVariables(VVAccess.ReadWrite)]
    public bool Interactable = true;

    /// <summary>
    /// Name as it shows up on the ui of station gateways.
    /// </summary>
    [DataField, ViewVariables(VVAccess.ReadWrite)]
    public FormattedMessage Name = new();

    /// <summary>
    /// Sound to play when opening the portal.
    /// </summary>
    /// <remarks>
    /// Originally named PortalSound as it was used for opening and closing.
    /// </remarks>
    [DataField("portalSound")]
    public SoundSpecifier OpenSound = new SoundPathSpecifier("/Audio/Effects/Lightning/lightningbolt.ogg");

    /// <summary>
    /// Sound to play when closing the portal.
    /// </summary>
    [DataField]
    public SoundSpecifier CloseSound = new SoundPathSpecifier("/Audio/Effects/Lightning/lightningbolt.ogg");

    /// <summary>
    /// Sound to play when trying to open or close the portal and missing access.
    /// </summary>
    [DataField]
    public SoundSpecifier AccessDeniedSound = new SoundPathSpecifier("/Audio/Machines/custom_deny.ogg");

    /// <summary>
    /// Every other gateway destination on the server.
    /// </summary>
    /// <remarks>
    /// Added on startup and when a new destination portal is created.
    /// </remarks>
    [DataField]
    public HashSet<EntityUid> Destinations = new();

    /// <summary>
    /// The time at which the portal will be closed.
    /// </summary>
    [ViewVariables(VVAccess.ReadWrite), DataField(customTypeSerializer: typeof(TimeOffsetSerializer))]
    public TimeSpan NextClose;

    /// <summary>
    /// The time at which the portal was last opened.
    /// Only used for UI.
    /// </summary>
    [DataField(customTypeSerializer: typeof(TimeOffsetSerializer))]
    public TimeSpan LastOpen;
}
