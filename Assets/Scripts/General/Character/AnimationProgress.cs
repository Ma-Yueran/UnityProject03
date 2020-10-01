/// <summary>
/// AnimationProgress helps StateData to execute functions only once.
/// </summary>
public class AnimationProgress
{
    /// <summary>
    /// is SetAttack of the CharacterController called.
    /// </summary>
    public bool setAttackActive;

    /// <summary>
    /// the dodge direction of the character.
    /// </summary>
    public Direction dodgeDirection;

    /// <summary>
    /// Constructs an AnimationProgress.
    /// </summary>
    public AnimationProgress()
    {
        ResetProgress();
    }

    /// <summary>
    /// Resets the progress.
    /// </summary>
    public void ResetProgress()
    {
        setAttackActive = false;
    }
}
