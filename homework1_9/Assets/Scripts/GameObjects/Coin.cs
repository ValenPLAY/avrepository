public class Coin : Consumable
{
    public int coinValue = 1;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {

    }



    public override void Pickup()
    {
        StatsContainer.coinsAmount += coinValue;
    }
}
