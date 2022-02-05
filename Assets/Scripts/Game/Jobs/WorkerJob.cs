using UnityEngine;

public class WorkerJob : JobBase
{
    private Vector2 StartingPosition { get; set; }
    private Vector2 FoodSourcePoint { get; set; }
    private Unit Unit { get; set; }
    private JobStage Stage { get; set; }
    public override bool Finished { get; set; }
    public WorkerJob(Vector2 foodSourcePoint, Unit unit)
    {
        FoodSourcePoint = foodSourcePoint;
        Unit = unit;
        StartingPosition = Unit.gameObject.transform.position;
        Finished = false;
        Stage = JobStage.COLLECTING_FOOD;
    }

    private enum JobStage
    {
        COLLECTING_FOOD,
        GOING_BACK,
        COMPLETED
    }
    private void GoToFoodPoint() => Unit.MoveToPoint(FoodSourcePoint);
    private void GoBack() => Unit.MoveToPoint(StartingPosition);

    public override void Perform()
    {
        if (GameManager.Instance.GameState != GameState.PLAYING) return;

        Vector2 currentPosition = Unit.gameObject.transform.position;
        if (Stage == JobStage.COLLECTING_FOOD)
        {
            if (currentPosition.Equals(FoodSourcePoint))
                Stage = JobStage.GOING_BACK;
            GoToFoodPoint();
        }
        else if (Stage == JobStage.GOING_BACK)
        {
            if (currentPosition.Equals(StartingPosition))
                Stage = JobStage.COMPLETED;
            GoBack();
        }
        else if (Stage == JobStage.COMPLETED)
            Finished = true;
    }
    public override void Conclude()
    {
        Unit.Owner.AddResources(
            Unit.carryAmount);
        Unit.Kill();
    }

}
