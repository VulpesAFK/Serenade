public class PlayerStateData {
    public int amountOfJumpsLeft;
    public void ResetAmountOfJumps(int amount) => amountOfJumpsLeft = amount;
    public void DecreaseAmountOfJumpsLeft() => amountOfJumpsLeft--;
    public bool coyoteTime;

}