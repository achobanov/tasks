using Challenge.Domain.Chance;
using Challenge.Domain.Core;
using Challenge.Domain;
using Challenge.Console;

var randomProvider = new RandomProvider();
var outcomeFactory = new BetOutcomeFactory(randomProvider);
var loss = outcomeFactory.Loss(50);
var win = outcomeFactory.Win(40, 1.01f, 2);
var bigWin = outcomeFactory.Win(10, 2, 10);
var slotMachine = new SlotMachine(randomProvider, 1, 10, loss, win, bigWin);

var @interface = new Integration(new ConsoleShell());
@interface.Register(slotMachine);
@interface.Select(slotMachine.Name);
@interface.Start();