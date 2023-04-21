using RawDeal;
using RawDealView;

const string DeckFolder = "04-NoEffects";

View view = View.BuildConsoleView();
string deckFolder = Path.Combine("data", DeckFolder);
Game game = new Game(view, deckFolder);
game.Play();
