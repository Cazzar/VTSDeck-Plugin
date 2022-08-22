using Plugin.Contracts.Actions;

namespace Plugin.Contracts;

internal interface IButtonReactions
{
    void KeyDown(IKeyDown keyDown);
    void KeyUp(IKeyUp keyUp);
}