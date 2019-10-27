namespace Bloodstone.AI.Steering
{
    public interface ISteeringPipeline
    {
        SteeringPrediction GetSteeringPrediction();
    }
}