namespace RawDealView;

class CardFormatter:ItemFormatter
{
    protected override string GetTextIndicatingThatTheListOfItemsIsEmpty()
        => "No hay cartas aquí";
}