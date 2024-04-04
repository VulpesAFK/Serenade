/*
    * Mediator used between states to prevent cross state variable passing
*/
public class PlayerStateData {
    public int amountOfJumpsLeft;
    public void ResetAmountOfJumps(int amount) => amountOfJumpsLeft = amount;
    public void DecreaseAmountOfJumpsLeft() => amountOfJumpsLeft--;

}